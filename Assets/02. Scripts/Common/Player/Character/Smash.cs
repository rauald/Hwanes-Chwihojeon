using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Character
{
    private void Start()
    {
        objectName = "스마슈";

        if (!DataController.instance.gameData.save)
        {
            maxHP = 25;
            curHP = maxHP;
            maxMP = 20;
            curMP = maxMP;
            atk = 17;
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
            #region 기본기
            case "베기":
                yield return new WaitForSeconds(0.5f);
                Attack();
                yield return new WaitForSeconds(0.5f);
                break;
            #endregion
            #region 개인 스킬
            case "대타격":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4 - 5 * (skillLevel - 1), 0, 0);
                while (time < (5 * (skillLevel - 1)))
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("먼지");
                    effect.transform.position = this.transform.position;
                    yield return new WaitForSeconds(0.05f);
                    this.transform.Translate(new Vector2(1, 0));
                    time++;
                }
                yield return new WaitForSeconds(0.5f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "쾌진격":
                yield return new WaitForSeconds(0.25f);
                float speed = 0;
                if (skillLevel == 1) speed = 0.022f;
                else if (skillLevel == 2) speed = 0.017f;
                else if (skillLevel == 3) speed = 0.013f;
                else if (skillLevel == 4) speed = 0.007f;
                while (time < 30)
                {
                    if (skillLevel >= 3)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("별");
                        effect.transform.position = this.transform.position;
                    }
                    this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position, 1.5f);
                    yield return new WaitForSeconds(speed);
                    time++;
                }
                Attack();
                yield return new WaitForSeconds(0.2f);
                break;
            case "비검 · 목단미인":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.5f * skillLevel);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            #endregion
            #region 단체 스킬
            case "백인일섬":
                for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
                {
                    if (PlayerManager.instance.fight.monsterArray[i].isDie) continue;

                    effect = ObjectPoolManager.instance.GetQueueSkill("선");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[i].transform.position + new Vector3(-4 - (5 * (skillLevel - 1)), 0);
                }
                yield return new WaitForSeconds(1f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "인법 · 분신술":
                int count = 0;
                GameObject[] clone = new GameObject[10];
                float rad = 0;
                float x = 0;
                float y = 0;

                this.transform.position = new Vector3(10, -16);
                if (skillLevel == 1) count = 2;
                else if (skillLevel == 2) count = 4;
                else if (skillLevel == 3) count = 6;
                else if (skillLevel == 4) count = 10;
                yield return new WaitForSeconds(0.25f);
                // 분신 생성
                for (int i = 0; i < count; i++)
                {
                    clone[i] = ObjectPoolManager.instance.GetQueueSkill("분신");
                }
                while (time < 20)
                {
                    for (int i = 0; i < count; i++)
                    {
                        rad = Mathf.Deg2Rad * ((360/count) * i + (18f * time));
                        x = 0.2f * time * Mathf.Sin(rad);
                        y = 0.2f * time * Mathf.Cos(rad);
                        clone[i].transform.position = this.transform.position + new Vector3(x, y);
                    }
                    yield return new WaitForSeconds(0.05f);
                    time++;
                }
                time = 0;
                while(time < curSkill.atkCount)
                {
                    yield return new WaitForSeconds(0.5f);
                    // 분신 돌아다님
                    Attack();
                    time++;
                }

                yield return new WaitForSeconds(0.25f);
                break;
            case "비검 · 시공단":
                yield return new WaitForSeconds(0.25f);

                for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
                {
                    if (PlayerManager.instance.fight.monsterArray[i].isDie) continue;

                    PlayerManager.instance.fight.monsterArray[i].SpaceTime();
                }

                yield return new WaitForSeconds(0.5f);

                if (skillLevel == 1) yield return new WaitForSeconds(0.25f);
                else if (skillLevel == 2) yield return new WaitForSeconds(0.5f);
                else if (skillLevel == 3) yield return new WaitForSeconds(0.75f);
                else if (skillLevel == 4) yield return new WaitForSeconds(1f);
                //SoundManager.instance.PlaySkillSFX("드래곤");

                Attack();
                yield return new WaitForSeconds(0.25f);
                SoundManager.instance.StopSkillSFX();
                break;
            #endregion
            #region 특수기
            case "도주":
                yield return new WaitForSeconds(1f);
                break;
            case "방어":
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
        {   // 개인 스킬
            case "안면백조권":
                if (skillLevel == 2) SkillInfo(5, 3, 10);
                else if (skillLevel == 3) SkillInfo(7, 3, 9);
                else if (skillLevel == 4) SkillInfo(9, 4, 8);
                break;
            case "선렬각":
                if (skillLevel == 2) SkillInfo(8, 4, 15);
                else if (skillLevel == 3) SkillInfo(11, 5, 14);
                else if (skillLevel == 4) SkillInfo(14, 6, 12);
                break;
            case "열화폭염권":
                if (skillLevel == 2) SkillInfo(5, 1, 12);
                else if (skillLevel == 3) SkillInfo(1, 1, 11);
                else if (skillLevel == 4) SkillInfo(0, 1, 10);
                break;
            case "암각 · 영상승룡파":
                if (skillLevel == 2) SkillInfo(5, 5, 30);
                else if (skillLevel == 3) SkillInfo(4, 6, 27);
                else if (skillLevel == 4) SkillInfo(5, 10, 24);
                break;
            // 단체 스킬
            case "고양이달래기":
                if (skillLevel == 2) SkillInfo(3, 1, 5);
                else if (skillLevel == 3) SkillInfo(5, 1, 5);
                else if (skillLevel == 4) SkillInfo(7, 1, 4);
                break;
            case "유미쌍조":
                if (skillLevel == 2) SkillInfo(6, 1, 10);
                else if (skillLevel == 3) SkillInfo(9, 1, 9);
                else if (skillLevel == 4) SkillInfo(12, 1, 8);
                break;
            case "대폭진":
                if (skillLevel == 2) SkillInfo(8, 1, 15);
                else if (skillLevel == 3) SkillInfo(12, 1, 14);
                else if (skillLevel == 4) SkillInfo(16, 1, 12);
                break;
            case "암각 · 영상뢰화":
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