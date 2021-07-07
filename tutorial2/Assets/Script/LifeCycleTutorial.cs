using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleTutorial : MonoBehaviour
{   
    //초기화 영역
    //스크립트가 최초 실행될때 한번 딱 실행되는 함수 
    void Awake()
    {
        //플레이어 데이터 읽어오기
    }
    //오브젝트 활성화(최초 1회 실행이 아닌 켜고 끌때마다 실행됨) 위치는 awake와 start사이.
    void OnEnable()
    {
        //플레이어 로그인
    }
    //업데이트 시작 직전, 최초 실행. 딱 한번 실행됨
    void Start()
    {
        //무기 준비
    }
    //물리연산영역
    //물리연산 업데이트. 1초에 50번 돈다. 컴퓨터 사양과 관련없이 고정된 실행주기로 실행함. cpu 많이 사용
    void FixedUpdate()
    {
        //이동
    }
    //게임 로직 업데이트 (물리연산 제외). 주기적으로 업데이트 해야하는 것들. 환경에 따라 실행주기가 떨어질수있음. 
    //근데 FixedUpdate보다 많이 실행되기도함. 60번정도. 이건 환경에 따라 달라짐
    void Update()
    {
        //ex: 몬스터 사냥
        //키를 직접 지정하여 설정
            //키보드 입력
            if(Input.anyKeyDown) // 누르는 이벤트 한번에 반응함
                //Debug.Log("any key down");
            if(Input.anyKey) // 누르고 있는 상태면 실행됨
                //Debug.Log("any key");
            if(Input.GetKeyDown(KeyCode.Return)) // 특정 키 누르는 이벤트 (return:엔터)
                Debug.Log("GetKeyDown_Return");
            if(Input.GetKey(KeyCode.LeftArrow)) // 특정 키가 눌리고있는 중이면 
                //Debug.Log("GetKey_LeftArrow");
            if(Input.GetKeyUp(KeyCode.RightArrow)) // 특정 키 누르고 손 땔때
                //Debug.Log("GetKeyUp_RightArrow");
            //키보드 입력 
            if(Input.GetMouseButtonDown(0)) // 특정 키 누르는 이벤트 (0:좌클릭. 1:우클릭)
                Debug.Log("GetMouseButtonDown_0");
            if(Input.GetMouseButton(0)) // 특정 키가 눌리고있는 중이면 
                Debug.Log("GetMouseButton_0");
            if(Input.GetMouseButtonUp(0)) // 특정 키 누르고 손 땔때
                Debug.Log("GetMouseButtonUp_0");
        //버튼방식
            //project setting>input manager에서 각 동작당 키 지정
            //Jump를 space키로 지정하고 한번 사용해볼거임.
            if(Input.GetButton("Jump")) 
                Debug.Log("GetButton_Jump!");
            if(Input.GetButtonUp("Jump")) 
                Debug.Log("GetButtonUp_Jump!!!!");
        //축 이동(버튼설정에서 right, lefr로 함). 축 이동은 값이 필요함
            if(Input.GetButton("Horizontal")) //y는 Vertical 사용
                //가중치 존재
                Debug.Log("GetButton_Horizontal:"+Input.GetAxis("Horizontal"));
                //가중치 없이 그냥 1씩 
                Debug.Log("GetButton_HorizontalRaw:"+Input.GetAxisRaw("Horizontal"));
    }
    //모든 업데이트가 끝난 후 마지막으로 실행되는 업데이트(카메라가 따라간다던가 경험치 획득 등 후처리)
    void LateUpdate()
    {
        //경험치 획득 
    }
    //오브젝트 비활성화. 위치는 모든 업데이트가 끝난 시점과 destroy사이.
    void OnDisable()
    {
        //플레이어의 로그아웃
    }
    //게임 오브젝트 삭제(뭔가 남기고 삭제됨)
    void OnDestroy()
    {
        //플레이어 데이터 해제(Awake랑 비슷)
    }

}

