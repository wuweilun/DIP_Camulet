using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static MsgBox;

public class LightningDetect : MonoBehaviour
{
    // https://forum.unity.com/threads/how-to-get-ar-foundation-arcamera-to-interact-with-collider-triggers.695011/
    /* Options of changing Lightning size:
       a. Scale x of SimpleLightningBoltAnimatedPrefab
       b. Change postion x in children LightningStart & LightningEnd */
    private MsgBox MsgBox;
    private PlayerScoreMsg ScoreMsgBox;
    void Start()
    {
        // get size of lighning by x & z coordinate
        Vector3 child_pos0 = gameObject.transform.GetChild(0).position;
        Vector3 child_pos1 = gameObject.transform.GetChild(1).position;
        // Debug.Log("child pos 0:"+child_pos0+"child pos 1:"+child_pos1);
        var dist_x = Mathf.Abs(child_pos0.x - child_pos1.x);
        var dist_z = Mathf.Abs(child_pos0.z - child_pos1.z);
        var len = Mathf.Sqrt(Mathf.Pow(dist_x,2) + Mathf.Pow(dist_z,2));
        Debug.Log("lightning len: "+len);

        Vector3 global_scale = gameObject.transform.lossyScale;
        // Debug.Log("global scale:"+ global_scale);

        var col_size_x = len/global_scale.x;
        var col_size_y = 0.5f/global_scale.y;
        var col_size_z = 0.5f/global_scale.z;

        // lightning.SetActive(false);
        BoxCollider coll = gameObject.AddComponent<BoxCollider>();
        coll.size = new Vector3(col_size_x, col_size_y, col_size_z);
        // Debug.Log("Add BoxCollider w/ size:"+ coll.size);
        // Debug.Log("Add BoxCollider w/ boundsize:"+ coll.bounds.size);  // depend on parent scale (not variable)
        // Debug.Log("Add BoxCollider w/ pos:"+ coll.transform.position);
        
        MsgBox = GameObject.FindGameObjectWithTag("MsgBox").GetComponent<MsgBox>();
        ScoreMsgBox = GameObject.FindGameObjectWithTag("ScoreMsgBox").GetComponent<PlayerScoreMsg>();
    }
    void OnTriggerEnter(Collider camera)
    {
        if (camera.gameObject.tag == "MainCamera")
        {
            Debug.Log("OnTriggerEnter!!" ); 
            MsgBox.AddText("Trigger Lightning!!");
            ScoreMsgBox.TouchLight();

            // Trigger phone vibration and damage effect
            StartCoroutine(VibrateScreenEffect());
            GameManager.instance.ApplyDamageEffect();
        }
    }

    IEnumerator VibrateScreenEffect()
    {
        // Vibrate the phone
        Handheld.Vibrate();

        // You can add additional effects or actions here during the vibration

        yield return null;
    }
    // TODO: show warning on screen when trigger
}
