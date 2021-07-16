using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_flow : MonoBehaviour
{
    public Transform target; //카메라가 따라가야할 타겟
    public Vector3 offset; // 보정값 
    private Vector3 rotateValue;

    void Update()
    {    
        transform.position = target.position + offset;
    }
}
