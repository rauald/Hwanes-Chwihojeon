using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    // ���� �Ǿ��ִ��� �ȵǾ��ִ��� Ȯ��
    public bool save;
    // �÷��̾�
    // ��ġ
    public Vector2 curPosition;
    // ���
    public int sceneIdx;
    // ���
    public int curGold;
    // ���� �ٿ��
    public string curMap;
    //public 
    // ���� true false
    public bool isTown;
    // �ο� true false
    public bool isFight;
    // ����
    public List<int> consumList = new List<int>();
    public List<int> consumCount = new List<int>();

    // �⺻
    public string[] charName = new string[3];         // ĳ���� �̸�
    public int[] level = new int[3];                  // ����
    public int[] maxHP = new int[3];                  // �ִ� ü��
    public int[] curHP = new int[3];                  // ���� ü��
    public int[] maxMP = new int[3];                  // �ִ� ����
    public int[] curMP = new int[3];                  // ���� ����
    public int[] maxEXP = new int[3];                 // �ִ� ����ġ
    public int[] curEXP = new int[3];                 // ���� ����ġ

    public int[] atk = new int[3];                 // ���ݷ�
    public int[] def = new int[3];                 // ����
    public int[] hit = new int[3];                 // ����� (���߷�)
    public int[] dodge = new int[3];               // ���߷� (ȸ����)
    public int[] cri = new int[3];                 // �� (ũ��Ƽ�� Ȯ��)

    public List<int> weaponList1 = new List<int>();          // ĳ���Ͱ� ������ �ִ� ���� ���
    public List<int> weaponList2 = new List<int>();          // ĳ���Ͱ� ������ �ִ� ���� ���
    public List<int> weaponList3 = new List<int>();          // ĳ���Ͱ� ������ �ִ� ���� ���

    //public List<Equip> weaponList1 = new List<Equip>();          // ĳ���Ͱ� ������ �ִ� ���� ���
    //public List<Equip> weaponList2 = new List<Equip>();          // ĳ���Ͱ� ������ �ִ� ���� ���
    //public List<Equip> weaponList3 = new List<Equip>();          // ĳ���Ͱ� ������ �ִ� ���� ���

    public List<int> armorList1 = new List<int>();           // ĳ���Ͱ� ������ �ִ� ���� ���
    public List<int> armorList2 = new List<int>();           // ĳ���Ͱ� ������ �ִ� ���� ���
    public List<int> armorList3 = new List<int>();           // ĳ���Ͱ� ������ �ִ� ���� ���

    //public List<Equip> armorList1 = new List<Equip>();           // ĳ���Ͱ� ������ �ִ� ���� ���
    //public List<Equip> armorList2 = new List<Equip>();           // ĳ���Ͱ� ������ �ִ� ���� ���
    //public List<Equip> armorList3 = new List<Equip>();           // ĳ���Ͱ� ������ �ִ� ���� ���

    // ���
    public int[] curWeapon = new int[3];             // ����
    public int[] curArmor = new int[3];              // ��

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