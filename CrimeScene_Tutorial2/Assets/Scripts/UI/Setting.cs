using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject go_SettingBase;
    private string sceneName = "HomeScene";
    void Update()
    {
        OpenSetting();
        CloseSetting();
    }

    private void OpenSetting(){
        if(GameManager.isOpenSetting){
            go_SettingBase.SetActive(true);
        }
    }

    private void CloseSetting(){
        if(!GameManager.isOpenSetting){
            go_SettingBase.SetActive(false);
        }
    }

    public void ClickExit(){
        GameManager.isOpenSetting = false;
        go_SettingBase.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
