using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    // 저장 되어있는지 안되어있는지 확인
    public bool save;
    // 플레이어
    // 위치
    public Vector2 curPosition;
    // 장소
    public int sceneIdx;
    // 골드
    public int curGold;
    // 현재 바운드
    public string curMap;
    //public 
    // 마을 true false
    public bool isTown;
    // 싸움 true false
    public bool isFight;
    // 물약
    public List<int> consumList = new List<int>();
    public List<int> consumCount = new List<int>();

    // 기본
    public string[] charName = new string[3];         // 캐릭터 이름
    public int[] level = new int[3];                  // 레벨
    public int[] maxHP = new int[3];                  // 최대 체력
    public int[] curHP = new int[3];                  // 현재 체력
    public int[] maxMP = new int[3];                  // 최대 마나
    public int[] curMP = new int[3];                  // 현재 마나
    public int[] maxEXP = new int[3];                 // 최대 경험치
    public int[] curEXP = new int[3];                 // 현재 경험치

    public int[] atk = new int[3];                 // 공격력
    public int[] def = new int[3];                 // 방어력
    public int[] hit = new int[3];                 // 기술력 (명중률)
    public int[] dodge = new int[3];               // 순발력 (회피율)
    public int[] cri = new int[3];                 // 운 (크리티컬 확률)

    public List<int> weaponList1 = new List<int>();          // 캐릭터가 가지고 있는 무기 목록
    public List<int> weaponList2 = new List<int>();          // 캐릭터가 가지고 있는 무기 목록
    public List<int> weaponList3 = new List<int>();          // 캐릭터가 가지고 있는 무기 목록

    //public List<Equip> weaponList1 = new List<Equip>();          // 캐릭터가 가지고 있는 무기 목록
    //public List<Equip> weaponList2 = new List<Equip>();          // 캐릭터가 가지고 있는 무기 목록
    //public List<Equip> weaponList3 = new List<Equip>();          // 캐릭터가 가지고 있는 무기 목록

    public List<int> armorList1 = new List<int>();           // 캐릭터가 가지고 있는 갑옷 목록
    public List<int> armorList2 = new List<int>();           // 캐릭터가 가지고 있는 갑옷 목록
    public List<int> armorList3 = new List<int>();           // 캐릭터가 가지고 있는 갑옷 목록

    //public List<Equip> armorList1 = new List<Equip>();           // 캐릭터가 가지고 있는 갑옷 목록
    //public List<Equip> armorList2 = new List<Equip>();           // 캐릭터가 가지고 있는 갑옷 목록
    //public List<Equip> armorList3 = new List<Equip>();           // 캐릭터가 가지고 있는 갑옷 목록

    // 장비
    public int[] curWeapon = new int[3];             // 무기
    public int[] curArmor = new int[3];              // 방어구

    public void SaveCharacter(int _idx, Character _char)
    {
        charName[_idx] = _char.objectName;
        level[_idx] = _char.level;
        maxHP[_idx] = _char.maxHP;
        curHP[_idx] = _char.curHP;
        maxMP[_idx] = _char.maxMP;
        curMP[_idx] = _char.curMP;
        maxEXP[_idx] = _char.maxEXP;
        curEXP[_idx] = _char.curEXP;
        atk[_idx] = _char.atk;
        def[_idx] = _char.def;
        hit[_idx] = _char.hit;
        dodge[_idx] = _char.dodge;
        cri[_idx] = _char.cri;

        if(_idx == 0)
        {
            weaponList1 = _char.weaponIdx;
            armorList1 = _char.armorIdx;
        }
        else if (_idx == 1)
        {
            weaponList2 = _char.weaponIdx;
            armorList2 = _char.armorIdx;
        }
        else if (_idx == 2)
        {
            weaponList3 = _char.weaponIdx;
            armorList3 = _char.armorIdx;
        }

        curWeapon[_idx] = _char.curWeaponIdx;
        curArmor[_idx] = _char.curArmorIdx;
    }
    
    public void SaveConsum(ConsumData _consumData)
    {
        consumList.Clear();

        consumList.Add(_consumData.idx);
        consumCount.Add(_consumData.count);
    }
}