using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CameraMove newCam; // CameraMove 스크립트에서 활성화되어있는 카메라 가져올 때 사용함
    Camera enabledCam; // 활성화된 카메라
    public float rotateSpeed;
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;
    public GameManager manager;
    
    void Awake()
    {
        newCam = GetComponent<CameraMove>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Turn();
    }

    void FixedUpdate()
    {
        RaycastHit rayHit;
        float hitDistance = 5.0f;

        Debug.DrawRay(transform.position, transform.forward * hitDistance, new Color(1, 0, 0)); // DrawRay() : 에디터 상에서만 Ray를 그려주는 함수

        if(Physics.Raycast(transform.position, transform.forward * hitDistance, out rayHit, hitDistance, LayerMask.GetMask("Cube")))
        {
            GameObject hitTarget = rayHit.collider.gameObject;
            if(rayHit.distance < hitDistance)
            {
                Debug.Log(hitTarget.name);
                if(Input.GetKeyDown("z"))
                {
                    manager.Action(hitTarget);
                    manager.menuAction();
                }
            }
        }
    }

    void Move()
    {   
        enabledCam = newCam.arrCam[newCam.nNowCam];
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 forward = (enabledCam.transform.localRotation * Vector3.forward).normalized;
        Vector3 right = (enabledCam.transform.localRotation * Vector3.right).normalized;

        forward.y = 0;
        right.y = 0;

        if(newCam.nNowCam == 0)
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 메인 카메라 (탑뷰)일때만
        }

        else
        {
            moveVec = hAxis*right + vAxis*forward; // 카메라의 시점에 맞게 캐릭터 이동
        }
        transform.position += moveVec * speed * Time.deltaTime;
        
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }
    void Turn() // 캐릭터 천천히 회전
    {
        if(hAxis == 0 && vAxis == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(moveVec);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }
}
