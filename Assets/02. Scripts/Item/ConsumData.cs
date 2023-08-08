using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsumData
{
    public Consum consum;
    // ������ �̸�
    public string itemName;
    // ������ �̹���
    public Sprite sprite;
    // ������ ����
    public int idx = 99;
    // ������ ����
    public string description;
    // ����
    public int count;
    // ü�� ȸ����
    public int healthRecovery;
    // ���� ȸ����
    public int manaRecovery;
    // ��� �����̻� ����
    public bool isCure;
    // �ߵ� �����̻� ����
    public bool isDecoding;
    // ���� �����̻� ����
    public bool isKnock;
    // ��Ȱ
    public bool isResurrection;
    // ����
    public int gold;

    public void ConsumD(Consum _consum)
    {
        consum = _consum;
        itemName = _consum.itemName;
        sprite = _consum.sprite;
        idx = _consum.idx;
        description = _consum.description;
        healthRecovery = _consum.healthRecovery;
        manaRecovery = _consum.manaRecovery;
        isCure = _consum.isCure;
        isDecoding = _consum.isDecoding;
        isKnock = _consum.isKnock;
        isResurrection = _consum.isResurrection;
        gold = _consum.gold;
    }

    public void ConsumClear()
    {
        consum = null;
        itemName = null;
        sprite = null;
        idx = 99;
        description = null;
        healthRecovery = 0;
        manaRecovery = 0;
        isCure = false;
        isDecoding = false;
        isKnock = false;
        isResurrection = false;
        gold = 0;
    }
}