using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeditationUI : MonoBehaviour
{
    public GameObject EEG;
    public GameObject med;
    private DisplayData eeg;
    Color mcolor;
    
    private int temp = 0;
    public int t;
    public float f;
    public int cnt;
    // Start is called before the first frame update
    void Start()
    {
        eeg = EEG.GetComponent<DisplayData>();
        f = 0;
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t = eeg.meditation1;
        f = t * (float)0.01;

        mcolor.a = f;
        mcolor.r = 1;
        mcolor.g = 1;
        mcolor.b = 1;

        med.GetComponent<Image>().color = mcolor;
    }
}