using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingUI : MonoBehaviour
{
    void Update()
    {
        Close();
    }

    private void Close(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.isOpenMenu = false;
            GameManager.isOpenSetting = false;
            GameManager.isOpenVote = false;
        }
    }

    public void ClickMenu(){
        GameManager.isOpenMenu = !GameManager.isOpenMenu;
        GameManager.isOpenSetting = false;
        GameManager.isOpenVote = false;
    }

    public void ClickSetting(){
        GameManager.isOpenSetting = !GameManager.isOpenSetting;
        GameManager.isOpenMenu = false;
        GameManager.isOpenVote = false;        
    }
    public void ClickVote(){
        GameManager.isOpenVote = !GameManager.isOpenVote;
        GameManager.isOpenMenu = false;
        GameManager.isOpenSetting = false;
    }   

}
