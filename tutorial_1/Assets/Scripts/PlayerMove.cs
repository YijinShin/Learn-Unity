using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {   
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator >();
        
    }

    // Update is called once per frame
    /*FixedUpdate()는 1초에 50번 실행된다. 
    그러면 addForce를 통해서 좌우 움직임을 준다고 할 때, 키를 꾹 누르면 addForce를 50번이나 
    하게 되면서 sprite가 휙 날라감(?) 그래서 최대 속력을 줘야함.
    */
    void FixedUpdate()
    {   
        //Move by key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);
        
        //rigid.velocity = sprite의 속도
        if(rigid.velocity.x > maxSpeed) //최대 스피드보다 더 커지면 // right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //최대값으로 잡아주기

        else if(rigid.velocity.x < maxSpeed*(-1)) //최대 스피드보다 더 커지면 // left max speed
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y); //최대값으로 잡아주기

        //landing platForm
        //rayHit = 빔을 쏴서 맞은 오브젝트의 정보
        if(rigid.velocity.y < 0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0)); //초록색 레이 쏘기. 디버그니까 실제 화면에서는 안보이고 에디터 창에서만 보인다. 
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider != null){
                if(rayHit.distance < 0.5f){
                    //Debug.Log(rayHit.collider.name); //로그에 ray가 닿은 오브젝트 이름 띄우기 
                    anim.SetBool("isJump", false);
                }
            }
        }

    }
    /* 직접적인 키 입력은 fixedupdated에서 하는 것이 좋지만 단발적인 키 입력은 일반적인 update에서 하는 것이 좋다. 
        지금 하려는것은 키에서 손 때면 바로 급격하게 멈추도록 하는 것. 
    */
    private void Update(){
        
        //stop sprite
        if(Input.GetButtonUp("Horizontal")){//버튼에서 손 때면 
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y); //속력 줄이기
        } //normalized : 백터의 크기를 1로 만듬. (velocity.normalized하면 백터가 반환됨)
        
        //방향전환(direction sprite)
        if(Input.GetButtonDown("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        
        //animation
        if(Mathf.Abs( rigid.velocity.x )< 0.3) // 횡 이동 단위값이 0이면(즉 이동을 멈추면)  //Mathf : 수학함수 제공, Abs: 절댓값
            anim.SetBool("isWalk", false); //isWalk가 bool이니까 SetBool이고, 멈추면 false가 되어야하니까 false를 넣음 
        else  
            anim.SetBool("isWalk", true); //isWalk가 bool이니까 SetBool이고, 멈추면 false가 되어야하니까 false를 넣음 

        //jump
        if(Input.GetButtonDown("Jump") && !anim.GetBool("isJump")){ //무한 점프를 막기 위해 혹시 우리가 점프하고있는 상태인지도 확인
            rigid.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
            anim.SetBool("isJump", true); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // 피격 이벤트 
        if(collision.gameObject.tag == "enemy"){
            OnDamaged(collision.transform.position);
        }

    }

    //무적시간 돌입 함수 
    void OnDamaged(Vector2 targetPosition){
        gameObject.layer = 9; // playerDamaged layer가 9번임. 
        //반투명
        spriteRenderer.color = new Color(1,1,1,0.4f);
        //튕기기
        int dirc = transform.position.x - targetPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1) * 7, ForceMode2D.Impulse);

        //animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 1);
    }   

    //무적 푸는 함수
    void OffDamaged(){
        gameObject.layer = 8; // player layer가 9번임. 
         spriteRenderer.color = new Color(1,1,1,1);
    }
}
