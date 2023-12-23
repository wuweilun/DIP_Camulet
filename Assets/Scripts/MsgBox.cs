using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MsgBox : MonoBehaviour {
    private List<string> MessageQueue = new List<string>();
    private int MaxLine = 25;
    private TextMeshProUGUI TextMesh;

    // Start is called before the first frame update
    void Start() {
        TextMesh = GameObject.FindGameObjectWithTag("MsgBoxTextMesh").GetComponent<TextMeshProUGUI>();
        TextMesh.text = "";
    }

    // Update is called once per frame
    void Update() {
        Display();
    }

    public void AddText(string text) {
        MessageQueue.Add("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text + "\n");
    }

    void Display() {
        lock (MessageQueue) {
            int removeCount = MessageQueue.Count - MaxLine;
            MessageQueue.RemoveRange(0, removeCount);

            string result = "";
            foreach (var msg in MessageQueue) {
                result += msg;
            }
            TextMesh.text = result;
        }
    }
}
