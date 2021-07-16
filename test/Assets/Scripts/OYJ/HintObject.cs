using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObject : MonoBehaviour
{
    // Start is called before the first frame update
    // void OnCollisionEnter(Collision collision){
    //     if(collision.gameObject.tag == "Player"){
    //         Debug.Log(this.name);
    //     }
    // }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Debug.Log(this.name);
        }
    }
}
