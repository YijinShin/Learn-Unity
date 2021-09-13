using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialIntro : MonoBehaviour
{
    [SerializeField] private GameManager theGameManager;
    [SerializeField] private GameObject logBase;
    [SerializeField] private Text nameText;
    [SerializeField] private Text logText;

    public string sceneName = "TutorialScene";
    private int currentLine = 0;
    private int lineNum;

    private float delayTime = 1f;

    void Start()
    {
        //lineNum = GameManager.lineSize;
        theGameManager = FindObjectOfType<GameManager>();
        //StartCoroutine("StartDelayCoroutine"); // 시작하고 2초 정도 뒤에 대사가 나오도록 
        
        
    }
    void Update()
    {
        StartCoroutine(StartDelayCoroutine()); 
        TryNextLog();
    }

    IEnumerator StartDelayCoroutine(){// 시작 딜레이 
        yield return new WaitForSeconds(delayTime); 

        logBase.SetActive(true);
        lineNum = theGameManager.lineSize; // 총 line 수 받아오기 
        nameText.text = theGameManager.Sentence[currentLine,0]; // 첫 name text 설정
        logText.text = theGameManager.Sentence[currentLine,1]; // 첫 log text 설정  
    }

    private void TryNextLog(){ // space를 누르면 다음 대사로 넘어가기 
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("currentLine:" + currentLine);
            if(currentLine < lineNum-1){ // 다음으로 대사 넘기기 
                currentLine += 1; 
                nameText.text = theGameManager.Sentence[currentLine,0]; // 첫 name text 설정
                logText.text = theGameManager.Sentence[currentLine,1]; 
            }
            else{ // 게임씬으로 넘어가기 
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
