using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "HomeScene";

    void Start()
    {
        
    }

    void Update()
    {
        TryHome();
    }

    private void TryHome(){
        if(Input.anyKey){
            SceneManager.LoadScene(sceneName);
        }
    }
}
