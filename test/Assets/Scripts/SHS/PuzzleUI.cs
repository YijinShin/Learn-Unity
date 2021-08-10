using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    public GameObject EEG;
    public GameObject Main;
    public GameObject puzzle;
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

        if(cnt >= 10)
        {
            mcolor.a = 1;
            Invoke("Turnoff", 0.5f);
        }
        else
        {
            mcolor.a = f;
        }
        mcolor.r = 1;
        mcolor.g = 1;
        mcolor.b = 1;

        puzzle.GetComponent<Image>().color = mcolor;
        unlocking();
    }

    public void unlocking()
    {
        if(t >= 70 && temp != t)
        {
            cnt++;
        }

        temp = t;
    }
    void Turnoff() {
        Main.SetActive(false);
    }
}
