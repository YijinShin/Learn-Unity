using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // inspector창에서 직접 설정 가능하도록 public
    float hAxis;
    float vAxis;
    Vector3 moveVector;

    Animator anim;
    bool wDown;

    public Camera cam;   // 카메라
    Vector3 playerDir; // 캐릭터가 움직이는 방향

    void Start()
    {
        playerDir = Vector3.zero;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //// 이동 백터 계산 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");

        Vector3 move = cam.transform.right * hAxis + cam.transform.forward * vAxis;

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }

        transform.position  +=move * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun",move != Vector3.zero); //moveVec가 0이 아니면 무조건 run
        anim.SetBool("isWalk",wDown); //moveVec가 0이 아니면 무조건 run

        //플레이어가 나가아는 방향을 바라보도록 
        transform.LookAt(transform.position + move);
    }
    
}
