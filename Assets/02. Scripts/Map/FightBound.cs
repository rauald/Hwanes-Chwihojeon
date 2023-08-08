using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBound : MonoBehaviour
{
    // �Ѿ������ �ٽ� �ٿ�� ���� ��
    private CompositeCollider2D bound;

    private void Awake()
    {
        bound = this.GetComponent<CompositeCollider2D>();
    }

    private void Start()
    {
        PlayerManager.instance.fightBound = bound;
    }
}