using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atachoe : Character
{
    private Rigidbody2D rd;

    protected override void Awake()
    {
        base.Awake();
        rd = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        objectName = "아타호";

        if (isParty) sprite.enabled = true;
        else sprite.enabled = false;

        if (!DataController.instance.gameData.save)
        {
            maxHP = 30;
            curHP = maxHP;
            maxMP = 18;
            curMP = maxMP;
            atk = 22;
            def = 7;
            hit = 122;
            dodge = 15;
            cri = 50;

            weaponIdx.Add(0);
            curWeapon = ItemData.instance.EquipFindAdd(objectName, weaponIdx[0], "무기");
            curWeaponIdx = curWeapon.idx;
            weaponList[0].use = true;

            armorIdx.Add(0);
            curArmor = ItemData.instance.EquipFindAdd(objectName, armorIdx[0], "방어구");
            curArmorIdx = curArmor.idx;
            armorList[0].use = true;

            TotalState();

            level = 9;
            maxEXP = 100;
            curEXP = 0;

            isDie = false;
        }
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        maxHP += 5;
        curHP = maxHP;
        maxMP += 3;
        curMP = maxMP;
        atk += 4;
        def += 4;
        hit += 4;
        dodge += 4;
        cri += 4;

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
            case "정권" :
            case "다리후리기":
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "돌려차기":
            case "던지기":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            #endregion
            #region 개인 스킬
            case "호격권":
                if (skillLevel == 1)
                {
                    // 1234 1234 1234 0.45초
                    // 소리      공격  끝
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("호격권");
                    yield return new WaitForSeconds(0.36f);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    // 1234 1234 1234 1234 1초
                    // 소리 사자      공격  끝
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("호격권");
                    yield return new WaitForSeconds(0.11f);
                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    yield return new WaitForSeconds(0.5f);
                    // 사자 이펙트 큐 돌려보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 3)
                {
                    // 1234 1234 1234 1234 1초
                    // 소리 사자      공격  끝
                    SoundManager.instance.PlaySkillSFX("준비");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("준비");
                    yield return new WaitForSeconds(0.11f);
                    SoundManager.instance.PlaySkillSFX("호격권");

                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    while (time < 5)
                    {
                        // 사자 이펙트 알파 오프
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.04f);
                        // 사자 이펙트 알파 온
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.04f);
                        time++;
                    }
                    yield return new WaitForSeconds(0.1f);
                    // 사자 이펙트 큐 돌보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 4)
                {
                    // 1234 1234 1234 1234 1234
                    // 소리      사자      공격  끝
                    SoundManager.instance.PlaySkillSFX("준비");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("준비");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("준비");
                    yield return new WaitForSeconds(0.22f);
                    SoundManager.instance.PlaySkillSFX("호격권");

                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    while (time < 5)
                    {
                        // 사자 이펙트 알파 오프
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.025f);
                        // 사자 이펙트 알파 온
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.025f);
                        time++;
                    }
                    // 알파값 조정 및 스케일 크게하기
                    time = 0;
                    while (time < 5)
                    {
                        // 오브젝트 스케일 점점 크게
                        effect.transform.localScale = new Vector3(1f + (time % 5) / 10, 1f + (time % 5) / 10);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // 사자 이펙트 큐 돌보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                break;
            case "맹호비상각":
                float speed = 0;
                if (skillLevel == 1) speed = 0.022f;
                else if (skillLevel == 2) speed = 0.017f;
                else if (skillLevel == 3) speed = 0.013f;
                else if (skillLevel == 4) speed = 0.007f;
                while (time < 30)
                {
                    if (skillLevel >= 3)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("먼지");
                        effect.transform.position = this.transform.position;
                    }
                    this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position, 1.5f);
                    yield return new WaitForSeconds(speed);
                    time++;
                }
                Attack();
                yield return new WaitForSeconds(0.2f);
                break;
            case "폭전축":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.25f);

                if (skillLevel == 1)
                {
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    // 스킬 이펙트 온
                    effect = ObjectPoolManager.instance.GetQueueSkill("스핀");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                    // 스킬 이펙트 오프
                    ObjectPoolManager.instance.InsertQueueSkill("스핀", effect);
                }
                else if (skillLevel == 3)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("스핀");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    while (time < curSkill.atkCount)
                    {
                        Attack();
                        yield return new WaitForSeconds(0.25f);
                        effect.transform.Translate(new Vector2(2, 0));
                        time++;
                    }
                    ObjectPoolManager.instance.InsertQueueSkill("스핀", effect);
                }
                else if (skillLevel == 4)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("스핀");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    while (time < curSkill.atkCount)
                    {
                        Attack();
                        yield return new WaitForSeconds(0.25f);
                        effect.transform.Translate(new Vector2(2, 1));
                        time++;
                    }
                    ObjectPoolManager.instance.InsertQueueSkill("스핀", effect);
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "맹호스페셜":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);

                if (skillLevel == 1 || skillLevel == 3)
                {
                    while (time < curSkill.atkCount)
                    {
                        yield return new WaitForSeconds(0.5f);
                        Attack();
                        time++;
                    }
                }
                else if (skillLevel == 2 || skillLevel == 4)
                {
                    while (time < (curSkill.atkCount - 1))
                    {
                        yield return new WaitForSeconds(0.5f);
                        Attack();
                        time++;
                    }
                    time = 0;
                    yield return new WaitForSeconds(0.25f);
                    while (time < 10)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("별");
                        effect.transform.position = this.transform.position;
                        this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(4, 0), 1f);
                        yield return new WaitForSeconds(0.025f);
                        if (time == 4) Attack();
                        time++;
                    }
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "비기 · 맹호광파참":
                this.transform.position = new Vector3(7, PlayerManager.instance.fight.monsterArray[target].transform.position.y);
                if (skillLevel == 1)
                {
                    // 충전 레이져 온
                    for (int i = 0; i < 10; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                    yield return new WaitForSeconds(1f);
                }
                else if (skillLevel == 2)
                {
                    // 기모으는 이펙트 온
                    for (int i = 0; i < 15; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                    yield return new WaitForSeconds(1f);
                    // 기모으는 이펙트 오프
                }
                else if (skillLevel == 3)
                {
                    // 기모으는 이펙트 온
                    for (int i = 0; i < 25; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                    yield return new WaitForSeconds(1f);
                    // 기모으는 이펙트 오프
                }
                else if (skillLevel == 4)
                {
                    // 기모으는 이펙트 온
                    for (int i = 0; i < 40; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                    yield return new WaitForSeconds(1f);
                    // 기모으는 이펙트 오프
                }
                // 레이저 빔
                SoundManager.instance.PlaySkillSFX("빔");
                SoundManager.instance.SkillSFX.loop = true;
                effect = ObjectPoolManager.instance.GetQueueSkill("빔");
                effect.transform.position = this.transform.position + new Vector3(1, 1, 0);
                while (time < curSkill.atkCount)
                {
                    yield return new WaitForSeconds(0.25f);
                    Attack();
                    if (time > (curSkill.atkCount - 1)) effect.transform.Translate(new Vector2(20, 0));
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                SoundManager.instance.SkillSFX.loop = false;
                SoundManager.instance.StopSkillSFX();
                ObjectPoolManager.instance.InsertQueueSkill("빔", effect);
                yield return new WaitForSeconds(0.5f);
                break;
            #endregion
            #region 단체 스킬
            case "맹호난무":
                this.transform.position = new Vector3(10, -16);
                if (skillLevel < 4)
                {
                    yield return new WaitForSeconds(1.5f);
                }
                else if (skillLevel == 4)
                {
                    yield return new WaitForSeconds(2f);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "맹호의 울부짖음":
                if (skillLevel == 1)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("호격권");
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("호격권");
                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = new Vector2(15, -16);
                    yield return new WaitForSeconds(0.5f);
                    // 사자 이펙트 큐 돌려보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                }
                else if (skillLevel == 3)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("호격권");

                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = new Vector2(15, -16);

                    while (time < 5)
                    {
                        // 사자 이펙트 알파 오프
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.05f);
                        // 사자 이펙트 알파 온
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    yield return new WaitForSeconds(0.25f);
                    // 사자 이펙트 큐 돌보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                }
                else if (skillLevel == 4)
                {
                    // 1234 1234 1234 1234 1234
                    // 소리      사자      공격  끝
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("호격권");

                    // 사자 이펙트 큐 불러오기
                    effect = ObjectPoolManager.instance.GetQueueSkill("사자");
                    effect.transform.position = new Vector2(15, -16);

                    while (time < 5)
                    {
                        // 사자 이펙트 알파 오프
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.05f);
                        // 사자 이펙트 알파 온
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // 알파값 조정 및 스케일 크게하기
                    time = 0;
                    while (time < 5)
                    {
                        // 오브젝트 스케일 점점 크게
                        effect.transform.localScale = new Vector3(1f + (time % 5) / 10, 1f + (time % 5) / 10);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // 사자 이펙트 큐 돌보내기
                    ObjectPoolManager.instance.InsertQueueSkill("사자", effect);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "호포권":
                GameObject[] energy = new GameObject[4];
                if (skillLevel == 1)
                {
                    // 충전 레이져 온
                    for (int i = 0; i < 10; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                }
                else if (skillLevel == 2)
                {
                    // 충전 레이져 온
                    for (int i = 0; i < 15; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                }
                else if (skillLevel == 3)
                {
                    // 충전 레이져 온
                    for (int i = 0; i < 25; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                }
                else if (skillLevel == 4)
                {
                    // 충전 레이져 온
                    for (int i = 0; i < 40; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("충전");
                    }
                }
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
                {
                    if (PlayerManager.instance.fight.monsterArray[i].isDie) continue;

                    energy[i] = ObjectPoolManager.instance.GetQueueSkill("에너지");
                    energy[i].transform.position = this.transform.position + new Vector3(3, 0);
                }
                while (time < 10)
                {
                    for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
                    {
                        if (energy[i] == null) continue;

                        energy[i].transform.position = Vector2.MoveTowards(energy[i].transform.position, PlayerManager.instance.fight.monsterArray[i].transform.position, 4f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                for (int i = 0; i < energy.Length; i++)
                {
                    if (energy[i] == null) continue;

                    ObjectPoolManager.instance.InsertQueueSkill("에너지", energy[i]);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "맹호룬룬권":
                this.transform.position = new Vector3(10, -16);
                SoundManager.instance.PlaySkillSFX("댄스");
                while (time < curSkill.atkCount)
                {
                    yield return new WaitForSeconds(0.5f);
                    Attack();
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "비기 · 맹호유성각":
                Vector2 dir = new Vector2(1, Random.Range(-1f, 1f)).normalized;
                int count = 0;

                if (skillLevel == 1)
                {
                    rd.AddForce(dir * 3000f);
                    count = 5;
                }
                else if (skillLevel == 2)
                {
                    rd.AddForce(dir * 5000f);
                    count = 5;
                    for (int i = 0; i < 2; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("유성");
                        effect.transform.position = new Vector2(Random.Range(20, 30), Random.Range(-1, -8));
                    }
                }
                else if (skillLevel == 3)
                {
                    rd.AddForce(dir * 7000f);
                    count = 10;
                    for (int i = 0; i < 5; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("유성");
                        effect.transform.position = new Vector2(Random.Range(20, 30), Random.Range(-1, -8));
                    }
                }
                else if (skillLevel == 4)
                {
                    rd.AddForce(dir * 9000f);
                    count = 15;
                    for (int i = 0; i < 8; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("유성");
                        effect.transform.position = new Vector2(Random.Range(10, 30), Random.Range(-1, -8));
                    }
                }
                SoundManager.instance.PlaySkillSFX("유성각");
                while (time < count)
                {
                    if (rd.velocity.x > 0) sprite.flipX = false;
                    else if (rd.velocity.x < 0) sprite.flipX = true;
                    yield return new WaitForSeconds(0.1f);
                    time++;
                }
                rd.velocity = Vector2.zero;
                Attack();
                yield return new WaitForSeconds(0.5f);
                SoundManager.instance.StopSkillSFX();
                sprite.flipX = false;
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
        if (skillName == "지옥 다리후리기" || skillName == "주구격" || skillName == "취호염무" || skillName == "노익장 대폭발")
        {

        }
        else if (skillName == "염가열소" || skillName == "인사불성 철권")
        {

        }
        CharacterSkillShowEnd();
    }
    protected override void SkillUp()
    {
        string skillName = curSkill.skillName;
        int skillLevel = curSkill.level;

        switch (skillName)
        {   // 개인 스킬
            case "호격권" :
                if (skillLevel == 2) SkillInfo(5, 1, 10);
                else if (skillLevel == 3) SkillInfo(7, 1, 9);
                else if (skillLevel == 4) SkillInfo(9, 1, 8);
                break;
            case "맹호비상각":
                if (skillLevel == 2) SkillInfo(8, 1, 12);
                else if (skillLevel == 3) SkillInfo(11, 1, 11);
                else if (skillLevel == 4) SkillInfo(14, 1, 10);
                break;
            case "폭전축":
                if (skillLevel == 2) SkillInfo(5, 1, 15);
                else if (skillLevel == 3) SkillInfo(1, 2, 14);
                else if (skillLevel == 4) SkillInfo(0, 3, 12);
                break;
            case "맹호스페셜":
                if (skillLevel == 2) SkillInfo(5, 3, 20);
                else if (skillLevel == 3) SkillInfo(4, 4, 18);
                else if (skillLevel == 4) SkillInfo(5, 7, 16);
                break;
            case "비기 · 맹호광파참":
                if (skillLevel == 2) SkillInfo(8, 3, 30);
                else if (skillLevel == 3) SkillInfo(5, 5, 27);
                else if (skillLevel == 4) SkillInfo(8, 5, 24);
                break;
            // 단체 스킬
            case "맹호난무":
                if (skillLevel == 2) SkillInfo(3, 1, 10);
                else if (skillLevel == 3) SkillInfo(5, 1, 9);
                else if (skillLevel == 4) SkillInfo(7, 1, 8);
                break;
            case "맹호의 울부짖음":
                if (skillLevel == 2) SkillInfo(6, 1, 5);
                else if (skillLevel == 3) SkillInfo(9, 1, 5);
                else if (skillLevel == 4) SkillInfo(12, 1, 4);
                break;
            case "호포권":
                if (skillLevel == 2) SkillInfo(8, 1, 15);
                else if (skillLevel == 3) SkillInfo(12, 1, 14);
                else if (skillLevel == 4) SkillInfo(16, 1, 12);
                break;
            case "맹호룬룬권":
                if (skillLevel == 2) SkillInfo(5, 3, 20);
                else if (skillLevel == 3) SkillInfo(4, 4, 18);
                else if (skillLevel == 4) SkillInfo(3, 5, 16);
                break;
            case "비기 · 맹호유성각":
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