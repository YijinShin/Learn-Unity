using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Proviso : MonoBehaviour
{
    [SerializeField] private GameObject go_ProvisoPanel; //
    [SerializeField]private GameObject go_SlotsParent; // 모든 슬롯의 부모객체
    private Slot[] slots; // proviso slot  



    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); //slots배열안에 하이라키의 모든 slot들이 싹 들어감. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquireItem(Item _item){ // 얻은 아이템의 정보 반영 함수 
        for(int i=0; i<slots.Length; i++){
            if(slots[i].item == null){
                slots[i].AddItem(_item);
                return;
            }
        }
    }
}
