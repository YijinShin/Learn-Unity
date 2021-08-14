using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //무기 중복 교체 실행 방지. : 한번 교체를 하면 true로 바뀌고 true로 바뀐 동안에는 교체 불가능. 일정시간이 흐르면 다시 false로 바꿀거임. 그떄 다시 교체 가능. 
    public static bool isChangeWeapon = false; //static : 한 스크립트 안에 b라는 변수가 있고, 그 스크립트를 3개의 객체에 줬을 때, static으로 하면 세 객체의 b라는 변수는 다 공유됨. 한쪽에서 바꾸면 다른쪽에서도 바뀜. 
    //공유자뭔, 클래스변수 = 정적변수. 타 스크립트에서 사용가능. WeaponManager.isChangeWeapon = treu;이런식으로 사용가능. 

    //딜레이
    [SerializeField]
    private float changeWeaponDelayTime; // 교체하는데 걸리는 시간
    [SerializeField]
    private float changeWeaponEndDelayTime; // 무기 교체가 완전히 끝난 시점. 

    //무기 관리
    [SerializeField]
    private Gun[] guns; // 모든 무기 종류
    [SerializeField]
    private Hand[] hands; //맨손, 너클 등

    //관리     
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>(); // gun 0,1,이렇게 인덱스로 부르는 것 보다 이름으로 부르는게 편하니까 각 인덱스마다 이름을 지어주는 것. 
    private Dictionary<string, Hand> handDictionary = new Dictionary<string, Hand>();

    //필요한 컴포넌트 
    [SerializeField]
    private string currentWeaponType; // 현재 무기 타입. 총, 도끼, 손 등 (총이면 정조준이나 UI 이런것들 활성화 해줘야하니까 )
    public static Transform curretnWeapon; //현재 무기 
    public static Animator currentWeaponAnimator;// 애니메이션 
    [SerializeField]
    private GunController theGunController; // 건이면 핸드 컨트롤러 끄고 이것만 킴 
    [SerializeField]
    private HandController theHandController; // 핸드면 건 컨트롤러 끄고 이것만 킴(안그러면 손인데 우클릭하면 총소리 들리고 그러까.)
    void Start()
    {
        //dictionary사용법
        //gunDictionary.Add("AK47", guns[0]); 이렇게 선언하고 
        //gunDictionary["AK47"] 이렇게 꺼내쓸 수 있다.
        //dictionary에 값 넣기 
        for(int i=0;i<guns.Length;i++){ 
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for(int i=0;i<hands.Length;i++){ 
            handDictionary.Add(hands[i].handName, hands[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Inventory.inventoryActivated){
            if(!isChangeWeapon){
                if(Input.GetKeyDown(KeyCode.Alpha1)){//숫자 1이 눌리면 무기 교체 (서브머신건1)
                    StartCoroutine(ChangeWeaponCoroutine("HAND", "맨손"));            
                }
                else if(Input.GetKeyDown(KeyCode.Alpha2)){ // 숫자 2가 눌리면 맨손
                        StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));            
                }
            }
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name){ // type:총, 손,  name: 어떤 총    
        isChangeWeapon = true;
        currentWeaponAnimator.SetTrigger("Weapon_out"); // 손을 집어 넣는 애니메이션
        yield return new WaitForSeconds(changeWeaponDelayTime); // 손 집어넣는 시간 만큼 딜레이

        CancelPreWeaponAction(); // 바꾸기 전에 있는 무기 행동 캔슬(총인데 정조준하고있었으면 다시 정조준 해제하고 원래 상태로 돌아온 후에 교체해야함. )
        //여기까지하면 이전 무기 행동 다 취소하고 집어넣은것까지

        WeaponChange(_type, _name); // 원하는 무기 꺼내기 
        yield return new WaitForSeconds(changeWeaponEndDelayTime); // 교체 완료 시간 만큼 딜레이

        currentWeaponType = _type;

        isChangeWeapon = false; // 다시 무기 교체 가능하게 
    }

    private void CancelPreWeaponAction(){
        switch(currentWeaponType){
            case "GUN": // 총인 경우 
                theGunController.CancelFineSight(); // 정조준 해제
                theGunController.CancelReload(); // 재장전 해제 
                break;
            case "HAND":
                break;
        }
    }

    private void WeaponChange(string _type, string _name){
        if(_type == "GUN")
            theGunController.GunChange(gunDictionary[_name]);
        else if(_type == "HAND")
            theHandController.HandChange(handDictionary[_name]);
    }
}
