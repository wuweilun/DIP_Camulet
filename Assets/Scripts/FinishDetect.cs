using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static MsgBox;

public class FinishDetect : MonoBehaviour
{
    // https://forum.unity.com/threads/how-to-get-ar-foundation-arcamera-to-interact-with-collider-triggers.695011/

    private MsgBox MsgBox;
    private PlayerMsg FinishMsgBox;
    private PlayerScoreMsg ScoreMsgBox;
    void Start()
    {
        BoxCollider coll = gameObject.AddComponent<BoxCollider>();
        MsgBox = GameObject.FindGameObjectWithTag("MsgBox").GetComponent<MsgBox>();
        FinishMsgBox = GameObject.FindGameObjectWithTag("PlayerMsgBox").GetComponent<PlayerMsg>();
        ScoreMsgBox = GameObject.FindGameObjectWithTag("ScoreMsgBox").GetComponent<PlayerScoreMsg>();
    }
    void OnTriggerEnter(Collider camera)
    {
        if (camera.gameObject.tag == "MainCamera")
        {
            Debug.Log("Enter finish area" ); 
            MsgBox.AddText("Enter finish area");
            if (ScoreMsgBox.CollectAllDragons()){
                FinishMsgBox.AddText("Congratulations!");
                StartCoroutine(ClearText());
            }
            else {
                FinishMsgBox.AddText("Not all divine dragons collected!");
                StartCoroutine(ClearText());
            }
            
        }
    }
    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(5);
        FinishMsgBox.Clear();
    }

}
