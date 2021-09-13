using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ActionController : MonoBehaviour
{
    //Raycast
    [SerializeField] private float range; // 조사 범위 
    private RaycastHit hitInfo; // 충돌체 정보 
    [SerializeField] private LayerMask layerMask; // Ray가 반응할 레이어 

    //상태변수
    private bool investigateActivate = false; // 조사 가능한 상태 
    public bool isSit = false; // 앉음 상태인지
    public bool isTiptoe = false; // 까치발 상태인지 
    private bool isLogActivate = false;
    
    //필요한 변수 
    [SerializeField] private GameObject InvestigateLogBase;
    [SerializeField] private  Proviso theProviso;
    private InvestigateLog theInvestigateLog;

    void Start(){
        theInvestigateLog = FindObjectOfType<InvestigateLog>();
        theProviso = FindObjectOfType<Proviso>();
    }

    void Update(){
        CheckItem();
        CheckPlayerMove();
        TryInvestigate();
        TrySit();
        TryTipteo();
    }

    // 조사할 아이템이 있는지 체크하는 함수 
    private void CheckItem(){ 
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask)){ // 범위안에 속하고
            if (hitInfo.transform.tag == "Item"){ // 아이템 태그인 경우
                investigateActivate = true;
                Debug.Log("info:" + hitInfo.transform.GetComponent<ItemInvestigate>().item.itemName);
            }
        }
        else{
            investigateActivate = false;
        }
    }

    private void TryInvestigate(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isLogActivate){
                InvestigateLogBase.SetActive(false);
                isLogActivate = false;
            }
            else{
                isLogActivate = true;
                InvestigateLogBase.SetActive(true);
                CheckInvestigate(hitInfo.transform.GetComponent<ItemInvestigate>().item); // 단서 찾았는지 상태변수에 반영  , 슬롯에 단서 추가 
                theInvestigateLog.SetLogText(hitInfo.transform.GetComponent<ItemInvestigate>().item); // 조사 log text 설정
                // 찾은 단서를 조사수첩에 추가  
            }
        }
    }

    private void CheckInvestigate(Item _item){ // 단서를 찾았으면 상태변수에 true 체크하는 함수  
        if(isSit){
            if(_item.sit){
                if(!_item.isSitFound){
                    _item.isSitFound = true;
                    theProviso.AcquireItem(_item);
                } 
            }
        }
        else if(isTiptoe){
            if(_item.tiptoe){
                if(!_item.isTiptoeFound){
                    _item.isTiptoeFound = true;
                    theProviso.AcquireItem(_item);
                } 
            }
        }
        else{
            if(_item.front){
                if(!_item.isFrontFound){
                    _item.isFrontFound = true;
                    theProviso.AcquireItem(_item);
                } 
            }
        }
    }

    private void TrySit(){
        if(Input.GetKeyDown(KeyCode.K)){
            if(isSit){
                isSit = false;
            }
            else{
                
                isSit = true;
                isTiptoe = false;
            }
        }
    }
    private void TryTipteo(){
        if(Input.GetKeyDown(KeyCode.L)){
            if(isTiptoe){
                isTiptoe = false;
            }
            else{
                isTiptoe = true;
                isSit = false;
            }
        }
    }

    private void CheckPlayerMove(){
        if(isTiptoe || isSit){
            GameManager.canPlayerMove = false;
        }
        else GameManager.canPlayerMove = true;
    }
}