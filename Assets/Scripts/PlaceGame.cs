using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(requiredComponent: typeof(ARRaycastManager), requiredComponent2: typeof(ARPlaneManager))]
public class PlaceGame : MonoBehaviour {
    [SerializeField]
    private GameObject Prefab;

    private ARRaycastManager ARRaycastManager;

    private ARPlaneManager ARPlaneManager;

    private List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void Awake() {
        ARRaycastManager = GetComponent<ARRaycastManager>();
        ARPlaneManager = GetComponent<ARPlaneManager>();

    }

    private void OnEnable() {
        TouchSimulation.Enable();
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable() {
        TouchSimulation.Disable();
        TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(Finger finger) {
        if (finger.index != 0) {
            return;
        }

        if (ARRaycastManager.Raycast(finger.currentTouch.screenPosition, Hits, TrackableType.PlaneWithinPolygon)) {
            foreach (ARRaycastHit hit in Hits) {
                Pose pose = hit.pose;
                GameObject obj = Instantiate(Prefab, pose.position, pose.rotation);
            }
        }
    }
}
