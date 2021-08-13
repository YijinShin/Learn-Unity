using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{   
    //조사 
    [SerializeField]
    private float range; // 조사 가능 거리

    private RaycastHit hitInfo; // 충돌체 정보 저장 
    private bool investigateActivate = false; // 조사 가능 여부 


    [SerializeField]
    private LayerMask layerMask; // 아이템 레이어에만 반응할수있도록
    
    //필요한 컴포넌트 

    // Update is called once per frame
    void Update()
    {
        CheckItem(); //아이템 있는지 확인 
        TryAction(); //조사키가 눌리는지 확인 
    }

    private void CheckItem(){
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)){
                            //(어디에서, 어느 방향으로, 정보는 어디에 저장할건지, 레이의 거리, 어떤 레이어)
            if(hitInfo.transform.tag == "Item"){
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();// text 사라짐
            //ItemImageDisappear();
        }
    }
    private void ItemInfoAppear(){
        investigateActivate = true; //이제 조사 가능 
        //AT.SetActive(true); 

    }
     private void InfoDisappear()
    {
        investigateActivate = false; //조사 불가능 
    }
    private void TryAction(){
        if(Input.GetKeyDown(KeyCode.E)){
            CheckItem();//아이템 있는지 확인 
            CanInvestigate();
        }
    }

    private void CanInvestigate()
    {
        if(investigateActivate){
            if(hitInfo.transform != null){
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "조사");
            }
        }
    }
}
