using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MsgBox : MonoBehaviour {
    private List<string> MessageQueue = new List<string>();
    private List<string> DisplayQueue = new List<string>();
    private int MaxLine = 40;
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
        MessageQueue.Add(text);
    }

    void Display() {
        lock (MessageQueue) {
            DisplayQueue.AddRange(MessageQueue);
            MessageQueue.Clear();
            int dcount = DisplayQueue.Count;
            int removeCount = dcount - MaxLine;
            DisplayQueue.RemoveRange(0, removeCount);

            string result = "";
            foreach (var msg in DisplayQueue) {
                result += "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg + "\n";
            }
            TextMesh.text = result;
        }
    }
}
