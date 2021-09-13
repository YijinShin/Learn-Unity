using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_GTM : MonoBehaviour
{
    public Text talkText;

    public RaycastHit scanObject;

    // Update is called once per frame
    public void Action(RaycastHit scanob)
    {
        // scanObject = scanob;
        talkText.text = "이것의 이름은" + scanob.transform.name;
    }
}
