using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 담당하는 헤더 

public class CameraController : MonoBehaviour
{
    private Camera theCam; 
    [SerializeField] private Transform target; //카메라가 따라가야할 타겟
    [SerializeField] private Vector3 offset; // 보정값 
    [SerializeField] private float rotateSpeed; // 회전 속도 
    [SerializeField] private float zoomSpeed; // 줌 속도
    public Vector3 originOffset;
    private Vector3 originPos;
    private Vector3 originRot;
    private Vector3 rotatePos;

    //상태 변수 
    private bool isZoomIn = false;

    void Start(){
        originOffset = offset;
        originPos = transform.position;
        originRot = transform.rotation.eulerAngles;

        rotatePos = new Vector3(target.position.x, target.position.y + originOffset.y, target.position.z + originOffset.z);
        theCam = GetComponent<Camera>();
    }

    void Update()
    {  
        Move();
        //ZoomWheel();      
        //Rotate();
        Rotate2();
        OriginPos();
    }

    private void Move(){
        theCam.transform.position = target.position + offset;
        transform.LookAt(target); // 대상을 보도록 결정 
        //Vector3 movePos = new Vector3 (transform.position.x - target.position.x, 0f, transform.position.z - target.position.z);
        //theCam.transform.position += movePos;
    }

    private void ZoomButton(){
        if(Input.GetKeyDown(KeyCode.Q)){ //Q를 누르면 zoom 전환
            if(isZoomIn){ // zoom in 상태인 경우 zoom out을 한다 
                isZoomIn = false;
                offset = originOffset; // 보정값 원래대로 돌려놓기 
            }
            else{ // zoom out 상태인 경우 zoom in을 한다 
                isZoomIn = true;
                offset = offset - new Vector3(4,4,0);
            }
        }
    }

    private void ZoomWheel(){
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if(distance != 0)
        {
            theCam.fieldOfView += distance;
        }
    }

    private void OriginPos(){
        if(Input.GetKeyDown(KeyCode.E)){
            transform.position = originPos;
            transform.rotation = Quaternion.Euler(originRot);
        }
    }

    private void Rotate(){
        if(Input.GetMouseButton(1)){
            Vector3 camRot = transform.rotation.eulerAngles; // 현재 카메라 각도
            camRot.y += Input.GetAxis("Mouse X") * rotateSpeed;
            camRot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed;
            Quaternion q = Quaternion.Euler(camRot);
            q.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);
        }
    }

    private void Rotate2(){
        if(Input.GetMouseButton(1)){
            rotatePos = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up) * rotatePos;
            transform.position = target.position + rotatePos; 
            transform.LookAt(target.position);
        }
    }
}
