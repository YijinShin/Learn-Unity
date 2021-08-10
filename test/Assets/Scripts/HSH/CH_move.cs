using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_move : MonoBehaviour
{
    public int Walk_speed;
    public float Rotation_speed;
    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode Right;
    public KeyCode Left;
    public bool Is_Working;


    private Animator animator;
    private Transform trans;
    private Rigidbody rigid;


//sit
    public KeyCode SitKey;
    public BoxCollider box;
    public CapsuleCollider capsule;
    public bool Sitting;
    public bool InputSit;
   public float delaytimer = 0f;
    public float delaytime = 0.1f;

    public float Stand;
    public float Sit_H;
    private bool forward;
    private bool right;
    public bool InCheck;
    private Vector3 Move;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        trans = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody>();
        right=false;
        forward = false;
        InCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Walk_speed= dd.GetComponent<DisplayData>().meditation1/10;
        Key_Get();
    }
    void FixedUpdate()
    {
        Move_CH();
        Turn_CH();
    }
    void Key_Get() {
        forward= false;
        right = false;
        InputSit = false;
        InCheck = false;
        Move = new Vector3 (0,0,0);
        if (Input.GetKey(Forward))
        {
            Move = new Vector3 (Move.x,Move.y,Move.z+1);
            InCheck = true;
            forward = true;
        }
        if (Input.GetKey(Left))
        {
            Move = new Vector3 (Move.x-1,Move.y,Move.z);
            InCheck = true;
            right = false;
        }
        if (Input.GetKey(Back))
        {
            Move = new Vector3 (Move.x,Move.y,Move.z-1);
            InCheck = true;
            forward = false;
        }
        if (Input.GetKey(Right))
        {
            Move = new Vector3 (Move.x+1,Move.y,Move.z);
            InCheck = true;
            right = true;
        }

        if (Input.GetKeyDown(SitKey))
        {
            InputSit = true;
            Sit(Sitting);
        }
        if (InCheck && Sitting) {
            Sit(Sitting);
        }

    }
    void Sit(bool sit) {
        Sitting = !Sitting;
        if (!sit)
        {
            capsule.enabled = false;
            trans.position = new Vector3(trans.position.x, Sit_H, trans.position.z);
            return;
        }
        else if (sit)
        {
            capsule.enabled = true;
            trans.position = new Vector3(trans.position.x, Stand, trans.position.z);
            return;
        }
        
    }


    void Move_CH(){

        animator.SetBool("Is_Working", InCheck);
        Move = Move.normalized * Walk_speed * Time.deltaTime;
        //Debug.Log(Move.x);
        rigid.MovePosition(transform.position+Move);
    }
    void Turn_CH(){
        if(!InCheck||(Move.x==0&&Move.z==0)) {
            rigid.constraints=RigidbodyConstraints.FreezeRotation;
            return; 

        }
        rigid.constraints=RigidbodyConstraints.None;
        rigid.constraints=RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezeRotationZ;
        
        Quaternion newRotation = Quaternion.LookRotation(Move);
        // Debug.Log(newRotation);
            //rigid.MoveRotation(newRotation);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation,Rotation_speed * Time.deltaTime);
    }
}
