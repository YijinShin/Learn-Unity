using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range; // gun 사정거리
    public float accuracy; //정확도
    public float fireRate; // 연사속도
    public float reloadTime; // 장전속도

    public int damage; // 데미지 

    public int reloadBulletCount; // 총알 재장전 개수 
    public int currentBulletCount; // 현재 탄창에 총알 개수 
    public int maxBulletCount; // 최대 소유 가능한 총알 개수 
    public int carrayBulletCount; //현재 소유하고 있는 총알의 개수(인벤에 가지고있는)

    public float retroActionForce; //반동 세기
    public float retroActionFineSightForce; //정조준 시 세기
    public Vector3 fineSightOriginPos; // 정조준시 위치
    
    public Animator anim;
    public ParticleSystem muzzleFlash; //화염 이팩트
    public AudioClip fireSound; // 총 발사 소리 
}
