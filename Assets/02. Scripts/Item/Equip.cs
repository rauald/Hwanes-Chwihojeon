using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "Item/Equip")]
public class Equip : Item
{
    public enum Character
    {
        Atachoe,
        Linxiang,
        Smash
    }

    public Character character;
    public int atk;
    public int def;
    public int hit;
    public int dodge;
    public int cri;
    public int gold;
    public bool use = false;
}