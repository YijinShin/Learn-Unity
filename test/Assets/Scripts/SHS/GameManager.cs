using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public Text talkText;
    public Text[] Explanation; // 설명창
    GameObject scanObject; // 플레이어가 닿은 물체
    public GameObject[] menuSet; // UI 창
    public GameObject dialogue; // 대화 창
    public int talkIndex;
    public GameObject select; // 범인 지목 창
    string talkData;

    public void Action(GameObject scanObj) // 플레이어 상태에 따라 다른 UI창에 각각 내용 기입
    {
        scanObject = scanObj;

        for(int i = 0; i < Explanation.Length; i++)
        {
            Explanation[i].text = "이 것의 이름은 " + scanObject.name + " 입니다.\n 그리고 현재 플레이어 상태는 " + i +"입니다.\n"; // UI 설명창에 플레이어 가까이 있는 객체 이름 출력
        }
    }

    public void menuAction(int playerStatus) // 플레이어 상태에 따라 다른 UI창 띄우기
    {
        /*if(menuSet[playerStatus].activeSelf)
        {
            menuSet[playerStatus].SetActive(false); // UI창 이미 띄워져있으면 안띄움
        }
        else
        {*/
            menuSet[playerStatus].SetActive(true); // 없으면 띄움
        //}
    }

    public void NextScene()
    {
        SceneManager.LoadScene("NextScene"); // Load Scene 버튼 누르면 다음 Scene으로 넘어감
    }

    public bool talkAction(int countInvestigate, string key, int number)
    {
        
        if(countInvestigate == 0)
        {
            dialogue.SetActive(false); // UI창 이미 띄워져있으면 안띄움
            return false; 
        }
        
        else
        {
            dialogue.SetActive(true); // 없으면 띄움
            talkData = talkManager.GetByKey(key, number);
            talkText.text = talkData;

            return true;
        }
    }

    public void selectCriminal()
    {
        if(select.activeSelf)
        {
            select.SetActive(false);    
        }

        else
        {
            select.SetActive(true);
        }
    }
}

