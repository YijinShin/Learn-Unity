using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Ray : MonoBehaviour
{
    [SerializeField]
    private Transform CH;
    [SerializeField]
    private float dis;
    bool pose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C)){
            pose=true;
        }
        RaycastHit hit;
        Vector3 rayDir = CH.transform.forward;
        Debug.DrawRay(CH.transform.position,rayDir*dis,Color.red);
        if(Input.GetKey(KeyCode.Z)){
            if(Physics.Raycast(CH.transform.position,rayDir,out hit,dis)){
                Debug.Log("광선에 맞았다!!");
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
    
}
