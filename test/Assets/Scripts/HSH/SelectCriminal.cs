using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCriminal : MonoBehaviour
{
    public GameObject select;
    // Start is called before the first frame update
    public void selectCriminal()
    {
        if (select.activeSelf)
        {
            select.SetActive(false);
        }

        else
        {
            select.SetActive(true);
        }
    }
}
