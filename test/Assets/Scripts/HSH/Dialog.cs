using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{

    public GameObject dialogue; // 대화 창
    string talkData;
    public TalkManager talkManager;
    public Text talkText;
    // Start is called before the first frame update
    public bool talkAction(int countInvestigate, int talkIndex)
    {
        Debug.Log("countInvestigate : " + countInvestigate);
        if (countInvestigate == 0)
        {
            dialogue.SetActive(false); // UI창 이미 띄워져있으면 안띄움
            countInvestigate++;
            return false;
        }

        else
        {
            dialogue.SetActive(true); // 없으면 띄움
            talkData = talkManager.GetExplain(talkIndex);
            talkText.text = talkData;

            return true;
        }
    }
}
