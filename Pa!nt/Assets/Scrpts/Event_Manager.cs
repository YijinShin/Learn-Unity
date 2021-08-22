using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameEnd() {

        Application.Quit();
    }
    public void SceneReload() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoSelect() {
        SceneManager.LoadScene("Stage_Select");
    }

    public void GotoFirstPage()
    {
        SceneManager.LoadScene("Stage_FirstPage");
    }
}
