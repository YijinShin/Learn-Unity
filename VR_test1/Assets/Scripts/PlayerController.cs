using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 속도 변수
    [SerializeField] private float walkSpeed; // 걷는 속도 변수 
    [SerializeField] private float runSpeed; // 뛰는 속도 변수 
    [SerializeField] private float applySpeed; //  실제 적용하는 변수 
    
    // 카메라 리미트
    [SerializeField] private float cameraRotationLimit = 1; // 고개를 들고내릴때 각도 한계를 정해줘야함. 안그러면 360도 돌아가니까 이상하겠지. 
    [SerializeField] private float currentCameraRotationX = 0f;  // 카메라 좌우회전. 일단 정면을 바라보도록 0으로 초기화. 
    
    // 필요한 컴포넌트
    [SerializeField] private Camera theCamera; // 플레이어 카메라 
    [SerializeField] private float lookSensitivity;// 민감도 
    private Rigidbody myRigid; 
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); //이렇게 넣는걸 유니티에서는 더 권장함. 더 빠르다고 .
        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
        
    }

    
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }
    
    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal"); // 버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)
        float _moveDirZ = Input.GetAxisRaw("Vertical"); // 버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)
        Vector3 _moveHorizontal = transform.right * _moveDirX; // transform : 본 컴포넌트가 가지고있는 위치값. 이 속성의 Right를 쓰겠다는 것.
        Vector3 _moveVertical = transform.forward * _moveDirZ; // transform : 본 컴포넌트가 가지고있는 위치값. 이 속성의 forward를 쓰겠다는 것.
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; //speed : 이동속도  
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); // 위와 같이 하면 걍 바로 순간이동하겠지. 그래서 _velocity를 deltaTime만큼 쪼개주는 것. 
    }

    private void CameraRotation(){ //player가 고개들 내리거나 든다.
        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스이동값. 마우스는 2차원이니까 x,y뿐임. 마우스로 위아래로 고개 들었다 내리기.
        float _cameraRotationX = _xRotation * lookSensitivity; //마우스 이동에 따른 실제 카메라 회전시킬 값
        currentCameraRotationX -= _cameraRotationX; // 현제 카메라 값 + 계산한 값 = 최종 카메라 회전 각도 
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // 우리가 설정한 리미트 안에 가두기. (가둘값, -범위,+범위) 범위를 넘으면 최대값으로 고정. 
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX,0f,0f); //실제 카메라 회전시키기 (x,y,z)니까 y,z는 가만히 있고 x만 변함
    }

    private void CharacterRotation(){ //player 회전 
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 자기자신의 현재 회전값 * 회전시킬 백터값을 쿼터니온으로 변환한 값. MoveRotation은 MovePosition과는 다르게 백터3가 아니라 쿼터니온을 인자값으로 받음
    }

}
