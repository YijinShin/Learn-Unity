using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Camera[] arrCam; // 카메라 개수
    AudioListener[] listeners; // 오디오 리스너
    public int nCamCount; // 총 카메라 개수
    public int nNowCam; // 현재 활성화되어있는 카메라 번호
    int nListenerCount; // 총 오디오리스너 개수
    int nNowListener; // 현재 활성화 되어있는 오디오리스너 번호

    void Awake()
    {
        nCamCount = arrCam.Length;
        nNowCam = 0;

        storeListeners();
        nListenerCount = listeners.Length;
        nNowListener = 0;

        cameraEnabled();
        ListenerEnabled();

    }

    void Update()
    {
        if(Input.GetKey("m"))
        {
            nNowCam = nNowListener = 0;
            cameraEnabled();
            ListenerEnabled();
        }

        else if(Input.GetKey("1"))
        {
            nNowCam = nNowListener = 1;
            cameraEnabled();
            ListenerEnabled();
            
        }

        else if(Input.GetKey("2"))
        {
            nNowCam = nNowListener = 2;
            cameraEnabled();
            ListenerEnabled();
            
        }

        else if(Input.GetKey("3"))
        {
            nNowCam = nNowListener = 3;
            cameraEnabled();
            ListenerEnabled();
            
        }

        else if(Input.GetKey("4"))
        {
            nNowCam = nNowListener = 4;
            cameraEnabled();
            ListenerEnabled();
        }
    }

    void storeListeners() // 오디오리스너 저장
    {
        listeners = new AudioListener[nCamCount];
        for (int i = 0; i < nCamCount; i++)
        {
            listeners[i] = arrCam[i].GetComponent<AudioListener>();
        }
    }
    void cameraEnabled() // 선택된 카메라 활성화
    {
        for (int i = 0; i < nCamCount; i++)
        {
            arrCam[i].enabled = (i == nNowCam);
        }
    }

    void ListenerEnabled() // 선택된 카메라의 오디오리스너 활성화
    {
        for (int i = 0; i < nListenerCount; i++)
        {
            listeners[i].enabled = (i == nNowListener);
        }
    }
}
