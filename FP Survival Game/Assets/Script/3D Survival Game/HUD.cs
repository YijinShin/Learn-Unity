using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 이거 써야지 UI 객체 사용가능. 

public class HUD : MonoBehaviour
{
    //필요한 컴포넌트 
    [SerializeField]
    private GunController theGunController; // 이 안에 Gun 객체인 currenGun이 있잖아. 그걸
    private Gun currentGun; // 여기에 넣어줄거임. 그러면 매번 컨트롤러에서 가지고오지 않아도 됨. 
    
    //필요하면 HUD호출. 필요없으면 HUD비활성화 
    [SerializeField]
    private GameObject GO_BulletHUD; 

    [SerializeField]
    private Text[] text_Bullet; // text에 총알 갯수 반영

    void Update()
    {
        CheckBullet();        
    }

    private void CheckBullet(){ 
        currentGun = theGunController.GetGun(); // curretnGun 받아오기
        text_Bullet[0].text = currentGun.carrayBulletCount.ToString(); // 소유한 총 총알수 text는 스트링만 받음.
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString(); // 한번 장전 가능한  총알수
        text_Bullet[1].text = currentGun.currentBulletCount.ToString(); // 장전된 총알수 
    }
}
