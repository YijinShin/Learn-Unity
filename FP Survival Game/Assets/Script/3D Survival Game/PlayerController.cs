using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   //스피드 조정 변수
   [SerializeField] //  [SerializeField]는 인스팩터 창에서 사용가능하냐이다.
    private float walkSpeed; // private은 "다른 스크립트에서 사용가능하냐"이다. 인스팩터창 접근가능이랑은 별개임. public은 타 스크립트에서 사용가능 + 인스팩터창 사용가능임. 
    [SerializeField] 
    private float runSpeed;
    [SerializeField] 
    private float crouchSpeed;
    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    //상태변수
    private bool isRun = false;
    private bool isGround = false;
    private bool isCrouch = false; // 앉았는지 안앉았는지 

    //앉았을 때 얼마나 앉을지 결정 (숙임의 정도)
    [SerializeField]
    private float crouchPosY; // 숙임의 정도 
    private float originPosY; // 앉았다 다시 원래 값으로 돌아오기 위해 값을 저장해둠
    private float applyCrounchPosY; // applySpeed와 마찬가지로 숙임의 정도를 각각쓰는게 아니라 applyCrounchPosY에 넣어서 사용할거임    
    //땅 착지 여부 
    private CapsuleCollider capsuleCollider;

    //민감도
    [SerializeField]
    private float lookSeneitivity; // 마우스로 카메라를 돌릴때 휙휙 돌아가면 보기 힘드니까 감도값을 줘서 조정해야함. 

    //카메라 리미트 
    [SerializeField]
    private float cameraRotationLimit = 1; // 고개를 들고내릴때 각도 한계를 정해줘야함. 안그러면 360도 돌아가니까 이상하겠지. 
    private float currentCameraRotationX = 0f; //카메라 좌우회전. 일단 정면을 바라보도록 0으로 초기화. 

    //필요한 컴포넌트 
    [SerializeField]
    private Camera theCamera; //위에 Rigidbody는 SerializeField안했지만 여기서는 하는 이유가 있다. 

    private Rigidbody myRigid;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); //이렇게 넣는걸 유니티에서는 더 권장함. 더 빠르다고 .
        // 카메라가 지금 플레이어 객체의 하위로 들어와있잖아. 그래서 하이라키에있는 모든 객체를 다 뒤쳐서 Camera타입의 객체를 찾아와야하니까 이렇게 써야한다. 
        //theCamera = FindObjectOfType<Camera>();
        //근데 카메라가 한개뿐이면 괜찮은데 여러개 있을 수 있으니 이렇게 찾는것은 더 복잡하다. 그래서 SerializeField를 통해 인스팩터 창에서 직접 우리가 원하는 카메라를 선택해서 넣어주는것. 
        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        //앉는건 플레이어지만 플레이어를내리면 땅에 박혀버린다. 따라서 카메라의 y값을 바꿀거임. 
        //왜 그냥 position이 아니라 localposition 인가 : 계층이 지금 카메라가 플레이어 안에 있잖아. 즉 여기서 만약에 카메라의 y=1이라고 하면 실제로 월드 기준으로는 2이지만 플레이어 좌표의 원점기준 y=1이라는뜻. 
        //앉는 것 역시 월드 기준이 아니라 플레이어 기준이 되어야하기 때문에 localPosition을 써야함. 그냥 Position 은 월드좌표입장에서의 카메라 위치임       
        originPosY = theCamera.transform.localPosition.y; 
        applySpeed = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround(); // 땅에 있는지 먼저 확인 후 점프 해야하니까 
        TryJump();
        TryRun(); // 뛰고있는지 걷고있는지 먼저 판단 후 
        TryCrouch();
        Move(); // 움직임
        CameraRotation();
        CharacterRotation();
    }
    private void IsGround(){
        // 레이저를 쏴서 플레이어가 바닦에 붙어있는지 확인 
        //(레이저를 쏘기 시작하는 곳, 쏘는 방향, 쏠 거리 )
        isGround = Physics.Raycast(transform.position, Vector3.down,capsuleCollider.bounds.extents.y + 0.1f); 
        // transform.position는 오브젝트의 위치는 맞는데 정확하게는 그 물체 이동할때 중심부에 조그만 정육면체 있지? 그거의 위치임. 물체 중심의 위치.  
        //capsuleCollider.bounds.extents.y : 캡슐콜라이더의 영역의(bounds) > 반 사이즈(extents) > 정확히는 y값의 반(y)
        //그러니까 만약 물체가 바닦에 맞닿아있으면 해당 물체의 높이의 반에 해당하는 길이의 레이저를 쏘면 딱 바닦에 닿음. 조금이라도 떠있으면 안닿겠지 
        //근데 얘가 대각선이나 경사면에 있으면 이게 또 보기에는 닿았는데 안닿았다고 쳐질수있다. 그래서 그 오차를 커버하기위해 0.1f의 오차를 더 줌. 
    }

    private void TryJump(){
        if(Input.GetKey(KeyCode.Space)){ //달리기 시작
            if(isGround){// 땅에 있을경우(공중에있으면 이중점프가 되는거니까)
                Jump();
            } 
        }
    }
    private void Jump(){
        if(isCrouch) //앉아있다가 점프한 경우 다시 서기 
            Crouch();
        //walk에서는 myRigid.movePosition이나 moveRotation을 이용했지만 이번에는 velocity를 쓸거임.
        // velocity : myRigid가 현재 움직이는 방향+속도. 이걸 수정할거임. transform.up=방향 , jumpForce=크기
        myRigid.velocity = transform.up * jumpForce;
    }

    private void TryRun(){
        if(Input.GetKey(KeyCode.LeftShift)){ //달리기 시작
            Running();
        }        
        if(Input.GetKeyUp(KeyCode.LeftShift)){ //달리기 끝 
            RunningCancel();
        }
    }

    private void Running(){
        isRun = true; 
        applySpeed = runSpeed; //스피드 변경
    }

     private void RunningCancel(){
        isRun = false;
        applySpeed = walkSpeed; //스피드 변경
    }

    private void TryCrouch(){
        if(Input.GetKeyDown(KeyCode.LeftCommand)){
            Crouch();
        }
    }
    private void Crouch(){
        
        isCrouch = !isCrouch;
        if(isCrouch){
            applySpeed = crouchSpeed;
            applyCrounchPosY = crouchPosY;
            Debug.Log("pos : "+applyCrounchPosY);
        }
        else{
            applySpeed = walkSpeed;
            applyCrounchPosY = originPosY;
            Debug.Log("pos : "+applyCrounchPosY);
        }
        
        //y값만 수정 
        //theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrounchPosY, theCamera.transform.localPosition.z);
        //위에서 시도한 카메라의 로컬포지션을 바꾸는 것은 너무 확확 바뀐다. 그래서 사용하는 것이 코르틴.
        StartCoroutine("LogCoroutine"); // CrouchCoroutine을 시작한다. 
        Debug.Log("camera:" + theCamera.transform.position.y);
    } 
    // 보통 함수는 위에서 아래로 차례대로 실행되는데 코르틴은 동시에 다 같이 실행된다. 병렬처리. 물론 실제로 cpu상에서 병렬처리를 하는건 아니고 하나의 cpu로 
    //빠르게 왔다갔다 처리하면서 병렬로 되는것처럼 보이게 하는 것. 

    IEnumerable CrouchCoroutine(){
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        Debug.Log(applyCrounchPosY+":"+_posY);
        while(_posY != applyCrounchPosY){ //목표값에 도달하기까지 반복적으로 증감. 
            count ++;
            //근데 보관을 사용해서 증가 시키면 속도의 강약이 조절됨. 안그러면 너무 선형적으로 움직이니까..
            //보관함수=Lerp. (1,2,1) : 1에서 2까지 1의 비율로 증가한다. 이러면 한번 실행했을때 1에서 바로 2가 됨. (1,2,0.5)= 1>1.5>1.75>1.86이렇게 증가함.
            _posY = Mathf.Lerp(_posY,applyCrounchPosY,0.3f); 
            theCamera.transform.localPosition = new Vector3(0,_posY,0); //카메라의 x,z는 항상 0이니까.
            if(count>15)break;
            yield return null;//코르틴의 장점에는 대기 라는게 있다. null을 넣어주면 한프레임 대기. 
        } 
        theCamera.transform.localPosition = new Vector3(0, applyCrounchPosY,0f);
    }

    IEnumerable LogCoroutine(){
        Debug.Log("in coroutine");
        //yield return null;//코르틴의 장점에는 대기 라는게 있다. null을 넣어주면 한프레임 대기. 
        yield return new WaitForSeconds(1.0f);
    }

    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal"); //버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)
        float _moveDirZ = Input.GetAxisRaw("Vertical"); //버튼 누르면 1,-1,0중 하나가 리턴됨. (왼:1,오:-1,안누름:0)

        Vector3 _moveHorizontal = transform.right * _moveDirX; //  transform : 본 컴포넌트가 가지고있는 위치값. 이 속성의 Right를 쓰겠다는 것.
        //그러면 Vector3의 기본값으로 (1,0,0)이 들어가있는데,(지금 오브젝트가 ) 거기에 _moveDirX를 곱한다. 그럼(1,0,0),(-1,0,0),(0,0,0) 셋중하나가 결과가 됨. 결과에 따라 오른쪽, 왼쪽 중 하나로 감. 
        Vector3 _moveVertical = transform.forward * _moveDirZ; //  transform : 본 컴포넌트가 가지고있는 위치값. 이 속성의 forward를 쓰겠다는 것.
        //그러면 Vector3의 기본값으로 (0,0,1)이 들어가있는데, 거기에 _moveDirZ를 곱한다. 그럼(0,0,1),(0,0,-1),(0,0,0) 셋중하나가 결과가 됨. 결과에 따라 앞, 뒤 중 하나로 감.

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; //speed : 이동속도
        //(1,0,0) + (0,0,1) = (1,0,1)이고, 각 숫자의 합이 2가됨
        //normalize를 해주면 (0.5,0,0.5)가 되면서 각 숫자의 합이 1이 됨
        //어짜피 (1,0,1)이나 (0.5,0,0.5)의 방향은 같음.(그래프 그려보면 둘다 1사분면 45도 대각선방향임)이렇게 하는 이유는 유니티에서 권장하기 때문. 아마 내부 알고리즘 적으로 더 빨라서 그러는 듯. 
        //우리 입장에서도 1초에 얼마나 움직이게할지 계산하는게 편함.     

        //myRigid.MovePosition(transform.position + _velocity ) // transform(현위치) + _velocity(이동백터) 
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); // 위와 같이 하면 걍 바로 순간이동하겠지. 그래서 _velocity를 deltaTime만큼 쪼개주는 것. 
        //Time.deltaTime : Update함수는 1초에 약 60번 실행됨. Time.deltaTime을 곱해주면 "1초동안 _velocity 만큼 움직이겠다"는 것이됨. 한번에 확이동하는것이 아니라 이동백터를 약60으로 쪼개서 매 프레임 마다 더하겠다는 것. 

    }

    private void CameraRotation(){ //player가 고개들 내리거나 든다.(카메라 로테이션)
        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스이동값. 마우스는 2차원이니까 x,y뿐임. 마우스로 위아래로 고개 들었다 내리기.
        float _cameraRotationX = _xRotation * lookSeneitivity; //마우스 이동에 따른 실제 카메라 회전시킬 값
        currentCameraRotationX -= _cameraRotationX; // 현제 카메라 값 + 계산한 값 = 최종 카메라 회전 각도 
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // 우리가 설정한 리미트 안에 가두기. (가둘값, -범위,+범위) 범위를 넘으면 최대값으로 고정. 

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX,0f,0f); //실제 카메라 회전시키기 (x,y,z)니까 y,z는 가만히 있고 x만 변함
    }

    private void CharacterRotation(){ //player 회전 > y기준 rotateion
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSeneitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 자기자신의 현재 회전값 * 회전시킬 백터값을 쿼터니온으로 변환한 값. MoveRotation은 MovePosition과는 다르게 백터3가 아니라 쿼터니온을 인자값으로 받음
        //Debug.Log(myRigid.rotation); // vec3는 3원소
        //Debug.Log(myRigid.rotation.eulerAngles); //쿼터니온은 4원소이다. 
    }
}
