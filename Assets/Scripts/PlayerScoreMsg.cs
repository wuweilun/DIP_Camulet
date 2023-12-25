using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerScoreMsg : MonoBehaviour {
    public TextMeshProUGUI TextMesh;

    private List<string> MessageQueue = new List<string>();
    private int MaxLine = 1;
    private int light_count = 0;
    private int dragon_count = 0;

    // Start is called before the first frame update
    void Start() {
        TextMesh.text = "" ; // + light_count.ToString()
        MessageQueue.Add("lightning:" + light_count.ToString() + "\n" + "dragon:" + dragon_count.ToString());
        // TextMesh.color =  new Color(222, 41, 22, 255);
    }

    // Update is called once per frame
    void Update() {
        Display();
    }

    public void AddText(string text) {
        MessageQueue.Add( text + "\n");
    }
    
    public void TouchLight() {
        light_count = light_count + 1;
        MessageQueue.Add( "lightning:" + light_count.ToString() + "\n" + "dragon:" + dragon_count.ToString());
    }

    public void CollectDragon() {
        dragon_count = dragon_count + 1;
        MessageQueue.Add( "lightning:" + light_count.ToString() + "\n" + "dragon:" + dragon_count.ToString());
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
