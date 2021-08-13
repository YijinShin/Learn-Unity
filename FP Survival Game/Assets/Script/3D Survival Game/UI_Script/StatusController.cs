using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    //체ㄹ력
    [SerializeField]
    private int hp; 
    private int currentHp;
    //스태미나 
    [SerializeField]
    private int sp;
    private int currentSp;
    [SerializeField]
    private int spIncreaseSpeed; // 스테미나 증가량 
    [SerializeField]
    private int spRechargeTime; // 스테미나 재회복 딜레이. 한번 깎이면 조금 딜레이 후에 다시 차기 시작.
    private int currentSpRechargeTime; // 이게 현재 남은 스테미나 회복 딜레이 타임. 이걸 1씩 깎으면서 딜레이 계산하는것. 
    private bool spUsed; // 스테미나 감소 여부. 달면 얘를 true로 바꾸고 그럼 딜레이 계산이 시작됨. 
    
    //방어력
    [SerializeField]
    private int dp;
    private int currentDp;
    
    //배고픔
    [SerializeField]
    private int hungry;
    private int currentHungry;
    [SerializeField]
    private int hungryDecreaseTime; // 배고픔이 줄어드는 속도
    private int currentHungryDecreaseTime; // 계산 
    
    //목마름
    [SerializeField]
    private int thirsty;
    private int currentThirsty;
    [SerializeField]
    private int thirstyDecreaseTime; // 목마름이 줄어드는 속도
    private int currentThirstyDecreaseTime; // 계산 

    //만족도
    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    //필요한 컴포넌트 
    [SerializeField]
    private Image[] images_Gauge; //게이지6개 이미지 받아오기 
    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5; // 키잉
    
    
    void Start()
    {
        //현 게이지 초기화 
        currentHp = hp;
        currentSp = sp;
        currentDp = dp;
        currentThirsty = thirsty;
        currentHungry = hungry;
        currentSatisfy = satisfy;
    }

    // Update is called once per frame
    void Update()
    {
        Hungry(); // 
        Thirsty();
        GaugeUpdate(); //이미지에 계산한 게이지 반영 
    }

    private void Hungry(){ // 배고픔 게이지 
        if(currentHungry > 0){ // 만약 배고픔 게이지가 0보다 크면 일정시간마다 깎아야함. 
            if(currentHungryDecreaseTime <= hungryDecreaseTime){ // currenttime이 딜레이 타임보다 작으면
                currentHungryDecreaseTime ++; // 올려주고
            }
            else{ //같거나 넘으면 이제 딜레이 끝난거니까 
                currentHungry --; //배고픔 게이지 감소 
                currentHungryDecreaseTime = 0; // 딜게이 계산 변수 초기화 
            }
        }
        else{
            Debug.Log("Hungry = 0");
        }
    }

    private void Thirsty(){ // 목마름 게이지 
        if(currentThirsty > 0){ // 만약 목마름 게이지가 0보다 크면 일정시간마다 깎아야함. 
            if(currentThirstyDecreaseTime <= thirstyDecreaseTime){ // currenttime이 딜레이 타임보다 작으면
                currentThirstyDecreaseTime ++; // 올려주고
            }
            else{ //같거나 넘으면 이제 딜레이 끝난거니까 
                currentThirsty --; //목마름 게이지 감소 
                currentThirstyDecreaseTime = 0; // 딜게이 계산 변수 초기화 
            }
        }
        else{
            Debug.Log("Thirsty = 0");
        }
    }

    private void GaugeUpdate(){
        images_Gauge[HP].fillAmount = (float)currentHp / hp;  // 0-1사이 값으로 해야하니까 현체력/최대체력 으로 계산해주기.
        images_Gauge[DP].fillAmount = (float)currentDp / dp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
        images_Gauge[SATISFY].fillAmount = (float)currentSatisfy / satisfy;
    }

    public void DecreaseStamina(int _count){
        spUsed = true;
        currentSpRechargeTime = 0;

        if(currentSp - _count > 0){
            currentSp -= _count;
        }else{
            currentSp = 0;
        }
    }

    private void SPRechargeTime(){ //sp 채우는 함수 
        if(spUsed){
            if(currentSpRechargeTime < spRechargeTime){ // 아직 딜레이 시간이 다 안갔을 경우 
                currentSpRechargeTime ++; // 딜레이 시간 계산
            }
            else{
                spUsed = false; // 딜레이 시간 다 차면 sp회복할 수 있게 spUsed를 false로 해준다. 
            }
        }
    }

    private void SPRecover(){
        if(!spUsed && currentSp < sp){ // spUsed

        }
    }

}
