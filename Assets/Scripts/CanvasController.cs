using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    [SerializeField]
    public GameObject MsgBoxTextMesh;

    // Start is called before the first frame update
    void Start() {
        MsgBoxTextMesh.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetMsgBoxActive(bool isShow) {
        if (MsgBoxTextMesh != null) {
            MsgBoxTextMesh.SetActive(isShow);
        }
    }
}
