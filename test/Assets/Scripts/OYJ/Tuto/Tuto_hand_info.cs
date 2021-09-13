using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_hand_info : MonoBehaviour
{
    public Tuto_GTM manager;
    private RaycastHit hitInfo;

    [SerializeField]
    private int range;
    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo,range)){
            // Debug.Log(hitInfo.transform.name);
            manager.Action(hitInfo);
            Debug.DrawRay(transform.position, transform.forward*range, Color.blue, 1f);
        }      
    }
}
