using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Itme/item")]
public class Item : ScriptableObject
{
    public string itemName; // 이름 
    public ItemType itemType; //아이템 타입
    public Sprite itemImage; // 인벤에 들어갈 아이템 이름 (image: 캔버스에서만 띄울수있음. sprite: 캔버스 필요없이 월드상에서 직접 출력 가능. (컴터 모델에 화면 이미지라던가 ))
    public GameObject itemPrefab; // 아이템을 월드에 떨굴때 그 실체 
    public string weaponType; //무기 유형
    public enum ItemType{
        Equipment,
        Used,
        Ingredient,
        ETC
    }
}
