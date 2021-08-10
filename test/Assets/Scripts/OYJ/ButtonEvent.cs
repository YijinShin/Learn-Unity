using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameObject button;
    public Color clickColor;
    public Color unclickColor;

    public void clickButton()
    {
        gameObject.GetComponentInChildren<Image>().color = clickColor;

    }

    public void unclickButton()
    {
        gameObject.GetComponentInChildren<Image>().color = unclickColor;

    }
}
