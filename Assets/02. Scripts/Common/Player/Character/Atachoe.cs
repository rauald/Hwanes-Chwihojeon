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
        objectName = "��Ÿȣ";

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
            curWeapon = ItemData.instance.EquipFindAdd(objectName, weaponIdx[0], "����");
            curWeaponIdx = curWeapon.idx;
            weaponList[0].use = true;

            armorIdx.Add(0);
            curArmor = ItemData.instance.EquipFindAdd(objectName, armorIdx[0], "��");
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
            #region �⺻��
            case "����" :
            case "�ٸ��ĸ���":
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "��������":
            case "������":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.25f);
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            #endregion
            #region ���� ��ų
            case "ȣ�ݱ�":
                if (skillLevel == 1)
                {
                    // 1234 1234 1234 0.45��
                    // �Ҹ�      ����  ��
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");
                    yield return new WaitForSeconds(0.36f);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    // 1234 1234 1234 1234 1��
                    // �Ҹ� ����      ����  ��
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");
                    yield return new WaitForSeconds(0.11f);
                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    yield return new WaitForSeconds(0.5f);
                    // ���� ����Ʈ ť ����������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 3)
                {
                    // 1234 1234 1234 1234 1��
                    // �Ҹ� ����      ����  ��
                    SoundManager.instance.PlaySkillSFX("�غ�");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("�غ�");
                    yield return new WaitForSeconds(0.11f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");

                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    while (time < 5)
                    {
                        // ���� ����Ʈ ���� ����
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.04f);
                        // ���� ����Ʈ ���� ��
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.04f);
                        time++;
                    }
                    yield return new WaitForSeconds(0.1f);
                    // ���� ����Ʈ ť ��������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 4)
                {
                    // 1234 1234 1234 1234 1234
                    // �Ҹ�      ����      ����  ��
                    SoundManager.instance.PlaySkillSFX("�غ�");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("�غ�");
                    yield return new WaitForSeconds(0.14f);
                    SoundManager.instance.PlaySkillSFX("�غ�");
                    yield return new WaitForSeconds(0.22f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");

                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position;
                    while (time < 5)
                    {
                        // ���� ����Ʈ ���� ����
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.025f);
                        // ���� ����Ʈ ���� ��
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.025f);
                        time++;
                    }
                    // ���İ� ���� �� ������ ũ���ϱ�
                    time = 0;
                    while (time < 5)
                    {
                        // ������Ʈ ������ ���� ũ��
                        effect.transform.localScale = new Vector3(1f + (time % 5) / 10, 1f + (time % 5) / 10);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // ���� ����Ʈ ť ��������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                break;
            case "��ȣ���":
                float speed = 0;
                if (skillLevel == 1) speed = 0.022f;
                else if (skillLevel == 2) speed = 0.017f;
                else if (skillLevel == 3) speed = 0.013f;
                else if (skillLevel == 4) speed = 0.007f;
                while (time < 30)
                {
                    if (skillLevel >= 3)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");
                        effect.transform.position = this.transform.position;
                    }
                    this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position, 1.5f);
                    yield return new WaitForSeconds(speed);
                    time++;
                }
                Attack();
                yield return new WaitForSeconds(0.2f);
                break;
            case "������":
                this.transform.position = PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(-4, 0, 0);
                yield return new WaitForSeconds(0.25f);

                if (skillLevel == 1)
                {
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    // ��ų ����Ʈ ��
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    Attack();
                    yield return new WaitForSeconds(0.25f);
                    // ��ų ����Ʈ ����
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                else if (skillLevel == 3)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    while (time < curSkill.atkCount)
                    {
                        Attack();
                        yield return new WaitForSeconds(0.25f);
                        effect.transform.Translate(new Vector2(2, 0));
                        time++;
                    }
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                else if (skillLevel == 4)
                {
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = this.transform.position + new Vector3(2, 1);
                    while (time < curSkill.atkCount)
                    {
                        Attack();
                        yield return new WaitForSeconds(0.25f);
                        effect.transform.Translate(new Vector2(2, 1));
                        time++;
                    }
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "��ȣ�����":
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
                        effect = ObjectPoolManager.instance.GetQueueSkill("��");
                        effect.transform.position = this.transform.position;
                        this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerManager.instance.fight.monsterArray[target].transform.position + new Vector3(4, 0), 1f);
                        yield return new WaitForSeconds(0.025f);
                        if (time == 4) Attack();
                        time++;
                    }
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "��� �� ��ȣ������":
                this.transform.position = new Vector3(7, PlayerManager.instance.fight.monsterArray[target].transform.position.y);
                if (skillLevel == 1)
                {
                    // ���� ������ ��
                    for (int i = 0; i < 10; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                    yield return new WaitForSeconds(1f);
                }
                else if (skillLevel == 2)
                {
                    // ������� ����Ʈ ��
                    for (int i = 0; i < 15; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                    yield return new WaitForSeconds(1f);
                    // ������� ����Ʈ ����
                }
                else if (skillLevel == 3)
                {
                    // ������� ����Ʈ ��
                    for (int i = 0; i < 25; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                    yield return new WaitForSeconds(1f);
                    // ������� ����Ʈ ����
                }
                else if (skillLevel == 4)
                {
                    // ������� ����Ʈ ��
                    for (int i = 0; i < 40; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                    yield return new WaitForSeconds(1f);
                    // ������� ����Ʈ ����
                }
                // ������ ��
                SoundManager.instance.PlaySkillSFX("��");
                SoundManager.instance.SkillSFX.loop = true;
                effect = ObjectPoolManager.instance.GetQueueSkill("��");
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
                ObjectPoolManager.instance.InsertQueueSkill("��", effect);
                yield return new WaitForSeconds(0.5f);
                break;
            #endregion
            #region ��ü ��ų
            case "��ȣ����":
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
            case "��ȣ�� ���¢��":
                if (skillLevel == 1)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");
                    yield return new WaitForSeconds(0.25f);
                }
                else if (skillLevel == 2)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");
                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = new Vector2(15, -16);
                    yield return new WaitForSeconds(0.5f);
                    // ���� ����Ʈ ť ����������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                else if (skillLevel == 3)
                {
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");

                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = new Vector2(15, -16);

                    while (time < 5)
                    {
                        // ���� ����Ʈ ���� ����
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.05f);
                        // ���� ����Ʈ ���� ��
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    yield return new WaitForSeconds(0.25f);
                    // ���� ����Ʈ ť ��������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                else if (skillLevel == 4)
                {
                    // 1234 1234 1234 1234 1234
                    // �Ҹ�      ����      ����  ��
                    yield return new WaitForSeconds(0.25f);
                    SoundManager.instance.PlaySkillSFX("ȣ�ݱ�");

                    // ���� ����Ʈ ť �ҷ�����
                    effect = ObjectPoolManager.instance.GetQueueSkill("����");
                    effect.transform.position = new Vector2(15, -16);

                    while (time < 5)
                    {
                        // ���� ����Ʈ ���� ����
                        effect.SetActive(false);
                        yield return new WaitForSeconds(0.05f);
                        // ���� ����Ʈ ���� ��
                        effect.SetActive(true);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // ���İ� ���� �� ������ ũ���ϱ�
                    time = 0;
                    while (time < 5)
                    {
                        // ������Ʈ ������ ���� ũ��
                        effect.transform.localScale = new Vector3(1f + (time % 5) / 10, 1f + (time % 5) / 10);
                        yield return new WaitForSeconds(0.05f);
                        time++;
                    }
                    // ���� ����Ʈ ť ��������
                    ObjectPoolManager.instance.InsertQueueSkill("����", effect);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "ȣ����":
                GameObject[] energy = new GameObject[4];
                if (skillLevel == 1)
                {
                    // ���� ������ ��
                    for (int i = 0; i < 10; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                }
                else if (skillLevel == 2)
                {
                    // ���� ������ ��
                    for (int i = 0; i < 15; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                }
                else if (skillLevel == 3)
                {
                    // ���� ������ ��
                    for (int i = 0; i < 25; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                }
                else if (skillLevel == 4)
                {
                    // ���� ������ ��
                    for (int i = 0; i < 40; i++)
                    {
                        ObjectPoolManager.instance.GetQueueSkill("����");
                    }
                }
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
                {
                    if (PlayerManager.instance.fight.monsterArray[i].isDie) continue;

                    energy[i] = ObjectPoolManager.instance.GetQueueSkill("������");
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

                    ObjectPoolManager.instance.InsertQueueSkill("������", energy[i]);
                }
                Attack();
                yield return new WaitForSeconds(0.25f);
                break;
            case "��ȣ����":
                this.transform.position = new Vector3(10, -16);
                SoundManager.instance.PlaySkillSFX("��");
                while (time < curSkill.atkCount)
                {
                    yield return new WaitForSeconds(0.5f);
                    Attack();
                    time++;
                }
                yield return new WaitForSeconds(0.25f);
                break;
            case "��� �� ��ȣ������":
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
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");
                        effect.transform.position = new Vector2(Random.Range(20, 30), Random.Range(-1, -8));
                    }
                }
                else if (skillLevel == 3)
                {
                    rd.AddForce(dir * 7000f);
                    count = 10;
                    for (int i = 0; i < 5; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");
                        effect.transform.position = new Vector2(Random.Range(20, 30), Random.Range(-1, -8));
                    }
                }
                else if (skillLevel == 4)
                {
                    rd.AddForce(dir * 9000f);
                    count = 15;
                    for (int i = 0; i < 8; i++)
                    {
                        effect = ObjectPoolManager.instance.GetQueueSkill("����");
                        effect.transform.position = new Vector2(Random.Range(10, 30), Random.Range(-1, -8));
                    }
                }
                SoundManager.instance.PlaySkillSFX("������");
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
            #region Ư����
            case "����":
                yield return new WaitForSeconds(1f);
                break;
            case "���":
                yield return new WaitForSeconds(0.5f);
                break;
                #endregion
        }
        if (skillName == "���� �ٸ��ĸ���" || skillName == "�ֱ���" || skillName == "��ȣ����" || skillName == "������ ������")
        {

        }
        else if (skillName == "��������" || skillName == "�λ�Ҽ� ö��")
        {

        }
        CharacterSkillShowEnd();
    }
    protected override void SkillUp()
    {
        string skillName = curSkill.skillName;
        int skillLevel = curSkill.level;

        switch (skillName)
        {   // ���� ��ų
            case "ȣ�ݱ�" :
                if (skillLevel == 2) SkillInfo(5, 1, 10);
                else if (skillLevel == 3) SkillInfo(7, 1, 9);
                else if (skillLevel == 4) SkillInfo(9, 1, 8);
                break;
            case "��ȣ���":
                if (skillLevel == 2) SkillInfo(8, 1, 12);
                else if (skillLevel == 3) SkillInfo(11, 1, 11);
                else if (skillLevel == 4) SkillInfo(14, 1, 10);
                break;
            case "������":
                if (skillLevel == 2) SkillInfo(5, 1, 15);
                else if (skillLevel == 3) SkillInfo(1, 2, 14);
                else if (skillLevel == 4) SkillInfo(0, 3, 12);
                break;
            case "��ȣ�����":
                if (skillLevel == 2) SkillInfo(5, 3, 20);
                else if (skillLevel == 3) SkillInfo(4, 4, 18);
                else if (skillLevel == 4) SkillInfo(5, 7, 16);
                break;
            case "��� �� ��ȣ������":
                if (skillLevel == 2) SkillInfo(8, 3, 30);
                else if (skillLevel == 3) SkillInfo(5, 5, 27);
                else if (skillLevel == 4) SkillInfo(8, 5, 24);
                break;
            // ��ü ��ų
            case "��ȣ����":
                if (skillLevel == 2) SkillInfo(3, 1, 10);
                else if (skillLevel == 3) SkillInfo(5, 1, 9);
                else if (skillLevel == 4) SkillInfo(7, 1, 8);
                break;
            case "��ȣ�� ���¢��":
                if (skillLevel == 2) SkillInfo(6, 1, 5);
                else if (skillLevel == 3) SkillInfo(9, 1, 5);
                else if (skillLevel == 4) SkillInfo(12, 1, 4);
                break;
            case "ȣ����":
                if (skillLevel == 2) SkillInfo(8, 1, 15);
                else if (skillLevel == 3) SkillInfo(12, 1, 14);
                else if (skillLevel == 4) SkillInfo(16, 1, 12);
                break;
            case "��ȣ����":
                if (skillLevel == 2) SkillInfo(5, 3, 20);
                else if (skillLevel == 3) SkillInfo(4, 4, 18);
                else if (skillLevel == 4) SkillInfo(3, 5, 16);
                break;
            case "��� �� ��ȣ������":
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