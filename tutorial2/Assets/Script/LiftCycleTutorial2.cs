using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCycleTutorial2 : MonoBehaviour
{
    Vector3 target = new Vector3(8,1.5f,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        // 목표 지점까지 이동 
        //Time.deltaTime : Time.deltaTime값은 프레임이 적으면 크고, 프레임이 많으면 작아짐. 프레임이 크든 작든 나오는 값이 동일해지도록 Time.deltaTime을 꼭 곱해줘야한다.
        // 1. MoveToward : 등속이동(현위치, 목표위치, 속도). 위치는 vec3
            //transform.position = Vector3.MoveTowards(transform.position, target, 1f);
        // 2. SmoothDamp : 부드러운 감속이동(현위치, 목표위치, 참조속도, 속도). 속도에 반비례하여 속도가 증가함.
            //Vector3 velo = Vector3.zero;
            /*Vector3 velo = Vector3.up*10;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);*/
        // 3. Lerp : 선형보간, smoothdamp보다 감속시간이 길다. 마지막 변수에 비례하여 속도 증가.(최대값1)
            //transform.position = Vector3.Lerp(transform.position, target, 0.1f);
        // 4. SLerp : 구면 선형 보간, 호를 그리며 이동
            transform.position = Vector3.Slerp(transform.position*Time.deltaTime, target*Time.deltaTime, 0.05f);
        
    }
}
