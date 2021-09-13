using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    private string sceneName = "TutorialIntroScene";

    void Start()
    {
        
    }

    void Update()
    {
    
    }

    public void ClickTutorial(){
        SceneManager.LoadScene(sceneName);
        Debug.Log("tutorial click");
    }
}
