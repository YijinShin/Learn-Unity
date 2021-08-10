using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPM : ScriptableObject
{
    [SerializeField]
    private Item item1;
    [SerializeField]
    private Item item2;
    [SerializeField]
    private Item item3;
    void Update()
    {
        CheckAllFound();
    }

    public void CheckAllFound(){
        if(item1.hasFound && item2.hasFound && item3.hasFound){
            Debug.Log("All Found! ");
        }
    }
}
