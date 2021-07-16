using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    // Start is called before the first frame update


    float v;
    float h;
    Vector3 move;
    public int speed;

    GameObject mainCamera;
    GameObject redCamera;
    GameObject blueCamera;
    GameObject redBox;
    GameObject temp;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        redCamera = GameObject.Find("RedCamera");
        blueCamera = GameObject.Find("BlueCamera");

        redBox = GameObject.Find("RedBox");

        mainCamera.GetComponent<Camera>().enabled = true;
        redCamera.GetComponent<Camera>().enabled = false;
        blueCamera.GetComponent<Camera>().enabled = false;
        temp = mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("FirstCamera")){
            TurnCamera(mainCamera);
            temp = mainCamera;
        }
        if(Input.GetButtonDown("SecondCamera")){
            TurnCamera(redCamera);
            temp = redCamera;
        }
        if(Input.GetButtonDown("ThirdCamera")){
            TurnCamera(blueCamera);
            temp = blueCamera;
        }
        
    }
    void FixedUpdate(){
        CriCamera(temp);
    }

    void TurnCamera(GameObject camera){

        mainCamera.GetComponent<Camera>().enabled = false;
        redCamera.GetComponent<Camera>().enabled = false;
        blueCamera.GetComponent<Camera>().enabled = false;
        camera.GetComponent<Camera>().enabled = true;
    }
    void CriCamera(GameObject camera){

        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        Vector3 normalV = camera.transform.forward.normalized;
        normalV.y = 0f;

        Vector3 normalH = camera.transform.right.normalized;
        normalH.y = 0f;

        move = normalH*h + normalV*v;
        transform.position += move*speed * Time.deltaTime;        
        transform.LookAt(transform.position + move);
    }

    // void OnCollisionEnter(Collision collision){
    //     if(collision.gameObject.tag == "Hint"){
    //         Debug.Log(collision.gameObject.name);
    //         Debug.Log(this.name);
    //     }
            
    //     Debug.Log(collision.gameObject.name);
    // }
}
