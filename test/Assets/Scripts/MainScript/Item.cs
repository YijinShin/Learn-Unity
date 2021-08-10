using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public Sprite itemImage;
    public ItemType itemType;
    public GameObject hint;
    public bool hasFound;  // 단서 발견 여부 

    public enum ItemType{
        normal,
        PM,
    }
}
