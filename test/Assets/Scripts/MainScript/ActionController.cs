using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ActionController : MonoBehaviour
{
    public bool TP_ON;
    public GameObject TP;


    public string key;
    public int linecounter;
    public GameObject talkmanager;
    public Text text;
    public string temp;
    public Text PMtext;


    [SerializeField]
    private float range; //조가 가능한 최대 거리 

    private bool investigateActivate = false; // 조사 가능할 시 true
    private bool itemImageActivate = false; // 이미지 활성화 여부 

    private RaycastHit hitInfo; // 충돌체 정보 저장 

    [SerializeField]
    private LayerMask layerMask; // 아이템 레이어에만 반응하도록 레이어마스크 설정
    [SerializeField]
    private Item item1;
    [SerializeField]
    private Item item2;
    [SerializeField]
    private Item item3;

    //필요한 컴포넌트
    [SerializeField]
    private GameObject AT;
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Image itemImage;

    public GameObject gage;

    public GameObject PMobject;

    public GameObject VideoPlayer;
    public GameObject PMmovie;
    public GameObject Puyzzle;
    public GameObject SelectCriminal;
    public GameObject Door_IN;
    private VideoPlayer m_VideoPlayer;

  /*  private void Awake()
    {
        m_VideoPlayer = GetComponent();
        m_VideoPlayer.loopPointReached += OnMo
    }*/
    void Update()
    {
        CheckItem();
        TryAction();
        CheckAllFound();
        PM();
        //print();
    }

    public void Close_All()
    {
        TP.SetActive(false);
        TP_ON = false;
        itemImageActivate = !itemImageActivate;
        Time.timeScale = 1;
        ItemImageDisappear();
    
    }

    private void TryAction()
    { // 조사하기 누름 
        if (Input.GetButtonDown("Investigate"))
        {
            if (!itemImageActivate)
            {
                itemImageActivate = !itemImageActivate;
                CheckItem();
                CanInvestigate();
            }
            else if (itemImageActivate)
            {
                itemImageActivate = !itemImageActivate;
            }

        }
    }



/*    void print() {
        TP_ON = true;
        TP.SetActive(true);
            temp = talkmanager.GetComponent<TalkManager>().GetByKey(hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName, linecounter);
            if (temp == "Wrong")
                Close_All();
            text.text = temp;
            linecounter++;
    }*/
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        { // 범위안에 속하고
            if (hitInfo.transform.tag == "Item")
            { // 아이템 태그인 경우
                ItemInfoAppear();// text 보여줌
            }
        }
        else
        {
            InfoDisappear();// text 사라짐
            ItemImageDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        investigateActivate = true; //이제 조사 가능 
        AT.SetActive(true); // text활성화 
        actionText.text = "조사하기"; // text 내용 수정

        //image 내용 수정 
    }

    private void InfoDisappear()
    {
        investigateActivate = false; //조사 불가능 
        AT.SetActive(false); // text 비활성화 
    }

    private void CanInvestigate()
    {
        if(!TP_ON)
        if (investigateActivate)
        { // 조사가 가능하고
            if (hitInfo.transform != null)
            {  // 정보가 있을 경우(혹시 모를 오류 방지) 
              // 조사함 로그
                if (hitInfo.transform.gameObject.name == "Door")
                    {
                        VideoPlayer.SetActive(true);
                        PMmovie.SetActive(true);
                       Invoke("OffVideo",4);
                 }
                 else
                 {
                        Debug.Log("investigate :" + hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName);
                        hitInfo.transform.GetComponent<ItemInvestigate>().item.hasFound = true;
                        hitInfo.transform.GetComponent<ItemInvestigate>().item.hint.SetActive(true);
                        //Time.timeScale = 0;
                        ShowItemImage();
                  }
            }

        }
        else
        {
                Debug.Log("살려줘");
            ItemImageDisappear();
        }
    }

    private void ShowItemImage()
    {
        itemImage.sprite = hitInfo.transform.GetComponent<ItemInvestigate>().item.itemImage; // text 내용 수정
        if (hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName == "Phone")
        {
            Puyzzle.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(true); // image 활성화 
            actionText.gameObject.SetActive(false); // text 비활성화 
            itemImageActivate = true;
        }
    }

    private void ItemImageDisappear()
    {
//        Time.timeScale = 1;
        itemImage.gameObject.SetActive(false); // image비활성화
        itemImageActivate = false;
    }

    private void CheckAllFound(){
        if (gage.GetComponent<Slider>().value != 100)
        {
            if (item1.hasFound && item2.hasFound && item3.hasFound)
            {
                gage.GetComponent<Slider>().value = 100;
                PMtext.gameObject.SetActive(true); // text 비활성화 
                PMtext.text = "p를 눌러서 사이코 메트리를 쓰자."; // text 내용 수정

            }
        }
    }

    private void PM()
    {
        if (item1.hasFound && item2.hasFound && item3.hasFound)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("p");
                PMobject.layer = 8;
                PMobject.tag = "Item";

                PMtext.gameObject.SetActive(true); // text 비활성화 
                PMtext.text = "붉게 빛나는 물체가 있다 가까이 가서 조사해보자"; // text 내용 수정
                Door_IN.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }

    void OnMovieFinished (VideoPlayer play) {
        Debug.Log("Video_end");
        play.Stop();
    }

    private void OffVideo()
    {

        SelectCriminal.SetActive(true);
        VideoPlayer.SetActive(false);
        PMmovie.SetActive(false); 
    }

}