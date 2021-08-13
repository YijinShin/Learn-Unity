using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    //현재 장착된 Hand타입 
    [SerializeField]
    private Hand currentHand;
    //상태변수
    private bool isAttack = false; //공격중
    private bool isSwing = false;//팔을 휘두르는중
    
    private RaycastHit hitInfo ;//쏜 레이저에 닿은 대상의 정보를 가져옴. (상대의 스크립트를 얻어오는 듯)

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    private void TryAttack(){
        if(Input.GetButton("Fire1")){ //좌클릭 
            if(!isAttack){
                //코루틴 실행
                StartCoroutine("AttackCoroutine");
            }
        }
    }
    IEnumerator AttackCoroutine(){
        isAttack = true;
        //에니메이션
        currentHand.anim.SetTrigger("Attack");//에니메이터의 Attack trigger를 발동시킴 
        yield return new WaitForSeconds(currentHand.attacDelayA); // attack delayA만큼 wait시키기(주먹을 다 뻗을때까지 기달)
        isSwing = true;//위의 기다림이 끝나면 이제 공격 들어감
        //공격 적중 여부 판단
        StartCoroutine("HitCoroutine");
        //공격 활성화 시점
        yield return new WaitForSeconds(currentHand.attacDelayB); // 팔 접을 떄까지 기달
        isSwing = false;//위의 기다림이 끝나면 이제 팔 다 접은것. 

        yield return new WaitForSeconds(currentHand.attacDelay - currentHand.attacDelayA -currentHand.attacDelayB); //다음 공격을 실행가능한 시간 전까지의 대기 
        isAttack = false;
    }

    IEnumerator HitCoroutine(){ // 공격적중여부 확인
        while(isSwing){            
            if(CheckObject()){
                Debug.Log("hit coroutine");
                //충돌됨
                isSwing = !isSwing;//한번 적중하면 꺼주기
                Debug.Log("Log:"+hitInfo.transform.name); //충돌한 object의 이름 프린트
            }
            yield return null;
        }
    }
    
    private bool CheckObject(){
        //레이저를 쏜 위치에 물체가 있으면 true반환
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range)){//플레이어 위치, 플레이어 정면방향,충돌체의 정보를 저장할 것 지정, 범위
            return true;
        }
        return false;
    }
    
    public void HandChange(Hand _hand){
        if(WeaponManager.curretnWeapon != null){// 뭔가를 들고있는 경우 기존에 있던거 비활 해야함. 
            WeaponManager.curretnWeapon.gameObject.SetActive(false); // 기존꺼 안보이게 함.
        } 
        currentHand = _hand; // 새 총 받아오기
        WeaponManager.curretnWeapon = currentHand.GetComponent<Transform>(); //Gun을 Transform 컴포넌트형태로 바꿔줘야함. 
        
        WeaponManager.currentWeaponAnimator = currentHand.anim; // 애니메이션 넣기. 이걸 여기로 가져오면 각 컴포넌트에서 애니메이션 처리 할 필요 없이 weaponManager에서 한번에 처리 가능. 
        
        currentHand.transform.localPosition = Vector3.zero; // 포지션 다시 초기화 시키고
        currentHand.gameObject.SetActive(true); // 눈에 보이게 함. 
    }
}
