using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consum", menuName = "Item/Consum")]
public class Consum : Item
{
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
}