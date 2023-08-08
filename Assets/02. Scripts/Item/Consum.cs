using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consum", menuName = "Item/Consum")]
public class Consum : Item
{
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
}