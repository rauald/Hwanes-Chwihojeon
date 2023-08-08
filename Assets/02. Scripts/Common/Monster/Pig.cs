using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Monster
{
    private void OnEnable()
    {
        objectName = "¸äµÅÁö";

        maxHP = 22;
        curHP = maxHP;
        atk = 30;
        def = 16;
        hit = 100;
        dodge = 28;
        cri = 5;

        totalAtk = atk;
        totalDef = def;
        totalHit = hit;
        totalDodge = dodge;
        totalCri = cri;

        exp = 30;
        gold = 4;

        level = 1;

        isDie = false;
    }
    public override void SkillShow()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) curSkill = basicSkill[0];
        else if (rand == 1) curSkill = targetSkill[0];
        base.SkillShow();
    }

    public override IEnumerator SkillMove()
    {
        string skillName = curSkill.skillName;
        float time = 0;
        GameObject effect = null;

        if (skillName == "¹ÚÄ¡±â")
        {
            yield return new WaitForSeconds(0.5f);
            Attack();
            yield return new WaitForSeconds(0.25f);
        }
        else if (skillName == "µ¹Áø")
        {
            float speed = 0.007f;
            while (time < 30)
            {
                effect = ObjectPoolManager.instance.GetQueueSkill("¸ÕÁö");
                effect.transform.position = this.transform.position;
                this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.characterList[target].transform.position, 1.5f);
                yield return new WaitForSeconds(speed);
                time++;
            }
            Attack();
            yield return new WaitForSeconds(0.2f);
        }
        MonsterSkillShowEnd();
    }
}