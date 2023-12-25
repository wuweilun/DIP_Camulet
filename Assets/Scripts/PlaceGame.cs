using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(requiredComponent: typeof(ARRaycastManager), requiredComponent2: typeof(ARPlaneManager))]
public class PlaceGame : MonoBehaviour {
    /// <summary>
    /// For testing. Spawn a cube.
    /// </summary>

    [SerializeField]
    public Button RotateButtonL;

    [SerializeField]
    public Button RotateButtonR;

    [SerializeField]
    public Toggle IsPositionLockedToggle;

    private ARRaycastManager ARRaycastManager;
    private ARPlaneManager ARPlaneManager;

    private List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    private MsgBox MsgBox;

    private Vector2 TouchPosition;

    private GameObject GameOrigin;

    private int RotationStep = 2;

    private bool IsPositionLocked = false;

    private GameOriginRotateButtonL btnL;
    private GameOriginRotateButtonR btnR;

    private float ToolbarHeight;

    private float Scale = 1.0f;

    private float PositionYOffset = 0.0f;

    private GameObject SpiralPlane;

    private void Awake() {
        ARRaycastManager = GetComponent<ARRaycastManager>();
        ARPlaneManager = GetComponent<ARPlaneManager>();
        try {
            btnL = RotateButtonL.GetComponent<GameOriginRotateButtonL>();
            btnR = RotateButtonR.GetComponent<GameOriginRotateButtonR>();
        }
        catch (Exception) {
            MsgBox.AddText("Can not find GameOriginRotateButtonL or GameOriginRotateButtonR");
        }
    }

    // Start is called before the first frame update
    void Start() {
        MsgBox = GameObject.FindGameObjectWithTag("MsgBox").GetComponent<MsgBox>();
        GameOrigin = GameObject.FindGameObjectWithTag("GameOrigin");
        this.SpiralPlane = GameObject.Find("/GameOrigin/Spiral_Plane");

        MsgBox.AddText("Welcome!");

        GameObject toolbar = GameObject.FindGameObjectWithTag("ToolBar");
        var rect = toolbar.GetComponent<RectTransform>();
        this.ToolbarHeight = rect.rect.height;
    }

    // Update is called once per frame
    void Update() {
        if (IsPositionLockedToggle != null) {
            this.IsPositionLocked = IsPositionLockedToggle.isOn;
        }

        if (this.IsPositionLocked) {
            ARRaycastManager.enabled = false;
            ARPlaneManager.enabled = false;
        }
        else {
            ARRaycastManager.enabled = true;
            ARPlaneManager.enabled = true;
            if (TryGetTouchPosition(out TouchPosition)) {
                FingerDown(TouchPosition);
            }
            //if (btnL != null && btnL.ButtonPressed) {
            //    RotateGameOrigin(-1);
            //}
            //if (btnR != null && btnR.ButtonPressed) {
            //    RotateGameOrigin(1);
            //}
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

        // (0,0) is at bottom left of the screen.
        // Filter out the touch on the toolbar.
        if (touchPosition.y <= this.ToolbarHeight) {
            MsgBox.AddText("FingerDown at toolbar " + touchPosition);
            return;
        }

        if (ARRaycastManager.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon)) {
            Pose pose = Hits[0].pose;
            //Instantiate(Prefab, pose.position,pose.rotation);

            Vector3 diff = pose.position - this.SpiralPlane.transform.position;
            GameOrigin.transform.position += diff;
            MsgBox.AddText($"FingerDown: Init obj at {pose.position} {pose.rotation}");
        }
    }

    public void RotateGameOriginL() {
        MsgBox.AddText("RotateGameOrigin L");
        RotateGameOrigin(-this.RotationStep);
    }

    public void RotateGameOriginR() {
        MsgBox.AddText("RotateGameOrigin R");
        RotateGameOrigin(this.RotationStep);
    }

    private void RotateGameOrigin(float angle) {
        if (!this.IsPositionLocked) {
            GameOrigin.transform.Rotate(0, angle, 0);
            MsgBox.AddText($"RotateGameOrigin {this.GameOrigin.transform.rotation}");
        }
    }

    public void ScaleButtonUp() {
        MsgBox.AddText($"ScaleButtonUp.");
        ChangeScale(this.Scale * 1.05f);
    }

    public void ScaleButtonDown() {
        MsgBox.AddText($"ScaleButtonDown.");
        ChangeScale(this.Scale * 0.95f);
    }

    private void ChangeScale(float newScale) {
        if (!this.IsPositionLocked) {
            this.Scale = newScale;
            this.GameOrigin.transform.localScale = new Vector3(newScale, newScale, newScale);
            MsgBox.AddText($"New scale: [{newScale}]");
        }
    }

    public void PositionYUp() {
        ChangePositionY(0.02f);
    }

    public void PositionYDown() {
        ChangePositionY(-0.02f);
    }

    private void ChangePositionY(float yOffset) {
        if (!this.IsPositionLocked) {
            this.PositionYOffset += yOffset;
            this.GameOrigin.transform.Translate(new Vector3(0, yOffset, 0));
            MsgBox.AddText($"PositionYOffset: [{this.PositionYOffset}]");
        }
    }
}
