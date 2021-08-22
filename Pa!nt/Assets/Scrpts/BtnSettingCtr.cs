using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingCtr : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject stickBtn;

    [SerializeField]
    private GameObject keysBtn;

    int playerButtonMode;
    bool joystick = false;

    void Start()
    {
        playerButtonMode = player.GetComponent<CH_move>().GetButtonMode();
        Debug.Log("button mode" + playerButtonMode);
        if(playerButtonMode == 1){
            joystick = false;
        }else{
            joystick = true;
        }
        
        stickBtn.SetActive(joystick);
        keysBtn.SetActive(!joystick);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
