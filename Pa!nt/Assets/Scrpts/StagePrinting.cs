using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePrinting : MonoBehaviour
{
    public int stage;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "Stage"+stage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
