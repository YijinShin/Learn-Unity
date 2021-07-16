using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script1 : MonoBehaviour // Script1 class가 monobehavior를 상속함. (monobehavior : unityengine에 있음)
{
    //변수 
        //변수를 인스팩터 뷰에서 보이게 하는 경우 > public도 좋지마나 SerializeField를 사용해서 노출시키는 것을 추천하심 
        //Start나 Awake에서 초기화 하는 것을 권장
    [SerializeField]
    int myValue;

    [SerializeField]
    int MyValue2;

    [SerializeField]
    int m_myValue3;

    // Start is called before the first frame update ( 첫번째 프레임 업뎃 이전에 실행됨 )
    void Start()
    {
        Debug.Log("Start");
        //vector3 : 3차원공간의 점. mesh : 점으로 이루어진 도형, texture : mesh에 입히는 이미지, material : gpu가 렌더링하기 위한 정보와 프로그램, shader :gpu에서 렌더링 할떄 색을 표현하기 위한 프로그램
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
