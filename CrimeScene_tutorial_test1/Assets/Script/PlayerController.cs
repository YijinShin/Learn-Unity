using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField]
    private float walkSpeed;
    [SerializeField]    
    private float applySpeed;

   // private Rigidbody myRigid;
    void Start()
    {
     //   myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        Debug.Log("heelo");
    }

    void Update()
    {
        //Move();
    }

    /*
    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal"); 
        float _moveDirZ = Input.GetAxisRaw("Vertical"); 

        //플레이어 위치에 더해줄 이동값
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        //최종 이동값 
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        //이동 
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    */
}

