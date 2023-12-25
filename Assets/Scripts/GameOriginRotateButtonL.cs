using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOriginRotateButtonL : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool ButtonPressed;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerDown(PointerEventData eventData) {
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        ButtonPressed = false;
    }
}
