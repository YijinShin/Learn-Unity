using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_player_mv : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int applySpeed;

    [SerializeField]
    private int lookSensitivity;
    private Rigidbody myRigid;
    
    [SerializeField]
    private GameObject ray;
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CharacterRotation();
        Up();
        Down();
    }

    private void Move(){
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    private void CharacterRotation(){
        if(Input.GetButton("Fire1")){
            float _yRotation = Input.GetAxisRaw("Mouse X");
            Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f)*lookSensitivity;
            myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        }
        
           
    }

    private void Up(){
        if(Input.GetButtonDown("Jump")){
            ray.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        if(Input.GetButtonUp("Jump")){
            ray.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
    }

    private void Down(){
        if(Input.GetKey(KeyCode.K)){
            ray.transform.localPosition = new Vector3(0, -0.5f, 0);
        }
        if(Input.GetKeyUp(KeyCode.K)){
            ray.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
    }
}
