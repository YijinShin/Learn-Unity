using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera1;
    //public var subCamera2;
    //public var subCamera3;
    // Start is called before the first frame update
    void Start()
    {
        mainCameraOn();
    }

    void update()
    {
        /*
        if(Input.GetButton("MainC"))//1
        {
            mainCameraOn();
            Debug.Log("key 1");
        }
        if(Input.GetButton("SubC1"))//2
        {
            subCameraOn();
            Debug.Log("key 2");            
        } 
        */
    }
    void mainCameraOn(){
        mainCamera.GetComponent<Camera>().enabled = true;
        subCamera1.GetComponent<Camera>().enabled = false;
    }

    void subCameraOn(){
        mainCamera.GetComponent<Camera>().enabled = false;
        subCamera1.GetComponent<Camera>().enabled = true;
    }
}
