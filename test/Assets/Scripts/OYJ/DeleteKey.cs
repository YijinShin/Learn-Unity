using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteKey : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn;
    GameObject noteBtn;
    // Update is called once per frame
    void Start()
    {
        btn = this.transform.GetComponent<Button>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            btn.onClick.Invoke();
        }
        
    }
}
