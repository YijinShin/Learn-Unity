using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetButtonMode : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public void SetToJoyStick()
    {
        player.GetComponent<CH_move>().SetButtonMode(2);
    }

    public void SetToKeys()
    {
        player.GetComponent<CH_move>().SetButtonMode(1);
    }
}
