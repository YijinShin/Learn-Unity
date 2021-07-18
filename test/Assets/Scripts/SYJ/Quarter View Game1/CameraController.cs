using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target; //카메라가 따라가야할 타겟
    [SerializeField]
    private Vector3 offset; // 보정값 
    [SerializeField]
    private Vector3 rotateValue;
    void Update()
    {    
        transform.position = target.position; //+ offset;
    }
}
