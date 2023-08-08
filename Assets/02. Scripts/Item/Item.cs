using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemBasic")]
public class Item : ScriptableObject
{
    // 아이템 타입
    public enum Type
    {
        WEAPON,
        ARMOR,
        CONSUMPTION
    }
    public Type type;
    // 아이템 이름
    public string itemName;
    // 아이템 이미지
    public Sprite sprite;
    // 아이템 순서
    public int idx;
    // 아이템 설명
    public string description;
}