using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Monster
{
    private void OnEnable()
    {
        objectName = "¿ø¼þÀÌ";

        maxHP = 15;
        curHP = maxHP;
        atk = 24;
        def = 12;
        hit = 100;
        dodge = 25;
        cri = 0;

        totalAtk = atk;
        totalDef = def;
        totalHit = hit;
        totalDodge = dodge;
        totalCri = cri;

        exp = 22;
        gold = 3;

        level = 1;

        isDie = false;
    }

    public override void SkillShow()
    {
        curSkill = basicSkill[0];
        base.SkillShow();
    }

    public override IEnumerator SkillMove()
    {
        yield return new WaitForSeconds(0.5f);
        Attack();
        yield return new WaitForSeconds(0.25f);
        MonsterSkillShowEnd();
    }
}