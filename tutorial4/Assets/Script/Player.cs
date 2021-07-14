using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // inspector창에서 직접 설정 가능하도록 public

    float hAxis;
    float vAxis;
    Vector3 moveVector;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //화살표 키보드 입력받아옴. edit > project setting > input manager에서 관리 
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //이동 백터 계산 (normalized : 대각선일 때 더 빠르게 움직이는 것을 막기 위해 방향값을 1로 보정하기)
        moveVector = new Vector3(hAxis,0,vAxis).normalized;
        //transform 
        transform.position += moveVector * speed * Time.deltaTime;
    }
}
