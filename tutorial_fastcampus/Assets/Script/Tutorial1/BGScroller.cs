using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//data class이기 때문에 Data를 인스팩터 창에서 접근 할 수 있도록 하기 위해 public으로 거의 선언한다. 

[System.Serializable]
public class BGScrollData
{
    public Renderer RenderForScroll;
    public float Speed;
    public float OffsetX;
}

public class BGScroller : MonoBehaviour
{
    [SerializeField]
    BGScrollData [] ScrollDatas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScroll();
    }

    void UpdateScroll()
    {
        //for which가 for보다 느려서 For문 사용 
        for(int i=0 ; i < ScrollDatas.Length ; i++){
            SetTextureOffset(ScrollDatas[i]);
        }
    }

    void SetTextureOffset(BGScrollData scrollData)
    {
        //초당 스피드를 offsetX에 더한다 = 이 값으로 x축으로 스크롤링하겠따. 
        scrollData.OffsetX += (float)(scrollData.Speed) * Time.deltaTime; // deltaTime : 한 프레임을 그리는데 걸리는 시간. 초단위

        Vector2 Offset = new Vector2(scrollData.OffsetX, 0);

        scrollData.RenderForScroll.material.SetTextureOffset("_MainTex", Offset);
    }
}
