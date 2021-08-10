using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{

    public GameObject dialogue; // ��ȭ â
    string talkData;
    public TalkManager talkManager;
    public Text talkText;
    // Start is called before the first frame update
    public bool talkAction(int countInvestigate, int talkIndex)
    {
        Debug.Log("countInvestigate : " + countInvestigate);
        if (countInvestigate == 0)
        {
            dialogue.SetActive(false); // UIâ �̹� ����������� �ȶ��
            countInvestigate++;
            return false;
        }

        else
        {
            dialogue.SetActive(true); // ������ ���
            talkData = talkManager.GetExplain(talkIndex);
            talkText.text = talkData;

            return true;
        }
    }
}
