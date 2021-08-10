using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderGage : MonoBehaviour
{
    public GameObject EEG;
    public GameObject gage;
    private DisplayData eeg;

    public int min;
    public float speed;
    private int meditation;
    // Start is called before the first frame update
    void Start()
    {
        eeg = EEG.GetComponent<DisplayData>();
    }

    // Update is called once per frame
    void Update()
    {
        meditation = eeg.meditation1;
        if (meditation > min)
        {
            gage.GetComponent<Slider>().value += meditation * speed;

        }
    }
}
