using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(requiredComponent: typeof(ARRaycastManager), requiredComponent2: typeof(ARPlaneManager))]
public class PlaceGame : MonoBehaviour {
    /// <summary>
    /// For testing. Spawn a cube.
    /// </summary>
    [SerializeField]
    private GameObject Prefab;

    public Button RotateButtonL;
    public Button RotateButtonR;

    private ARRaycastManager ARRaycastManager;
    private ARPlaneManager ARPlaneManager;

    private List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    private MsgBox MsgBox;

    private Vector2 TouchPosition;

    private GameObject GameOrigin;

    private void Awake() {
        ARRaycastManager = GetComponent<ARRaycastManager>();
        ARPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Start is called before the first frame update
    void Start() {
        MsgBox = GameObject.FindGameObjectWithTag("MsgBox").GetComponent<MsgBox>();
        GameOrigin = GameObject.FindGameObjectWithTag("GameOrigin");
        MsgBox.AddText("Welcome!");
    }

    // Update is called once per frame
    void Update() {
        if (!TryGetTouchPosition(out TouchPosition)) {
            return;
        }
        FingerDown(TouchPosition);
        if (RotateButtonL != null && RotateButtonL.HasMouseCapture()) {
            RotateGameOrigin("R");
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition) {
        if (Input.touchCount > 0) {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void FingerDown(Vector2 touchPosition) {
        MsgBox.AddText("FingerDown: " + touchPosition);
        if (ARRaycastManager.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon)) {
            Pose pose = Hits[0].pose;
            //Instantiate(Prefab, pose.position,pose.rotation);
            GameOrigin.transform.position = pose.position;
            MsgBox.AddText($"FingerDown: Init obj at {pose.position} {pose.rotation}");
        }
    }

    private void RotateGameOrigin(string direction) {
        if (direction == "L") {
            MsgBox.AddText("RotateGameOrigin L");
            GameOrigin.transform.rotation.
        }
        else if (direction == "R") {
            MsgBox.AddText("RotateGameOrigin R");

        }
        else {
            MsgBox.AddText("RotateGameOrigin error. direction=" + direction);
        }
    }
}
