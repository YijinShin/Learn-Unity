using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOYJ : MonoBehaviour
{
    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    // 상태 변수
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    // 앉았을 때 얼마나 앉을지 결정하는 변수.
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider;

    //민감도
    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0; 
    private float currentCameraRotationZ = 0; 

    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        // originPosY = transform.position.y; 이것 같은 경우네는 캐릭터가 앉게 되어서 땅바닥 밑으로 내려갈 수 있음. 그래서 카메라를 변경할 예정
        originPosY = theCamera.transform.localPosition.y; //localPosition은 부모의 위치에서 하는 것임.
        applyCrouchPosY = originPosY;
        applySpeed = walkSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        // CameraRotation();
        CharacterRotation();
    }

    //앉기 시도
    private void TryCrouch(){
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            Crouch();
        }
    }

    //앉기
    private void Crouch(){
        isCrouch = !isCrouch;
        if(isCrouch){
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else{
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        // theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrouchPosY, theCamera.transform.localPosition.z);
        StartCoroutine(CrouchCoroutine());

    }

    //앉는 부분을 조금 더 자연스럽게 안기위해서 사용하는 문법(Coroutine)
    IEnumerator CrouchCoroutine(){
        float _posY = theCamera.transform.localPosition.y;
        int count =0; // 이 변수를 넣어주는 이유는 아래 Lerp가 정확히 목표 지점까지 가는게 아니라 계속해서 낮아지거나 높아지기 때문에 어느 정도 선에 갔을 때는 목표지점으로 이동시키는 것이다.
        while(_posY != applyCrouchPosY){
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if(count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);

    }

    //움직임
    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    //점프 시도
    private void TryJump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGround){
            Jump();
        }
    }

    //캐릭터 회전
    private void CharacterRotation(){
        if(Input.GetButton("Fire1")){
            float _yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f)*lookSensitivity;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        }
        
           
    }

    //카메라 회전
    private void CameraRotation(){
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    
    //뛰기
    private void Running(){
        if(isCrouch){
            Crouch();
        }
        isRun = true;
        applySpeed = runSpeed;
    }

    //뛰기 취소
    private void RunningCancel(){
        isRun = false;
        applySpeed = walkSpeed;
    }

    //뛰기 시도
    private void TryRun(){
        if(Input.GetKey(KeyCode.LeftShift)){
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            RunningCancel();
        }
    }

    //점프
    private void Jump(){
        if(isCrouch){
            Crouch();
        }
        myRigid.velocity = transform.up * jumpForce;
    }

    //땅에 있는지 확인
    private void IsGround(){
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        Debug.Log(isGround);
        //extents는 반값임.
    }
}
