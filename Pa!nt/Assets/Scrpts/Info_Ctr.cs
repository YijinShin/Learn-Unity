using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_Ctr : MonoBehaviour
{
    [SerializeField]
    private GameObject information;

    [SerializeField]
    private GameObject thisOb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D o){
        if(o.gameObject.name == "Player")
        {
            information.SetActive(true);
            thisOb.SetActive(false);
        }
    }
}
