using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
   //스피드 조정 변수
   [SerializeField] //  [SerializeField]는 인스팩터 창에서 사용가능하냐이다.
    private float walkSpeed; // private은 "다른 스크립트에서 사용가능하냐"이다. 인스팩터창 접근가능이랑은 별개임. public은 타 스크립트에서 사용가능 + 인스팩터창 사용가능임. 
    [SerializeField] 
    private float runSpeed;
    [SerializeField] 
    private float applySpeed;

    //상태변수
    private bool isRun = false;

    //필요한 컴포넌트 
    [SerializeField]
    private Camera eyeCamera; //위에 Rigidbody는 SerializeField안했지만 여기서는 하는 이유가 있다. 

        //민감도
    [SerializeField]
    private float lookSeneitivity; // 마우스로 카메라를 돌릴때 휙휙 돌아가면 보기 힘드니까 감도값을 줘서 조정해야함. 

    //카메라 리미트 
    [SerializeField]
    private float cameraRotationLimit = 1; // 고개를 들고내릴때 각도 한계를 정해줘야함. 안그러면 360도 돌아가니까 이상하겠지. 
    private float currentCameraRotationX = 0f; //카메라 좌우회전. 일단 정면을 바라보도록 0으로 초기화. 

    private Rigidbody myRigid;    
    private CapsuleCollider capsuleCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); //이렇게 넣는걸 유니티에서는 더 권장함. 더 빠르다고 .
        // 카메라가 지금 플레이어 객체의 하위로 들어와있잖아. 그래서 하이라키에있는 모든 객체를 다 뒤쳐서 Camera타입의 객체를 찾아와야하니까 이렇게 써야한다. 
        //eyeCamera = FindObjectOfType<Camera>();
        //근데 카메라가 한개뿐이면 괜찮은데 여러개 있을 수 있으니 이렇게 찾는것은 더 복잡하다. 그래서 SerializeField를 통해 인스팩터 창에서 직접 우리가 원하는 카메라를 선택해서 넣어주는것. 
        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 움직임
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

}
