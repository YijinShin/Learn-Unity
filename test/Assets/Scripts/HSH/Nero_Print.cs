using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nero_Print : MonoBehaviour
{
    public GameObject Display_Data;
    private DisplayData DD;
    public TextMeshProUGUI TMP1;
    public TextMeshProUGUI TMP2;
    public TextMeshProUGUI TMP3;

    // Start is called before the first frame update
    void Start()
    {
        DD = Display_Data.GetComponent<DisplayData>();

    }

    // Update is called once per frame
    void Update()
    {
        TMP1.text=DD.poorSignal1.ToString();
        TMP2.text = DD.attention1.ToString();
        TMP3.text = DD.meditation1.ToString();
    }
}
