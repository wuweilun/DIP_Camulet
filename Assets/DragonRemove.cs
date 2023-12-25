using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DragonRemove : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public string selectableTag = "dragon";
    public Camera raycastCamera;
    // private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.CompareTag(selectableTag))
                {
                    Debug.Log("Raycast hit dragon");
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("Raucast doesn't hit dragon");
                }
            } 
        }
    }
}
