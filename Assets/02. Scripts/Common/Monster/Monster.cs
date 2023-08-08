using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : State
{
    // ���� ��ų ��� ����Ʈ
    public List<Skill> basicSkill = new List<Skill>();
    public List<Skill> targetSkill = new List<Skill>();
    public List<Skill> allSkill = new List<Skill>();

    public SpriteRenderer img;

    public Skill curSkill;          // ���� ���� ��ų

    public int exp;                 // ���Ͱ� �� ����ġ
    public int gold;                // ���Ͱ� �� ���

    private void Start()
    {
        isDie = false;
    }
    /// ����
    public void Attack()
    {
        damage = totalAtk;
        IsCritical();
        if (curSkill.type != Skill.Type.ALL)
        {
            PlayerManager.instance.characterList[target].HitDamage(damage, hit, isCri, curSkill.isSFX);
        }
        else
        {
            for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
            {
                PlayerManager.instance.characterList[i].HitDamage(damage, hit, isCri, curSkill.isSFX);
            }
        }
    }
    public virtual void SkillShow()
    {
        target = Random.Range(0, PlayerManager.instance.characterList.Count);
        curAtkCount = 0;
        // Ÿ������ ���� �׾����� Ȯ��
        PlayerManager.instance.fight.MonReTarget(target);
        ani.SetTrigger(curSkill.aniName);
        StartCoroutine(SkillMove());
    }
    public virtual IEnumerator SkillMove() { yield return null; }

    public void SpaceTime()
    {
        StartCoroutine(SpaceTimeHit());
    }

    private IEnumerator SpaceTimeHit()
    {
        int time = 0;
        while(time < 10)
        {
            sprite.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
            yield return new WaitForSeconds(0.05f);
            time++;
        }
        sprite.color = new Color(255, 255, 255);
    }

    public override void Dead()
    {
        base.Dead();
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        ani.SetTrigger("doDie");
        int loop = 0;
        while (loop < 5)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);

            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
            loop++;
        }

        sprite.enabled = false;

        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            PlayerManager.instance.characterList[i].EXPUp(exp);
        }
        UIManager.instance.info.statusInfo.Status();
        // ��� Ǯ�� Ű��
        PlayerManager.instance.gold += gold;
    }
}