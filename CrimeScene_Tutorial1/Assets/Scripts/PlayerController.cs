using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //스피드 조정 변수
    [SerializeField] private float walkSpeed; //기본 스피드 
    [SerializeField] private float runSpeed; // 뛰는 스피드 
    private float applySpeed;
    
    //민감도
    [SerializeField]
    private float lookSeneitivity; // 마우스로 카메라를 돌릴때 휙휙 돌아가면 보기 힘드니까 감도값을 줘서 조정해야함. 

    //필요한 컴포넌트 
    [SerializeField] private Camera theCamera; 
    private CapsuleCollider capsuleCollider;
    private Rigidbody myRigid;
    [SerializeField] private GameObject lookTarget; //카메라 회전시 플레이어가 바라보는 게임 오브젝트

    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); 
        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.canPlayerMove){
            Move(); // 움직임
        }
    }

    private void Move(){

        float _moveDirX = Input.GetAxisRaw("Horizontal"); //버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)
        float _moveDirZ = Input.GetAxisRaw("Vertical"); //버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)
        
        //카메라 기준 이동
        Vector3 _moveHorizontal_cam = lookTarget.transform.right * _moveDirX; 
        Vector3 _moveVertical_cam = lookTarget.transform.forward * _moveDirZ; 
        Vector3 _velocity_cam = (_moveHorizontal_cam + _moveVertical_cam).normalized * walkSpeed;
        myRigid.MovePosition(transform.position + _velocity_cam * Time.deltaTime); // _velocity를 deltaTime만큼 쪼개주는 것. 
        
        //플레이어가 나가아는 방향을 바라보도록 
        transform.LookAt(transform.position + _velocity_cam);

        //자꾸 rotation.x가 기울어져서 0으로 고정시킴.. 
    }
}
