using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linxiang : Character
{
    private void Start()
    {
        objectName = "����";

        if (!DataController.instance.gameData.save)
        {
            maxHP = 20;
            curHP = maxHP;
            maxMP = 25;
            curMP = maxMP;
            atk = 15;
            def = 13;
            hit = 130;
            dodge = 30;
            cri = 30;

            curWeapon = weaponList[0];
            weaponList[0].use = true;
            curArmor = armorList[0];
            armorList[0].use = true;

            TotalState();

            level = 1;
            maxEXP = 100;
            curEXP = 0;

            isDie = false;
        }
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        maxHP += 4;
        curHP = maxHP;
        maxMP += 3;
        curMP = maxMP;
        atk += 3;
        def += 2;
        hit += 5;
        dodge += 5;
        cri += 2;

        TotalState();
    }

    public override IEnumerator SkillMove()
    {
        string skillName = curSkill.skillName;
        int skillLevel = curSkill.level;
        float time = 0;
        GameObject effect = null;
        switch (skillName)
        {
            #region �⺻��
            case "�������":
                yield return new WaitForSeconds(0.5f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "����ű":
            case "�̵�ű":
            case "��ű":
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            #endregion
            #region ���� ��ų
            case "�ȸ������":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                while (time < curSkill.atkCount)
                {
                    yield return new WaitForSeconds(0.5f);
                    Attack();
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "���İ�":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                if (skillLevel <= 3)
                {
                    while (time < curSkill.atkCount)
                    {
                        yield return new WaitForSeconds(0.33f);
                        Attack();
                        time++;
                    }
                }
                else
                {
                    while (time < (curSkill.atkCount - 1))
                    {
                        yield return new WaitForSeconds(0.33f);
                        Attack();
                        time++;
                    }
                    time = 0;
                    while (time < 10)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("��");
                        effect.transform.position = this.transform.position;
                        this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(4, 0), 1f);
                        yield return new WaitForSeconds(0.033f);
                        if (time == 7) Attack();
                        time++;
                    }
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "��ȭ������":
                yield return new WaitForSeconds(0.5f);
                effect = ObjectPoolManager.instance.GetQueueSkill("��");
                effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;

                if (skillLevel >= 2)
                {
                    yield return new WaitForSeconds(0.08f);
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(2, 0);
                }
                if (skillLevel >= 3)
                {
                    yield return new WaitForSeconds(0.08f);
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-2, 0);
                }
                if (skillLevel == 4)
                {
                    yield return new WaitForSeconds(0.08f);
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(0, -1);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "�ϰ� �� ����·���":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.25f);
                if (skillLevel == 1)
                {
                    while (time < 10)
                    {
                        if (time % 4 == 0) Attack();
                        effect = ObjectPoolManager.instance.GetQueueSkill("�巡��");
                        effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(Random.Range(-1f, 1f), 0);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                }
                else
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("�巡���");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    int speed = 0;
                    if (skillLevel == 2) speed = 14;
                    else if (skillLevel == 3) speed = 18;
                    else speed = 38;

                    while (time < speed)
                    {
                        if (time % 4 == 0) Attack();

                        for (int i = 0; i < 3; i++)
                        {
                            effect = ObjectPoolManager.instance.GetQueueSkill("�巡��");
                            effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-0.5f + (0.5f * i), 0);
                        }
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                }
                yield return new WaitForSeconds(0.5f);
                break;
            #endregion
            #region ��ü ��ų
            case "����̴޷���":
                SoundManager.instance.PlaySkillSFX("�����");
                if(skillLevel <= 2) yield return new WaitForSeconds(0.5f);
                else if (skillLevel == 3) yield return new WaitForSeconds(0.75f);
                else if (skillLevel == 4) yield return new WaitForSeconds(1f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "���̽���":
                this.transform.position = new Vector3(10, -16);
                if (skillLevel <= 2) yield return new WaitForSeconds(0.5f);
                else if (skillLevel == 3) yield return new WaitForSeconds(0.75f);
                else if (skillLevel == 4) yield return new WaitForSeconds(1f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "������":
                yield return new WaitForSeconds(0.5f);
                SoundManager.instance.PlaySkillSFX("����");
                if(skillLevel == 2 || skillLevel == 3)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("�����");
                    effect.transform.position = this.transform.position + new Vector3(-6, 0);
                    effect = ObjectPoolManager.instance.GetQueueSkill("�����");
                    effect.transform.position = this.transform.position + new Vector3(6, 0);
                }
                if(skillLevel >= 3)
                {
                    float angle;
                    float x;
                    float y;
                    while(time < 10)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");

                        angle = Mathf.Deg2Rad * time * 18;
                        x = 3 * Mathf.Cos(angle);
                        y = 1 * Mathf.Sin(angle);
                        effect.transform.position = this.transform.position + new Vector3(x, -y);
                        yield return null;
                        time++;
                    }
                }
                if(skillLevel == 4)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = this.transform.position + new Vector3(-2, 0);
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = this.transform.position + new Vector3(0, -1);
                    effect = ObjectPoolManager.instance.GetQueueSkill("��");
                    effect.transform.position = this.transform.position + new Vector3(2, 0);
                }
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "�ϰ� �� �����ȭ":
                this.transform.position = new Vector3(10, -16);
                if (skillLevel == 1) yield return new WaitForSeconds(1f);
                else if (skillLevel == 2) yield return new WaitForSeconds(2f);
                else if (skillLevel == 3) yield return new WaitForSeconds(2.5f);
                else if (skillLevel == 4) yield return new WaitForSeconds(2.75f);
                SoundManager.instance.PlaySkillSFX("�巡��");
                while(time < 5)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");
                        effect.transform.position = new Vector2(Random.Range(0, 4) * 3 + 20, -Random.Range(0, 4) * 3 + 4);
                    }
                    yield return new WaitForSeconds(0.05f) ;
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                SoundManager.instance.StopSkillSFX();
                break;
            #endregion
            #region Ư����
            case "����":
                yield return new WaitForSeconds(1f);
                break;
            case "���":
                yield return new WaitForSeconds(0.5f);
                break;
                #endregion
        }
        yield return null;
        CharacterSkillShowEnd();
    }
    protected override void SkillUp()
    {
        string skillName = curSkill.skillName;
        int skillLevel = curSkill.level;

        switch (skillName)
        {   // ���� ��ų
            case "�ȸ������":
                if (skillLevel == 2) SkillInfo(5, 3, 10);
                else if (skillLevel == 3) SkillInfo(7, 3, 9);
                else if (skillLevel == 4) SkillInfo(9, 4, 8);
                break;
            case "���İ�":
                if (skillLevel == 2) SkillInfo(8, 4, 15);
                else if (skillLevel == 3) SkillInfo(11, 5, 14);
                else if (skillLevel == 4) SkillInfo(14, 6, 12);
                break;
            case "��ȭ������":
                if (skillLevel == 2) SkillInfo(5, 1, 12);
                else if (skillLevel == 3) SkillInfo(1, 1, 11);
                else if (skillLevel == 4) SkillInfo(0, 1, 10);
                break;
            case "�ϰ� �� ����·���":
                if (skillLevel == 2) SkillInfo(5, 5, 30);
                else if (skillLevel == 3) SkillInfo(4, 6, 27);
                else if (skillLevel == 4) SkillInfo(5, 10, 24);
                break;
            // ��ü ��ų
            case "����̴޷���":
                if (skillLevel == 2) SkillInfo(3, 1, 5);
                else if (skillLevel == 3) SkillInfo(5, 1, 5);
                else if (skillLevel == 4) SkillInfo(7, 1, 4);
                break;
            case "���̽���":
                if (skillLevel == 2) SkillInfo(6, 1, 10);
                else if (skillLevel == 3) SkillInfo(9, 1, 9);
                else if (skillLevel == 4) SkillInfo(12, 1, 8);
                break;
            case "������":
                if (skillLevel == 2) SkillInfo(8, 1, 15);
                else if (skillLevel == 3) SkillInfo(12, 1, 14);
                else if (skillLevel == 4) SkillInfo(16, 1, 12);
                break;
            case "�ϰ� �� �����ȭ":
                if (skillLevel == 2) SkillInfo(10, 1, 30);
                else if (skillLevel == 3) SkillInfo(14, 1, 27);
                else if (skillLevel == 4) SkillInfo(20, 1, 24);
                break;
        }
    }
    private void SkillInfo(int _damage, int _count, int _removeMp)
    {
        curSkill.skillDamage = _damage;
        curSkill.atkCount = _count;
        curSkill.removeMp = _removeMp;
    }
}