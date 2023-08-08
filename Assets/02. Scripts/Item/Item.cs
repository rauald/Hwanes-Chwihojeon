using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemBasic")]
public class Item : ScriptableObject
{
    // ������ Ÿ��
    public enum Type
    {
        WEAPON,
        ARMOR,
        CONSUMPTION
    }
    public Type type;
    // ������ �̸�
    public string itemName;
    // ������ �̹���
    public Sprite sprite;
    // ������ ����
    public int idx;
    // ������ ����
    public string description;
}