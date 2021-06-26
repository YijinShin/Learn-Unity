using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{   

    //변수 선언
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;



    void Awake() // 변수 초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        Think(); 

        Invoke("Think", 5); // 주어진 시간이 지난 뒤 지정된 함수를 실행함. 그냥 재귀로 부르면 과부화걸림 
    }

    void FixedUpdate()
    {   
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y); // 왼쪽이니까 -1

        //지형 체크(낭떨어지 인지하기)
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position. y); //자기 자신 위치 + next move = 다음 수 
        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0)); //자기 한수 앞에 ray 쏘기 
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null){
            Turn(); 
        }
        
    }

    //행동지표를 바꿔주는 함수
    void Think(){

        //렌덤으로 좌우로 움직임.
        nextMove = Random.Range(-1, 2); // 최저값은 렌덤값에 포함이 되는데 최대값은 포함이 안됨. 그래서 2를 넣어야함. 
        //sprite animation
        anim.SetInteger("walkSpeed", nextMove);
        //좌우 반전 
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
        //2~5초 마다 한번씩 생각함.
        float nextThinkTime = Random.Range(2f,5f);
        Invoke("Think", nextThinkTime); //재귀

    }

    void Turn(){
        nextMove *= -1; 
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke(); // 우리가 바꿔줬잖아. 그러니까 자동 실행되는 Invoke를 잠깐 멈추는 것. 안그러면 타이밍 때문에 파밧하고 2번 연속 바뀔 수 있다. 
        Invoke("Think", 5); 
    }
}
