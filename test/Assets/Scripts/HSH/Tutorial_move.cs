using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_move : MonoBehaviour
{
    private Transform Camera_Transform;
    private Transform transform;
    private Vector3 V3Direction;
    private Rigidbody rigid;
    private bool move;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float Rotation_speed;

    // Start is called before the first frame update
    void Start()
    {
        Camera_Transform = GameObject.Find("Camera Arm").GetComponent<Transform>();
        transform = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Get_Direction();
    }
    private void FixedUpdate()
    {
        PlayerTurn();
        Move();
        
        
    }
    void Get_Direction()
    {
        move = false;
        Vector3 heading = Camera_Transform.localRotation * Vector3.forward;
        heading.y = 0;
        heading = heading.normalized;
        if(Input.GetAxis("Vertical")!=0||Input.GetAxis("Horizontal")!=0){
            move = true;
            V3Direction = heading * Time.deltaTime * Input.GetAxis("Vertical") * speed;
            V3Direction += Quaternion.Euler(0,90,0)*heading*Time.deltaTime*Input.GetAxis("Horizontal")*speed;
        }
    }
    void PlayerTurn() {

//        rigid.constraints = RigidbodyConstraints.None;
//        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ;

        Quaternion newRotation = Quaternion.LookRotation(V3Direction);
        // Debug.Log(newRotation);
        //rigid.MoveRotation(newRotation);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, Rotation_speed * Time.deltaTime);
    }
    void Move() {
        if(move){
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
    }
}
