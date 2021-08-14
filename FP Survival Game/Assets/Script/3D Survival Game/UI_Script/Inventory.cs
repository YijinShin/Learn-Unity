using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  //인벤토리 열면 마우스클릭으로 좌우 움직임, 공격 이런거 막을거임. 

    //필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent; // 모든 슬롯의 부모객체
    private Slot[] slots; // 슬롯들


    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); //slots배열안에 하이라키의 모든 slot들이 싹 들어감. 
    }   
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory(){ // 인벤토리 여는 함수
        if(Input.GetKeyDown(KeyCode.I)){
            inventoryActivated = !inventoryActivated;

            if(inventoryActivated){
                OpenInventory();
            }
            else    
                CloseInventory();
        }
    }

    private void OpenInventory(){
        go_InventoryBase.SetActive(true);
    }   

    private void CloseInventory(){
        go_InventoryBase.SetActive(false);   
    }

    public void AcquireItem(Item _item, int _count =1){ // 얻은 아이템의 정보 반영 함수 
        if(_item.itemType != Item.ItemType.Equipment){

            for(int i=0; i<slots.Length ;i++){ // 슬롯의 수만큼 반복. 만약 이미 얻은 아이템이 있다면 갯수만 올려주기 

                if(slots[i].item != null){

                    if(slots[i].item.itemName == _item.itemName){
                        slots[i].SetSlotCount(_count);
                        return;
                    }

                }   
            }
        }

        for(int i=0; i<slots.Length ;i++){ //빈 슬롯에 아이템 추가 
            if(slots[i].item == null){
                slots[i].AddItem(_item, _count);   
                return;
            }
        }
    }
}
