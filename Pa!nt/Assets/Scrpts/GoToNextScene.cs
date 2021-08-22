using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    void Start(){

    }
    // Start is called before the first frame update
    public void GoToStartScene() {
        SceneManager.LoadScene("Stage_Select");
    }

    public void GoToTutorialScene() {
        SceneManager.LoadScene("Stage_0");
    }
}
