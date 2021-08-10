using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public Color clickColor;
    public Color unclickColor;
    public void changeSet()
    {
        gameObject.GetComponentInChildren<Image>().color = clickColor;
    }

    public void unchangeSet()
    {
        gameObject.GetComponentInChildren<Image>().color = unclickColor;

    }
}
