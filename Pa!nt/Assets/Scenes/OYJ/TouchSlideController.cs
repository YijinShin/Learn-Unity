using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSlideController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform trans;
    Rigidbody2D rigid;

    private float lastTouchTime;
    private const float doubleTouchDelay = 0.5f;

    void Start()
    {
        trans = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody2D>();
        lastTouchTime = Time.time;
    }

    void Update(){
        // Debug.Log(Input.touchCount);
        // Debug.Log(Input.GetTouch(0).position);
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float kk = Input.GetAxisRaw("Horizontal");
        // trans.Translate(Vector3.right*kk*10*Time.deltaTime);
        if(Input.GetTouch(0).position.x < 400){
            trans.Translate(Vector3.right*-10*Time.deltaTime);
        }else{
            trans.Translate(Vector3.right*10*Time.deltaTime);
        }

        if(Input.GetTouch(0).deltaPosition.y> 40){
            Debug.Log("ScreenJump");
            rigid.AddForce(trans.up*2, ForceMode2D.Impulse);
        }
        // if(Input.GetButton("Jump")){
        //     rigid.AddForce(transform.up, ForceMode2D.Impulse);
        // }
        if(Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    
                    if(Time.time - lastTouchTime < doubleTouchDelay) // 더블터치 판정
                    {
                        Debug.Log("Double") ;
                    }
                    break;

                case TouchPhase.Moved:
                    Debug.Log("Moved") ;
                    break;

                case TouchPhase.Ended:
                    lastTouchTime = Time.time;
                    break;
            }
        }
        
    }
}
