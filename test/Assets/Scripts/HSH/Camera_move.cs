using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    public GameObject Ch;
    private Transform Camera_tr;
    private Transform Ch_tr;

    // Start is called before the first frame update
    void Start()
    {
        Ch_tr=Ch.GetComponent<Transform>();
        Camera_tr=this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera_tr.position=new Vector3(Ch_tr.position.x+3,Ch_tr.position.y+3,Ch_tr.position.z);
    }
}
