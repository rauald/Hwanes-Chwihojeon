using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsumData
{
    public Consum consum;
    // 아이템 이름
    public string itemName;
    // 아이템 이미지
    public Sprite sprite;
    // 아이템 순서
    public int idx = 99;
    // 아이템 설명
    public string description;
    // 개수
    public int count;
    // 체력 회복량
    public int healthRecovery;
    // 마나 회복량
    public int manaRecovery;
    // 모든 상태이상 해제
    public bool isCure;
    // 중독 상태이상 해제
    public bool isDecoding;
    // 기절 상태이상 해제
    public bool isKnock;
    // 부활
    public bool isResurrection;
    // 가격
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