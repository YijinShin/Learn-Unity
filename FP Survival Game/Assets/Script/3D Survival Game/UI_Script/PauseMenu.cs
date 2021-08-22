using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

   [SerializeField] private GameObject go_BaseUi;
   [SerializeField] private SaveNLoad theSaveNLoad;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(!GameManager.isPause){
                CallMenu();
            }
            else{
                CloseMenu();
            }
        }
    }

    private void CallMenu(){
        GameManager.isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;//시간 흐름 조절 , 0배속으로 흐르게 함. 즉 시간이 멈춤
    }
    private void CloseMenu(){
        GameManager.isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;//시간 흐름 조절 , 1배속으로 흐르게 함. 즉 정상적인 흐름. 
    }   

    public void ClickSave(){
        theSaveNLoad.SaveData(); // 기록 
    }

    public void ClickLoad(){
        theSaveNLoad.LoadData();
    }
    public void ClickExit(){
        Application.Quit(); // 게임 종료
    }
}
