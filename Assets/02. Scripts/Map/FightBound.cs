using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBound : MonoBehaviour
{
    // 넘어왔으니 다시 바운드 받을 값
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