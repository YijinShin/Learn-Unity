using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public ItemType itemType;

    //image 
    public Sprite itemFrontImage;
    public Sprite itemSitImage;
    public Sprite itemTiptoeImage;
    
    //상태변수 
    public bool isFrontFound = false;  // 단서 발견 여부 
    public bool isSitFound = false;  // 단서 발견 여부 
    public bool isTiptoeFound = false;  // 단서 발견 여부 
    public bool sit;  // 앉음 상태 조사 가능 여부 
    public bool tiptoe;  // 까치발 상태 조사 가능 여부 
    public bool front; // 정면 상태 조사 가능 여부 

    // log
    public string sitLog;
    public string tiptoeLog;
    public string frontLog;


    public enum ItemType{
        normal,
        PM,
    }
}