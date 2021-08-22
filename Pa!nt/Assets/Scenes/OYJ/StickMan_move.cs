using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan_move : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")){
            anim.SetBool("IsMove",true);
        }else if(Input.GetButtonUp("Horizontal")){
            anim.SetBool("IsMove",false);
        }
    }
}
