using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Select_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Select();
    }

    void Select()
    {
        string stageName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(stageName);
    }
}
