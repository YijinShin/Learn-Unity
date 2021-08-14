using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 이벤트 담당하는 헤더 

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler // IPointerClickHandler : 클릭을 담당하는 인터페이스 , DragHandler : 드레그 시작, 중, 드레그 멈춤, 마우스 클릭 끝 으로 각각 담당하는 인터페이스
{
    
    private Vector3 originPos; // 원래 위치 
    //아이템 정보 
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 갯수
    public Image itemImage; // 아이템 이미지
    
    //필요한 컴포넌트
    [SerializeField]
    private Text itemCountText; // 아이템 수 띄울 텍스트
    [SerializeField]
    private GameObject go_CountImage; // 획득한 아이템 있을 경우만 파란 동그라미띄움. (빈슬롯이면 안띄움)

    private WeaponManager theWeaponManager;
    void Start()
    {
        originPos = transform.position; // 원위치
        theWeaponManager = FindObjectOfType<WeaponManager>(); // SerializeField을 안쓴 이유 : slot은 프리팹이잖아. 프리팹은 SerializeField로 자기 안에 있는 객체만 참조가 가능함. 그냥 하이라키에있는건 안됨. 그래서 스타트에서 해줘야함. 
    }
    
    private void SetColor(float _alpha){ // 아이템 이미지 알파값 변경하는 함수(빈 슬롯에는 아이템 이미지를 투명하게 해야함. 안그러면 흰 네모가 보이니까. )
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color; 

    }
    
    public void AddItem(Item _item, int _count = 1){ // 습득한 이이템이랑 기본 수량 받기
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment){
            go_CountImage.SetActive(true);
            itemCountText.text = itemCount.ToString();
        }else{
            itemCountText.text = "0";
            go_CountImage.SetActive(false);
        }
        SetColor(1); // 투명도 높이기 
    }

    public void SetSlotCount(int _count){ // 슬롯 아이템 수 조정하는 함수
        itemCount += _count;
        itemCountText.text = itemCount.ToString();

        if(itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot(){ //슬롯 초기화 
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        itemCountText.text = "0";
        go_CountImage.SetActive(false);
    }

    //IPointerClickHandler 상속하면 꼭있어야하는 함수 
    public void OnPointerClick(PointerEventData eventData){

        if(eventData.button == PointerEventData.InputButton.Right){ // 본 스크립트를 적용한 객체에 마우스 포인터를 가져다 대고, 마우스 우클릭하면 이 조건문에 만족해서 이벤트 실행
            if(item != null){ // 아이템이 있는지 확인
                if(item.itemType == Item.ItemType.Equipment){
                    //장비면 착용 구현 안함. 
                }   
                else{ // 장비 아니면 소모 
                    Debug.Log(item.itemName + "을 사용했습니다.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    //DragHandler 상속하면 꼭있어야하는 함수 
    public void OnBeginDrag(PointerEventData eventData){
        if(item != null){ // 아이템이 있으면 
            
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData){
        //계속 마우스 위치 따라감 
        DragSlot.instance.transform.position = eventData.position; //eventData.position : 이벤트가 발생한 객체의 위치 
    }

    //이건 슬롯1에서 드레그 시작해서 다시 슬롯1에서 끝나는 경우, 슬롯1에서 드래그 시작해서 아예 인벤 밖에서 끝나는 경우 에 호출됨. 
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("OnEndDrop");
        //드레그가 끝나면 원래 위치로 돌아와야함. 
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    //이건 슬롯 1에서 드레그를 시작해서 1이 아닌 다른 슬롯위에서 드레그를 끝내면 호출됨. enddrag랑 다름. 그래서 여기서 changeSlot을 호출하는것.
    public void OnDrop(PointerEventData eventData){
        Debug.Log("OnDrop");
        if(DragSlot.instance.dragSlot != null){ // 빈 슬롯을 드래그할 수 있잖아. 빈슬롯이면 아이템 위치 바꾸는 그런거 안되도록!
            ChangeSlot(); //아이템 슬롯 위치 바꾸는 함수 
        }        
    } 

    private void ChangeSlot(){ // 인벤 아이템 슬록 위치 바꾸는 함수 
        //B 사본 만들어두기 
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount); // A를 B에 덮어쓰기

        if(_tempItem != null){
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount); //B를 A에 덮어쓰기 
        }else{ // B와 교체할 A가 없다면 
            DragSlot.instance.dragSlot.ClearSlot(); // 슬롯 비우기 
        }

    }

    
}
