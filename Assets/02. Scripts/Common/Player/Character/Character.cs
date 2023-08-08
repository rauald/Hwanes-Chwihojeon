using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : State
{
    public List<CharacterSkill> basicSkill = new List<CharacterSkill>();        // �⺻�� ���
    public List<CharacterSkill> targetSkill = new List<CharacterSkill>();       // ���� ��ų ���
    public List<CharacterSkill> allSkill = new List<CharacterSkill>();          // ��ü ��ų ���
    public List<CharacterSkill> etcSkill = new List<CharacterSkill>();          // Ư�� ��ų ���

    public CharacterSkill curSkill;     // ���� ����� ��ų
    public ConsumData curConsum;        // ���� ����� ����

    public List<int> weaponIdx = new List<int>();
    public List<Equip> weaponList = new List<Equip>();          // ĳ���Ͱ� ������ �ִ� ���� ���
    public List<int> armorIdx = new List<int>();
    public List<Equip> armorList = new List<Equip>();           // ĳ���Ͱ� ������ �ִ� ���� ���

    // ĳ���� ��ȣ (��Ÿȣ = 0 / ���� = 1 / ������ = 2)
    public bool isParty = false;        // �շ� ���� ������
    public Sprite icon;                 // ������ �̹���
    public Sprite profile;              // ������ �̹���

    public int maxMP;                   // �ִ� ����
    public int curMP;                   // ���� ����
    public int maxEXP;                  // �ִ� ����ġ
    public int curEXP;                  // ���� ����ġ

    // ���
    public int curWeaponIdx;
    public Equip curWeapon;             // ����
    public int curArmorIdx;
    public Equip curArmor;              // ��
    // ���� ���� ����
    public bool isAtkCon;

    // ��Ƽ �շ�
    public void PartyJoin(bool _join)
    {
        isParty = _join;
        if (isParty) sprite.enabled = true;
        else sprite.enabled = false;
    }
    // ĳ���� �� ���� = �⺻ ���� + ��� ���� 
    public void TotalState()
    {
        totalAtk = atk + curWeapon.atk + curArmor.atk;
        totalDef = def + curWeapon.def + curArmor.def;
        totalHit = atk + curWeapon.hit + curArmor.hit;
        totalDodge = dodge + curWeapon.dodge + curArmor.dodge;
        totalCri = cri + curWeapon.cri + curArmor.cri;
    }
    #region ���
    // ��� ����
    public void EquipAdd(Equip _add)
    {
        if (_add.type == Equip.Type.WEAPON)
        {
            weaponIdx.Add(_add.idx);
            weaponIdx.Sort();

            weaponList.Add(_add);
            List<Equip> sortWeapon = weaponList.OrderBy(x => x.idx).ToList();

            weaponList = sortWeapon;
        }
        else if (_add.type == Equip.Type.ARMOR)
        {
            armorIdx.Add(_add.idx);
            armorIdx.Sort();

            armorList.Add(_add);
            List<Equip> sortArmor = armorList.OrderBy(x => x.idx).ToList();

            armorList = sortArmor;
        }
    }
    #endregion
    #region ȸ��
    // MP ȸ��
    public void MPHeal(int _heal)
    {
        curMP += _heal;
        if (curMP >= maxMP)
        {
            curMP = maxMP;
        }
    }
    // ��� ȸ��
    public void HPMPHeal(int _hpHeal, int _mpHeal, string _cc = null, string _resurrection = null)
    {
        if (_resurrection == "��Ȱ")
        {
            isDie = false;
        }
        HPHeal(_hpHeal);
        MPHeal(_mpHeal);
    }
    #endregion
    #region ��� �߰� ����
    /// <summary>
    /// �⺻�� �߰� ����
    /// </summary>
    /// <param name="_skill">�⺻��</param>
    /// <param name="_addRemove">true : �߰� / false : ����</param>
    public void BasicSkill(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) basicSkill.Add(_skill);
        else basicSkill.Remove(_skill);
    }
    /// <summary>
    /// ���� ��ų �߰� ����
    /// </summary>
    /// <param name="_skill">���� ��ų</param>
    /// <param name="_addRemove">true : �߰� / false : ����</param>
    public void TargetSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) targetSkill.Add(_skill);
        else targetSkill.Remove(_skill);
    }
    /// <summary>
    /// ��ü ��ų �߰� ����
    /// </summary>
    /// <param name="_skill">��ü ��ų</param>
    /// <param name="_addRemove">true : �߰� / false : ����</param>
    public void AllSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) allSkill.Add(_skill);
        else allSkill.Remove(_skill);
    }
    /// <summary>
    /// Ư�� ��ų �߰� ����
    /// </summary>
    /// <param name="_skill">Ư�� ��ų</param>
    /// <param name="_addRemove">true : �߰� / false : ����</param>
    public void EtcSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) etcSkill.Add(_skill);
        else etcSkill.Remove(_skill);
    }
    #endregion
    #region ����
    public IEnumerator SkillShow()
    {
        curMP -= curSkill.removeMp;
        if (curSkill.aniName != "doBasicSkill" && curSkill.aniName != "doEtcSkill")
        {
            if (curSkill.ExpUp())
            {
                SkillUp();
                curSkill.skillDamage += 5;
                // ������ ���
                ani.SetTrigger("doSkillLevelUp");
                SoundManager.instance.PlaySFX("ũ��");
                yield return new WaitForSeconds(2.0f);
            }
        }
        yield return null;
        ani.SetFloat("SkillNumber", curSkill.number);
        ani.SetFloat("SkillLevel", curSkill.level);
        ani.SetTrigger(curSkill.aniName);
        curAtkCount = 0;
        StartCoroutine(SkillMove());
    }
    public virtual IEnumerator SkillMove() { yield return null; }
    protected virtual void SkillUp() { }
    public void Attack()
    {
        damage = totalAtk;
        // �ϰ� ����·���, ������
        if (curAtkCount < curSkill.atkCount)
        {
            if (curSkill.atkCount < 5)
            {
                damage += (curSkill.skillDamage + (curAtkCount * Random.Range(0, 5)));
            }
            else
            {
                damage += (curSkill.skillDamage + (-curSkill.atkCount + (curAtkCount * Random.Range(0,5))));
            }
            curAtkCount++;
        }
        IsCritical();
        // ��ü ��ų�� �ƴ϶��
        if (curSkill.type != Skill.Type.ALL)
        {
            // Ÿ������ ���� �׾����� Ȯ��
            PlayerManager.instance.fight.ReTarget(target);
            // Ÿ���� �� ����
            PlayerManager.instance.fight.monsterArray[target].HitDamage(damage, hit, isCri, curSkill.isSFX);
        }
        else  // ��ü ��ų �̶��
        {
            // ������� �����Ѵ�
            for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
            {
                // ���� ���� ����
                if (PlayerManager.instance.fight.monsterArray[i].isDie) continue;

                PlayerManager.instance.fight.monsterArray[i].HitDamage(damage, hit, isCri, curSkill.isSFX);
            }
        }
    }
    public IEnumerator ConsumShow()
    {
        ani.SetTrigger("doHeal");
        GameObject effect;
        int time = 0;
        while (time < 20)
        {
            effect = ObjectPoolManager.instance.GetQueueSkill("��");
            effect.transform.position = this.transform.position + new Vector3(0, 2);
            time++;
            yield return new WaitForSeconds(0.04f);
        }
        if (curConsum.itemName == "������ ����") MPHeal(curConsum.manaRecovery);
        else if (curConsum.itemName == "��� �ѹ��") HPMPHeal(curConsum.healthRecovery, curConsum.manaRecovery);
        else HPHeal(curConsum.healthRecovery);

        UIManager.instance.info.statusInfo.Status();
        UIManager.instance.StartCoroutine(UIManager.instance.HealUI(curConsum.healthRecovery, this.transform.position));

        CharacterSkillShowEnd();
    }
    public override void Dead()
    {
        base.Dead();
        ani.SetBool("isDie", true);
    }
    #endregion
    #region ����
    // ����ġ ȹ��
    public void EXPUp(int _exp)
    {
        curEXP += _exp;
        if (curEXP >= maxEXP)
        {
            curEXP = _exp - (maxEXP - curEXP);
            level++;
            LevelUp();
        }
    }
    // ĳ���� ������
    protected virtual void LevelUp()
    {
        level++;
    }
    #endregion
}