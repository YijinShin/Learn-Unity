using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_UI : MonoBehaviour
{

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpDown() {
        Player.GetComponent<CH_move>().SetJump(true);
        Debug.Log("GetKey");
        Debug.Log(Player);
    }
    public void JumpUp()
    {
        Player.GetComponent<CH_move>().SetJump(false);
    }
    public void LeftDown() {
        Player.GetComponent<CH_move>().SetLeft(true);
    }
    public void RightDown()
    {
        Player.GetComponent<CH_move>().SetRight(true);
    }
    public void RightUP() {
        Player.GetComponent<CH_move>().SetRight(false);
    }
    public void LeftUP()
    {
        Player.GetComponent<CH_move>().SetLeft(false);
    }
    public void InteractiveDown() {

        Player.GetComponent<CH_move>().Set_Interact(true);
    }
    public void InteractiveUP() {
        Player.GetComponent<CH_move>().Set_Interact(false);
    }

}
