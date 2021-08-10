using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //조가 가능한 최대 거리 

    private bool investigateActivate = false; // 조사 가능할 시 true
    private bool itemImageActivate = false; // 이미지 활성화 여부 

    private RaycastHit hitInfo; // 충돌체 정보 저장 

    [SerializeField]
    private LayerMask layerMask; // 아이템 레이어에만 반응하도록 레이어마스크 설정

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Image itemImage;
    

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction(){ // 조사하기 누름 
        if(Input.GetButtonDown("Investigate")){
            if(!itemImageActivate){
                itemImageActivate = !itemImageActivate;
                CheckItem();
                CanInvestigate();
                //TurnOffItemImage();
            }
            else{
                itemImageActivate = !itemImageActivate;
                ItemImageDisappear();
            }
        }
    }
    
    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)){ // 범위안에 속하고
            if(hitInfo.transform.tag == "Item"){ // 아이템 태그인 경우
                ItemInfoAppear();// text 보여줌
            }
        }
        else{ 
            InfoDisappear();// text 사라짐
            ItemImageDisappear();
        }
    }

    private void ItemInfoAppear(){
        investigateActivate = true; //이제 조사 가능 
        actionText.gameObject.SetActive(true); // text활성화 
        actionText.text = hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName + "조사하기"; // text 내용 수정
        //image 내용 수정 
    }
    
    private void InfoDisappear(){
        investigateActivate = false; //조사 불가능 
        actionText.gameObject.SetActive(false); // text 비활성화 
    }

    private void CanInvestigate(){
        if(investigateActivate){ // 조사가 가능하고
            if(hitInfo.transform != null){ // 정보가 있을 경우(혹시 모를 오류 방지) 
                // 조사함 로그
                Debug.Log("investigate :"+hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName);
                ShowItemImage();
            }
            else{
                ItemImageDisappear();
            }
        }
    }

    private void ShowItemImage(){
        itemImage.sprite = hitInfo.transform.GetComponent<ItemInvestigate>().item.itemImage; // text 내용 수정
        itemImage.gameObject.SetActive(true); // image 활성화 
        actionText.gameObject.SetActive(false); // text 비활성화 
        itemImageActivate = true;
    }

    private void ItemImageDisappear(){
        itemImage.gameObject.SetActive(false); // image비활성화
        itemImageActivate = false;
    }

    private void TurnOffItemImage(){
        if(Input.GetKeyDown("z")){
            Debug.Log("go back");
            ItemImageDisappear();
        }
    }
}
