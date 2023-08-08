using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : State
{
    public List<CharacterSkill> basicSkill = new List<CharacterSkill>();        // 기본기 목록
    public List<CharacterSkill> targetSkill = new List<CharacterSkill>();       // 개인 스킬 목록
    public List<CharacterSkill> allSkill = new List<CharacterSkill>();          // 단체 스킬 목록
    public List<CharacterSkill> etcSkill = new List<CharacterSkill>();          // 특수 스킬 목록

    public CharacterSkill curSkill;     // 현재 사용할 스킬
    public ConsumData curConsum;        // 현재 사용할 물약

    public List<int> weaponIdx = new List<int>();
    public List<Equip> weaponList = new List<Equip>();          // 캐릭터가 가지고 있는 무기 목록
    public List<int> armorIdx = new List<int>();
    public List<Equip> armorList = new List<Equip>();           // 캐릭터가 가지고 있는 갑옷 목록

    // 캐릭터 번호 (아타호 = 0 / 린샹 = 1 / 스마슈 = 2)
    public bool isParty = false;        // 합류 한지 안한지
    public Sprite icon;                 // 아이콘 이미지
    public Sprite profile;              // 프로필 이미지

    public int maxMP;                   // 최대 마나
    public int curMP;                   // 현재 마나
    public int maxEXP;                  // 최대 경험치
    public int curEXP;                  // 현재 경험치

    // 장비
    public int curWeaponIdx;
    public Equip curWeapon;             // 무기
    public int curArmorIdx;
    public Equip curArmor;              // 방어구
    // 공격 할지 말지
    public bool isAtkCon;

    // 파티 합류
    public void PartyJoin(bool _join)
    {
        isParty = _join;
        if (isParty) sprite.enabled = true;
        else sprite.enabled = false;
    }
    // 캐릭터 총 스탯 = 기본 스탯 + 장비 스탯 
    public void TotalState()
    {
        totalAtk = atk + curWeapon.atk + curArmor.atk;
        totalDef = def + curWeapon.def + curArmor.def;
        totalHit = atk + curWeapon.hit + curArmor.hit;
        totalDodge = dodge + curWeapon.dodge + curArmor.dodge;
        totalCri = cri + curWeapon.cri + curArmor.cri;
    }
    #region 장비
    // 장비 습득
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
    #region 회복
    // MP 회복
    public void MPHeal(int _heal)
    {
        curMP += _heal;
        if (curMP >= maxMP)
        {
            curMP = maxMP;
        }
    }
    // 모두 회복
    public void HPMPHeal(int _hpHeal, int _mpHeal, string _cc = null, string _resurrection = null)
    {
        if (_resurrection == "부활")
        {
            isDie = false;
        }
        HPHeal(_hpHeal);
        MPHeal(_mpHeal);
    }
    #endregion
    #region 기술 추가 삭제
    /// <summary>
    /// 기본기 추가 삭제
    /// </summary>
    /// <param name="_skill">기본기</param>
    /// <param name="_addRemove">true : 추가 / false : 삭제</param>
    public void BasicSkill(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) basicSkill.Add(_skill);
        else basicSkill.Remove(_skill);
    }
    /// <summary>
    /// 개인 스킬 추가 삭제
    /// </summary>
    /// <param name="_skill">개인 스킬</param>
    /// <param name="_addRemove">true : 추가 / false : 삭제</param>
    public void TargetSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) targetSkill.Add(_skill);
        else targetSkill.Remove(_skill);
    }
    /// <summary>
    /// 단체 스킬 추가 삭제
    /// </summary>
    /// <param name="_skill">단체 스킬</param>
    /// <param name="_addRemove">true : 추가 / false : 삭제</param>
    public void AllSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) allSkill.Add(_skill);
        else allSkill.Remove(_skill);
    }
    /// <summary>
    /// 특수 스킬 추가 삭제
    /// </summary>
    /// <param name="_skill">특수 스킬</param>
    /// <param name="_addRemove">true : 추가 / false : 삭제</param>
    public void EtcSkillAdd(CharacterSkill _skill, bool _addRemove)
    {
        if (_addRemove) etcSkill.Add(_skill);
        else etcSkill.Remove(_skill);
    }
    #endregion
    #region 전투
    public IEnumerator SkillShow()
    {
        curMP -= curSkill.removeMp;
        if (curSkill.aniName != "doBasicSkill" && curSkill.aniName != "doEtcSkill")
        {
            if (curSkill.ExpUp())
            {
                SkillUp();
                curSkill.skillDamage += 5;
                // 레벨업 모션
                ani.SetTrigger("doSkillLevelUp");
                SoundManager.instance.PlaySFX("크리");
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
        // 암각 영상승룡파, 난도질
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
        // 전체 스킬이 아니라면
        if (curSkill.type != Skill.Type.ALL)
        {
            // 타겟팅한 적이 죽었는지 확인
            PlayerManager.instance.fight.ReTarget(target);
            // 타겟한 적 공격
            PlayerManager.instance.fight.monsterArray[target].HitDamage(damage, hit, isCri, curSkill.isSFX);
        }
        else  // 전체 스킬 이라면
        {
            // 모든적을 공격한다
            for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
            {
                // 죽은 적은 제외
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
            effect = ObjectPoolManager.instance.GetQueueSkill("별");
            effect.transform.position = this.transform.position + new Vector3(0, 2);
            time++;
            yield return new WaitForSeconds(0.04f);
        }
        if (curConsum.itemName == "마법의 물약") MPHeal(curConsum.manaRecovery);
        else if (curConsum.itemName == "고급 한방약") HPMPHeal(curConsum.healthRecovery, curConsum.manaRecovery);
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
    #region 보상
    // 경험치 획득
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
    // 캐릭터 레벨업
    protected virtual void LevelUp()
    {
        level++;
    }
    #endregion
}