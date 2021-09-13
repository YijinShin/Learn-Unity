using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Camera : MonoBehaviour
{
    [SerializeField]
    private float rotate_Speed;
    [SerializeField]
    private Transform CH;
    [SerializeField]
    private Transform Camera_Arm;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void LookAround(){
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        Vector3 camAngle = Camera_Arm.rotation.eulerAngles;

        if(Input.GetMouseButton(0))
        Camera_Arm.rotation = Quaternion.Euler(0,camAngle.y+mouseDelta.x,camAngle.z);
    }
    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }
    private void Move(){
        if(Input.GetMouseButton(0))
        Camera_Arm.position =new Vector3(CH.position.x,CH.position.y+6,CH.position.z);
    }
}
