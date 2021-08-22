using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateController : MonoBehaviour
{

    private Vector3 rotatePos;
    [SerializeField] private Transform target; //카메라가 따라가야할 타겟
    [SerializeField] private int rotateSpeed;
    [SerializeField] private Vector3 offset;
        void Start()
    {
        rotatePos = new Vector3(target.position.x, target.position.y + offset.y, target.position.z + offset.z );
    }

    
    void Update()
    {
        //Rotate();
    }

    private void Rotate(){
        if(Input.GetMouseButton(1)){
            rotatePos = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up) * rotatePos;
            transform.position = target.position + rotatePos; 
            transform.LookAt(target.position);
        }
    }
}
