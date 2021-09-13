using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //game object
    [SerializeField] private GameObject go_MenuBase;
    [SerializeField] private GameObject go_Proviso;
    [SerializeField] private GameObject go_PreInfo;
    private GameObject go_PlayingUI;

    //상태변수
    private bool isProviso = false; // 단서 창인지
    private bool isPreInfo = false; // 사전정보 창인지

    void Start(){
        
    }
    
    void Update()
    {
        OpenMenu();
        CloseMenu();
        OpenProviso();
        CloseProviso();
        OpenPreInfo();
        ClosePreInfo();
    }

    private void OpenMenu(){
        if(GameManager.isOpenMenu){
            go_MenuBase.SetActive(true);
        }
    }    
    private void CloseMenu(){
        if(!GameManager.isOpenMenu){
            isProviso = false;
            isPreInfo = false;
            go_MenuBase.SetActive(false);
        }
    }

    private void OpenProviso(){
        if(isProviso){
            go_Proviso.SetActive(true);
        }
    }

    private void CloseProviso(){
        if(!isProviso){
            go_Proviso.SetActive(false);
        }
    }

    private void OpenPreInfo(){
        if(isPreInfo){
            go_PreInfo.SetActive(true);
        }
    }

    private void ClosePreInfo(){
        if(!isPreInfo){
            go_PreInfo.SetActive(false);
        }
    }

    public void ClickProviso(){
        isProviso = true;
        isPreInfo = false;
    }

    public void ClickPreInfo(){
        isProviso = false;
        isPreInfo = true;
    }
}
