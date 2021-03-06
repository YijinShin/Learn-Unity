using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerOYJ : MonoBehaviour
{
    // 현재 장탁된 hand형 타입 무기
    [SerializeField]
    private HandOYJ currentHand;

    private bool isAttack = false;
    private bool isSwing = false;
    private RaycastHit hitInfo;

    void Update(){
        TryAttack();
    }

    private void TryAttack(){
        if(Input.GetButton("Fire1")){
            if(!isAttack){
                //코루틴 실행.
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine(){
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        //공격활성화 시점
        StartCoroutine(HitCoroutine());
        
        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayA -currentHand.attackDelayB);
        isAttack =false;

        isAttack = false;
    }

    IEnumerator HitCoroutine(){
        while(isSwing){
            if(CheckObject()){
                
                //충돌됨
                isSwing = !isSwing;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject(){

        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range)){
            return true;
        }
        return false;
    }
}
