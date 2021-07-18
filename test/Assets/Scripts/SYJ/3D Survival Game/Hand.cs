using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string handName; // 손 이름. (맨손, 총든손 등)
    public float range; // 공격범위
    public int damage; // 공격력
    public float workSpeed; // 작업속도
    public float attacDelay;//공격 딜레이
    public float attacDelayA;//공격 활성화 시점(주먹을 완전히 뻗은 다음에 데미지가 들어가니까. 주먹이 나가는데 시간이 있음)
    public float attacDelayB;//공격 비활성화 시점(주먹을 빼는데 걸리는 시간)

    public Animator anim; //에니메이션 
    
}
