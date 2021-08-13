using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // 이름
    public GameObject itemPrefab; // 프리팹 
    public Sprite itemImage; // 이미지 
    //(스프라이트는 캔버스 필요없고 그냥 화면 띄울수있음.컴퓨터의 화면이미지라던가 넣기 좋음. 이미지는 캔버스 필요)
    public ItemType itemType; // 타입
    public GameObject hint; // 
    public bool hasFound;  // 단서 발견 여부 

    public enum ItemType{
        normal,
        PM,
    }
}
