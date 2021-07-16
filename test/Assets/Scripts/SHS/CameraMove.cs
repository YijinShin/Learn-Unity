using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public Transform mainCameraRotation;
    public Camera camera1;
    public Transform camera1Rotation;
    public Camera camera2;
    public Transform camera2Rotation;
    public Camera camera3;
    public Transform camera3Rotation;
    public Camera camera4;
    public Transform camera4Rotation;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera.enabled = true;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
    }

    public void mainCameraEnabled()
    {
        mainCamera.enabled = true;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
    }

    public void camera1Enabled()
    {
        mainCamera.enabled = false;
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
    }
    public void camera2Enabled()
    {
        mainCamera.enabled = false;
        camera1.enabled = false;
        camera2.enabled = true;
        camera3.enabled = false;
        camera4.enabled = false;
    }
    public void camera3Enabled()
    {
        mainCamera.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = true;
        camera4.enabled = false;
    }
    public void camera4Enabled()
    {
        mainCamera.enabled = false;
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("m"))
        {
            mainCameraEnabled();
            Debug.Log(""+mainCameraRotation.rotation.eulerAngles);
            Debug.Log("M player's rotation : "+player.transform.rotation.eulerAngles);
            player.transform.rotation = Quaternion.Euler(mainCameraRotation.rotation.eulerAngles);
        }

        else if(Input.GetKey("1"))
        {
            camera1Enabled();
            Debug.Log(""+camera1Rotation.rotation.eulerAngles);
            Debug.Log("1 player's rotation : "+player.transform.rotation.eulerAngles);
            player.transform.rotation = Quaternion.Euler(camera1Rotation.rotation.eulerAngles);
        }

        else if(Input.GetKey("2"))
        {
            camera2Enabled();
            Debug.Log(""+camera2Rotation.rotation.eulerAngles);
            Debug.Log("2 player's rotation : "+player.transform.rotation.eulerAngles);
            player.transform.rotation = Quaternion.Euler(camera2Rotation.rotation.eulerAngles);
        }

        else if(Input.GetKey("3"))
        {
            camera3Enabled();
            Debug.Log(""+camera3Rotation.rotation.eulerAngles);
            Debug.Log("3 player's rotation : "+player.transform.rotation.eulerAngles);
            player.transform.rotation = Quaternion.Euler(camera3Rotation.rotation.eulerAngles);
        }

        else if(Input.GetKey("4"))
        {
            camera4Enabled();
            Debug.Log(""+camera4Rotation.rotation.eulerAngles);
            Debug.Log("4 player's rotation : "+player.transform.rotation.eulerAngles);
            player.transform.rotation = Quaternion.Euler(camera4Rotation.rotation.eulerAngles);
        }
    }
}
