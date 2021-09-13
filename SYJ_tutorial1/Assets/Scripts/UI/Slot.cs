using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; // 획득한 단서 
    public Image itemImage; // 단서 이미지

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void SetColor(float _alpha){ // 아이템 이미지 알파값 변경하는 함수(빈 슬롯에는 아이템 이미지를 투명하게 해야함. 안그러면 흰 네모가 보이니까. )
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color; 
    }

    public void AddItem(Item _item){ 
        item = _item;
        if(_item.isFrontFound){
            Debug.Log("front에서 단서추가 ");
            itemImage.sprite = item.itemFrontImage; 
        }
        else if(_item.isSitFound){
            Debug.Log("sit에서 단서추가 ");
            itemImage.sprite = item.itemSitImage; 
        }
        else if(_item.isTiptoeFound){
            Debug.Log("tiptoe에서 단서추가 ");
            itemImage.sprite = item.itemTiptoeImage; 
        }
        
        SetColor(1); // 투명도 높이기 
    }


}
