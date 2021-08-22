using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private Transform Camera_Trans;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        Camera_Trans = this.GetComponent<Transform>();
        trans=Player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera_Trans.position = new Vector3(trans.position.x, trans.position.y+10, trans.position.z - 10); 
    }
}
