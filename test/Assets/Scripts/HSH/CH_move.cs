using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_move : MonoBehaviour
{
    public float Walk_speed;
    public float Rotation_speed;

    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode Right;
    public KeyCode Left;
    
    private Transform transform;
    private Rigidbody rigidbody;

    private bool forward;
    private bool right;
    private bool InCheck;

    private Vector3 Move;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = this.GetComponent<Transform>();
        rigidbody = this.GetComponent<Rigidbody>();
        right=false;
        forward = false;
        InCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    }
    void Move_CH(){
        Move = Move.normalized * Walk_speed * Time.deltaTime;
        Debug.Log(Move.x);
        rigidbody.MovePosition(transform.position+Move);
    }
    void Turn_CH(){
        if(Move.x==0&&Move.z==0) 
            return; 
        Quaternion newRotation = Quaternion.LookRotation(Move);
       // Debug.Log(newRotation);
        //rigidbody.MoveRotation(newRotation);
       rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotation,Rotation_speed * Time.deltaTime);
    }
}
