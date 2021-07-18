using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text Explanataion; // 설명창
    public GameObject scanObject; // 플레이어가 닿은 물체
    public GameObject menuSet; // UI 창
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        Explanataion.text = "이 것의 이름은 " + scanObject.name + " 입니다.";
    }

    public void menuAction()
    {
        if(menuSet.activeSelf)
        {
            menuSet.SetActive(false);
        }
        else
        {
            menuSet.SetActive(true);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("NextScene");
    }
}
