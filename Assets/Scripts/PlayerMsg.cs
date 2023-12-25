using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerMsg : MonoBehaviour {
    public TextMeshProUGUI TextMesh;

    private List<string> MessageQueue = new List<string>();
    private int MaxLine = 1;

    // Start is called before the first frame update
    void Start() {
        TextMesh.text = "" ; // + light_count.ToString()
    }

    // Update is called once per frame
    void Update() {
        Display();
    }

    public void AddText(string text) {
        MessageQueue.Add( text + "\n");
    }
    
    void Display() {
        lock (MessageQueue) {
            int removeCount = MessageQueue.Count - MaxLine;
            if (removeCount > 0) {
                MessageQueue.RemoveRange(0, removeCount);
            }

            string result = "";
            foreach (var msg in MessageQueue) {
                result += msg;
            }
            TextMesh.text = result;
        }
    }

    public void Clear() {
        lock (MessageQueue) {
            MessageQueue.Clear();
        }
    }
}
