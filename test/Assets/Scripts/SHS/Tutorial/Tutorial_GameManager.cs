using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_GameManager : MonoBehaviour
{
    public Tutorial_TalkManager talkManager;
    public Text talkText;
    string talkData;
    static int talkIndex = 0;

    GameObject diary_UI;
    GameObject dialogue;
    GameObject victim;
    GameObject suspects;
    GameObject notebook;
    GameObject notebook_UI;
    GameObject letter_UI;
    GameObject clue_Info;
    GameObject pre_Info;
    GameObject smartphone_A_UI;
    GameObject smartphone_B_UI;
    GameObject knife_UI;
    GameObject airplane_UI;
    GameObject vote;

    void Start()
    {
        //talkManager = GetComponent<Tutorial_TalkManager>();
        dialogue = GameObject.Find("Dialogue");
        //victim = GameObject.Find("Victim");
        //suspects = GameObject.Find("Suspects");
        //diary_UI = GameObject.Find("Diary_UI");

        if(SceneManager.GetActiveScene().name == "Tutorial_FirstScene")
        {
            victim = GameObject.Find("Victim");
            suspects = GameObject.Find("Suspects");

            victim.SetActive(false);
            suspects.SetActive(false);
        }

        else
        {
            diary_UI = GameObject.Find("Diary_UI");
            notebook = GameObject.Find("Notebook");
            notebook_UI = GameObject.Find("Notebook_UI");
            clue_Info = GameObject.Find("Clue_Info");
            pre_Info = GameObject.Find("Pre_Info");
            letter_UI = GameObject.Find("Letter_UI");
            smartphone_A_UI = GameObject.Find("Smartphone_A_UI");
            smartphone_B_UI = GameObject.Find("Smartphone_B_UI");
            knife_UI = GameObject.Find("Knife_UI");
            airplane_UI = GameObject.Find("Airplane_UI");
            vote = GameObject.Find("Vote");

            diary_UI.SetActive(false);
            notebook.SetActive(false);
            notebook_UI.SetActive(false);
            clue_Info.SetActive(false);
            pre_Info.SetActive(false);
            letter_UI.SetActive(false);
            smartphone_A_UI.SetActive(false);
            smartphone_B_UI.SetActive(false);
            knife_UI.SetActive(false);
            airplane_UI.SetActive(false);
            vote.SetActive(false);
        }
        
        
    }

    void Update()
    {
        showDialogue();
    }

    void FixedUpdate()
    {
        MoveToCrimeScene();
        MoveToFirstScene();
        showPanel();
    }

    void showDialogue()
    {
        talkData = talkManager.GetExplain(talkIndex);
        talkText.text = talkData;

        if(talkData == null)
        {
            dialogue.SetActive(false);
        }

        if(Input.GetButtonDown("Investigate"))
        {
            if((talkIndex == 26 && diary_UI.activeSelf == false) || (talkIndex == 30 && pre_Info.activeSelf == false) || (talkIndex == 32 && clue_Info.activeSelf == false)
             || (talkIndex == 33 && notebook_UI.activeSelf == true) ||(talkIndex == 35 && letter_UI.activeSelf == false) || (talkIndex == 41 && smartphone_A_UI.activeSelf == false)
             || (talkIndex == 57 && smartphone_B_UI.activeSelf == false) || (talkIndex == 65 && knife_UI.activeSelf == false) || (talkIndex == 69 && airplane_UI.activeSelf == false))
            {
                
            }

            else
            {
                talkIndex++;
            }
        }

    }

    void showPanel()
    {
        if(talkIndex == 15)
        {
            victim.SetActive(true);
        }

        else if(talkIndex == 16)
        {
            victim.SetActive(false);
            suspects.SetActive(true);
        }

        else if(talkIndex == 17)
        {
            victim.SetActive(false);
            suspects.SetActive(false);
        }

        else if(talkIndex == 29)
        {
            diary_UI.SetActive(false);
        }

        else if (talkIndex == 30)
        {
            notebook.SetActive(true);
        }

        else if (talkIndex == 38)
        {
            letter_UI.SetActive(false);
        }

        else if (talkIndex == 57)
        {
            smartphone_A_UI.SetActive(false);
        }

        else if (talkIndex == 64)
        {
            smartphone_B_UI.SetActive(false);
        }

        else if(talkIndex == 69)
        {
            knife_UI.SetActive(false);
        }

        else if(talkIndex == 71)
        {
            airplane_UI.SetActive(false);
        }

        else if(talkIndex == 94)
        {
            vote.SetActive(true);
        }
    }

    void MoveToCrimeScene()
    {
        if(talkIndex == 18)
        {
            victim.SetActive(false);
            suspects.SetActive(false);
            SceneManager.LoadScene("Tutorial_SHS");
        }
    }

    void MoveToFirstScene()
    {
        if(talkIndex == 103)
        {
            SceneManager.LoadScene("Tutorial_FirstScene");
        }
    }

    public void showDiary_UI()
    {
        diary_UI.SetActive(true);
    }

    public int get_talkIndex()
    {
        return talkIndex;
    }

  
    public void showNotebok_UI()
    {
        if(notebook_UI.activeSelf == false)
        {
            if(talkIndex == 30)
            {
                talkIndex++;
            }
            notebook_UI.SetActive(true);
            pre_Info.SetActive(true);
            clue_Info.SetActive(false);
        }

        else if(notebook_UI.activeSelf == true)
        {
            if(talkIndex == 33)
            {
                talkIndex++;
            }
            notebook_UI.SetActive(false);
            pre_Info.SetActive(false);
            clue_Info.SetActive(false);
        }
    }

    public void showLetter_UI()
    {
        letter_UI.SetActive(true);
    }

    public void showClue_Info()
    {
        pre_Info.SetActive(false);
        clue_Info.SetActive(true);

        if(talkIndex == 31)
        {
            talkIndex++;
        }
    }

    public void showPre_Info()
    {
        pre_Info.SetActive(true);
        clue_Info.SetActive(false);
    }

    public void show_Smartphone_A_UI()
    {
        smartphone_A_UI.SetActive(true);
    }

    public void show_Smartphone_B_UI()
    {
        smartphone_B_UI.SetActive(true);
    }

    public void showKnife_UI()
    {
        knife_UI.SetActive(true);
    }

    public void showAirplane_UI()
    {
        airplane_UI.SetActive(true);
    }

}
