using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBall6 : MonoBehaviour
{
    // 목표 : 물리 충돌 이벤트 만들기 

    //오브젝트의 재질 접근을 위한 변수
    MeshRenderer mesh;
    Material mat;

    //변수 초기화 
    void Start()
    {    
        mesh = GetComponent<MeshRenderer>();
        mat = GetComponent<Material>();
    }

    //물리 충돌 시작할 때 호출
    private void OnCollisionEnter(Collision collision)
    {
        mat.color  = new Color(0,0,0);//black
    }
    //물리 충돌 중
    private void OnCollisionStay(Collision collision)
    {
        
    }
    //물리 충돌이 끝났을 때
    private void OnCollisionExit(Collision collision)
    {
        
    }
}
