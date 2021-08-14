using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    //현재 장착된 총 
    [SerializeField]
    private Gun currentGun; //현재 들고있는 gun 
    private float currentFireRate; // 딜레이를 여기에 넣어둘건데, 이 숫자를 계속 마이너스 하다가 0이되면 다시 발사할 수 있도록 할거임. 
    private AudioSource audioSource; //mp3 플레이어 같은것 
    private bool isReload; // 재장전 중인지 표현하는 상태변수 

    //정조준
    [SerializeField]
    private Vector3 originPos;//본래 위치 값
    private bool isFineSightMode = false; //정조준 모드인지 확인하는 상태변수

    //피격
    private RaycastHit hitInfo; //충돌체 정보 
     [SerializeField]
    private Camera theCam; // 게임화면이 카메라 시점이니까 총알이 조준되는건 카메라 기준 정중앙

    [SerializeField]
    private GameObject hitEffectPrefab; // 피격 이팩트 
    


    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();

        WeaponManager.curretnWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnimator = currentGun.anim;
    }
    void Update()
    {
        GunFireRateCalc(); // 딜레이 깎기
        if(!Inventory.inventoryActivated){
            TryFire(); // 발사 시도
        }   
        TryReload(); // 수동 재장전 
        TryFineSight();
    }

    private void GunFireRateCalc(){ //연사속도 계산 
        if(currentFireRate > 0){ // currentFireRate가 0보다 크면(아직 딜레이가 남아있는 상황)
            currentFireRate -= Time.deltaTime; //deltaTime은 1초에 1식 감소시키는데, update가 매 프레임 마다 실행되니까 매 프레임마다 1씩 currentFireRate를 감소시키는 것
            //deltaTime는 1초의 역수, 즉 1/60를 가짐. 이게 60번 실행되면 1이 되니까 즉 1초에 1씩 감소시키는게 됨. 
        }
    }   

    private void TryFire(){  // 발사 전 계산
        if( Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload){ // 발사 버튼을 누르고있고, 딜레이 시간이 끝났고, 재장전 중이 아닌 경우 발사가능 
            Fire();  
        }
    }

    private void Fire(){ 
        if(!isReload){
            if(currentGun.currentBulletCount > 0 ){ // 총알이 탄창에 있으면 발사 
                Shoot();
            }
            else //총알이 탄창에 없으면 재장전 
                //CancelFineSight(); //정조준 중이라면 해제하고나서 
                StartCoroutine(ReloadCoroutine()); // 재장전 
        }
    }

    private void Shoot(){
        currentGun.currentBulletCount --; // 총알 하나 빼기 
        currentFireRate = currentGun.fireRate; // 딜레이 초기화 
        PlaySE(currentGun.fireSound); // 오디오 클립 전달 
        currentGun.muzzleFlash.Play();
        //피격 처리 
        Hit();
        //반동처리 
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void TryReload(){ //수동 재장전 함수 
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount){
            CancelFineSight(); // 정조준 중이었다면 그걸 해제하고나서 
            StartCoroutine(ReloadCoroutine()); //재장전
        }
    }

    private void PlaySE(AudioClip _clip){ //오디오 재생 함수 
        audioSource.clip = _clip;
        audioSource.Play();
    }
    
    IEnumerator ReloadCoroutine(){ // 재장전 (코루틴으로 해야함)
        isReload = true;
        if(currentGun.carrayBulletCount > 0){ //재장전 실행해야함 
            
            currentGun.anim.SetTrigger("Reload"); //애니메이션 실행 
            
            currentGun.carrayBulletCount +=  currentGun.currentBulletCount; // 재장전을 총알을 다 쓰지 않은 상태에서 했을 경우 아직 탄창에 있는 총알은 그대로 유지하고 부족한 만큼만 재장전 해줘야한다. 
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime); // 재장전 하는 시간 만큼 기다리기 

            if( currentGun.carrayBulletCount >= currentGun.reloadBulletCount){ //  재장전 총알 수 보다 현재 인벤에 가지고있는 총알수가 많은 경우 
                currentGun.currentBulletCount += currentGun.reloadBulletCount; // 장전된 총알 수 더해주기 
                currentGun.carrayBulletCount -= currentGun.reloadBulletCount; // 인벤의 총알 수 빼주기 
            }
            else{ // 재장전 총알 수 보다 현재 인벤에 가지고있는 총알수가 적은 경우 
                currentGun.currentBulletCount += currentGun.carrayBulletCount; // 가지고있는 총알 다 장전 
                currentGun.carrayBulletCount = 0; // 가지고있는 총알 수 0으로 
            }
            isReload = false;
        }
        else{
            Debug.Log("총알 없음");
        }
        
    }

    public void CancelReload(){
        if(isReload){
            StopAllCoroutines();
            isReload = false;
        }
    }

    private void TryFineSight(){
        if(Input.GetButtonDown("Fire2") && !isReload){ // 재장전중에는 정조준 선택못함. 
            Debug.Log("mode change");
            FineSight();
        }
    }   

    public void CancelFineSight(){ // 정조준 하던중에 취소하는 함수(playercontroller쪽에서도 뛸때 정조준 취소를 해야하기 때문에 public으로 함.)
        if(isFineSightMode){// 정조준중에
            FineSight(); //다시 이 함수를 부르면 mode가 false가 되니까 cancel이 된다. 
        }
    }
    private void FineSight(){
        isFineSightMode = !isFineSightMode; //모드 바꾸기 
        currentGun.anim.SetBool("FineSightMode",isFineSightMode); // 에니메이션 

        if(isFineSightMode){//정조준 모드인 경우 
            StopAllCoroutines(); //타 코루틴과 같이 실행되면서 원위치가 자꾸 바뀌는 것을 막기 위해 
            StartCoroutine(FineSightActivateCoroutine()); // 위치 이동 
        }
        else{ //일반 모드인 경우
            StopAllCoroutines();  
            StartCoroutine(FineSightDeactivateCoroutine()); //위치 이동 
        }
    }

    IEnumerator FineSightActivateCoroutine(){ // 원 위치에서 정조준 위치로 이동 
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos){ // 현 위치가 정조준했을 때의 위치와 다른 경우 
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f); //정조준 위치로 옮김
            // lerp(현위치, 목표 위치 ) 현위치가 목표 위치랑 같아질때 까지 0.2f씩 계속 옮김.  
            yield return null; //1프레임씩 계속 대기 
        }   
    }

    IEnumerator FineSightDeactivateCoroutine(){ // 정조준 위치에서 원래 위치로 이동
        while(currentGun.transform.localPosition != originPos){ // 현 위치가 idle일 때의 위치와 다른 경우 
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f); //idle 위치로 옮김
            // lerp(현위치, 목표 위치 ) 현위치가 목표 위치랑 같아질때 까지 0.2f씩 계속 옮김.  
            yield return null; //1프레임씩 계속 대기 
        }   
    }

    IEnumerator RetroActionCoroutine(){
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z); // 정조준 안했을 떄 최대반동 x에 반동값을 주고, 나머지는 그대로 
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, originPos.y, originPos.z); //정조준 시 최대반동

        if(isFineSightMode){ // 정조준 상태인 경우
            currentGun.transform.localPosition = currentGun.fineSightOriginPos; // 현 위치를 원래 위치로 되돌릴거임. 반동때문에 위치가 이동된 상태에서 바로 반동이 들어오면 그 반동이 눈에 잘 안보일거임. 그러니까 반동중에 또 반동이 들어오면 순간적으로 원위치 시키고 바로 반동위치로 이동시키는 것이 눈에 잘보임. 
            //반동시작 
            while(currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f){ // lerp의 단점이 정확히 목표지점까지 딱 일치하는 경우가 잘 없음. 그래서 오차범위로 0.02f를 빼준거임. 안그러면 이 while문에서 탈출을 잘 못함. 
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f); // 반동위치로 이동 
                yield return null; // 대기 
            }
            //원위치
              while(currentGun.transform.localPosition != originPos){ // lerp의 단점이 정확히 목표지점까지 딱 일치하는 경우가 잘 없음. 그래서 오차범위로 0.02f를 빼준거임. 안그러면 이 while문에서 탈출을 잘 못함. 
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f); // 반동위치로 이동 
                yield return null; // 대기 
            }
            
        }
        else{
            currentGun.transform.localPosition = originPos; // 현 위치를 원래 위치로 되돌릴거임. 반동때문에 위치가 이동된 상태에서 바로 반동이 들어오면 그 반동이 눈에 잘 안보일거임. 그러니까 반동중에 또 반동이 들어오면 순간적으로 원위치 시키고 바로 반동위치로 이동시키는 것이 눈에 잘보임. 
            //반동시작 
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f){ // lerp의 단점이 정확히 목표지점까지 딱 일치하는 경우가 잘 없음. 그래서 오차범위로 0.02f를 빼준거임. 안그러면 이 while문에서 탈출을 잘 못함. 
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f); // 반동위치로 이동 
                yield return null; // 대기 
            }
            //원위치
              while(currentGun.transform.localPosition != originPos){ // lerp의 단점이 정확히 목표지점까지 딱 일치하는 경우가 잘 없음. 그래서 오차범위로 0.02f를 빼준거임. 안그러면 이 while문에서 탈출을 잘 못함. 
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f); // 반동위치로 이동 
                yield return null; // 대기 
            }
        }
    }
    private void Hit(){
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range)){//카메라의 월드 좌표로 해야함. 
            //hit effect
            var clone = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); // prefab생성하는 함수 (어떤 프리팹, 어디에(point는 충돌한 정홛한 위치를 반환), LookRotation:특정한 객체를 바라보는 것.normal:충돌할 객체의 표면을 반환)
            //만약 밖을 쐈다면 위를 바라보는 상태로 프리팹이 해당 충돌 위치에 생성될거임. 
            // 이팩트 프리팹을 클론이라는 변수에 넣어서 2초후에 제거되도록 함. 안그러면 메모리에 계속 새로운 이팩트 프리팹이 쌓여서 나중에 메모리가 부족해짐. 
            Destroy(clone, 2f);

        }

    }

    public void GunChange(Gun _gun){
        if(WeaponManager.curretnWeapon != null){// 뭔가를 들고있는 경우 기존에 있던거 비활 해야함. 
            WeaponManager.curretnWeapon.gameObject.SetActive(false); // 기존꺼 안보이게 함.
        } 
        currentGun = _gun; // 새 총 받아오기
        WeaponManager.curretnWeapon = currentGun.GetComponent<Transform>(); //Gun을 Transform 컴포넌트형태로 바꿔줘야함. 
        
        WeaponManager.currentWeaponAnimator = currentGun.anim; // 애니메이션 넣기. 이걸 여기로 가져오면 각 컴포넌트에서 애니메이션 처리 할 필요 없이 weaponManager에서 한번에 처리 가능. 
        
        currentGun.transform.localPosition = Vector3.zero; // 포지션 다시 초기화 시키고
        currentGun.gameObject.SetActive(true); // 눈에 보이게 함. 
    }

    public Gun GetGun(){ // gun 클래스 내의 current건을 다른 스트립트에서 받아갈 수 있도록 하는 함수 .
        return currentGun;
    }
}
