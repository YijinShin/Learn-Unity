using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //아이템 획득 가능한 범위 
    private bool pickupActivate = false; // 습득 가능할 시 true;
    private RaycastHit hitInfo; //충돌체 정보 저장
    
    [SerializeField]
    private LayerMask layerMask; // 특정 레이어에 대해서만 반응하게 하도록 하기 위해 필요 
    
    [SerializeField]
    private Text actionText;  
    [SerializeField]
    private Inventory theInventory;
    void Start()
    {
        
    }

    void Update()
    {
        CheckItem();
        TryAction();        
    }

    private void TryAction(){
        if(Input.GetKeyDown(KeyCode.E)){
            CheckItem(); //아이템 있는지 없는지 확인
            CanPickUP(); //아이템 줍기  

        }
    }

    private void CheckItem(){
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo,range)){
            if(hitInfo.transform.tag == "Item"){
                ItemInfoAppear();
            }
        }
        else 
            InfoDisappear();
    }

    private void ItemInfoAppear(){
        pickupActivate = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "줍기(E)";
    }

    private void InfoDisappear(){
        pickupActivate = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUP(){
        if(pickupActivate){
            if(hitInfo.transform != null){
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득");

                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                
                Destroy(hitInfo.transform.gameObject); // 충돌체 파괴
                InfoDisappear();
            }
        }
    }
}
