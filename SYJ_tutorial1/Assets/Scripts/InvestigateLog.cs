using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateLog : MonoBehaviour
{
    
    [SerializeField] private Text log;
    private ActionController theActionController;
    void Start()
    {
        theActionController = FindObjectOfType<ActionController>();
    }

    public void SetLogText(Item _item){
        if(theActionController.isSit){
            log.text = _item.sitLog;
        }
        else if(theActionController.isTiptoe){
            log.text = _item.tiptoeLog;
        }
        else{
            log.text = _item.frontLog;
        }
    }
}
