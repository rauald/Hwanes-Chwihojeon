using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public string objectName;          // 오브젝트 이름
    public Animator ani;               // 오브젝트 애니메이션
    public SpriteRenderer sprite;      // 스프라이트 랜더러 껏다키기

    // 기본
    public int level;                  // 레벨
    public int maxHP;                  // 최대 체력
    public int curHP;                  // 현재 체력

    public int atk;                    // 공격력
    public int def;                    // 방어력
    public int hit;                    // 기술력 (명중률)
    public int dodge;                  // 순발력 (회피율)
    public bool isDodge;               // 순발력 (회피율)
    public bool isCri;                 // 크리티컬 떳는지
    public int cri;                    // 운 (크리티컬 확률)
    protected float criAtk = 1.5f;     // 크리티컬 데미지

    protected int damage;              // 들어갈 데미지
    public int curAtkCount;            // 현재 데미지 카운트
    protected bool dotDamage;          // 도트 데미지

    // 기본 + 아이템 총합
    public int totalAtk;               // 기본 + 아이템 총합 공격력
    public int totalDef;               // 기본 + 아이템 총합 방어력
    public int totalHit;               // 기본 + 아이템 총합 기술력 (명중률)
    public int totalDodge;             // 기본 + 아이템 총합 순발력 (회피율)
    public int totalCri;               // 기본 + 아이템 총합 운 (크리티컬 확률)

    public bool isDie;                 // 죽음

    // 전투
    protected Vector2 startPoint;          // 전투 시작 위치
    protected Vector2 movePoint;           // 스킬 사용시 이동될 위치
    public int target;                     // 누굴 타겟팅 한지

    // 스킬
    protected GameObject[] skillSlot;

    protected virtual void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        ani = this.GetComponent<Animator>();
    }
    // 전투 위치
    public void FightPosition(Vector2 _position)
    {
        startPoint = _position;
        this.transform.position = startPoint;
    }
    // 자리로 돌아가기
    public void CharacterSkillShowEnd()
    {
        this.transform.position = startPoint;
        PlayerManager.instance.fight.StartCoroutine(PlayerManager.instance.fight.CharacterTurnEnd());
    }
    public void MonsterSkillShowEnd()
    {
        this.transform.position = startPoint;
        PlayerManager.instance.fight.StartCoroutine(PlayerManager.instance.fight.MonsterTurnEnd());
    }
    // 크리티컬 인지 아닌지
    protected void IsCritical()
    {
        int rand = Random.Range(0, 100) + 1;
        // 크리티컬
        if (rand < cri)
        {
            isCri = true;
            damage = (int)(damage * criAtk);
            return;
        }
        isCri = false;
    }
    /// <summary>
    /// 피격
    /// </summary>
    /// <param name="_damage">자신 공격력</param>
    /// <param name="_hit">자신 명중률</param>
    public void HitDamage(int _damage, int _hit, bool _isCri, bool _sfx)
    {
        int rand = Random.Range(0, 100) + 1;
        // 회피
        if (rand > (_hit - dodge))
        {
            _damage = 0;
            SoundManager.instance.PlaySFX("회피");
            ani.SetTrigger("doDodge");
        }
        else
        {
            _damage -= totalDef;
            if (_damage > 0)
            {
                curHP -= _damage;
                string sfxName = null;
                if (_sfx)
                {
                    if (PlayerManager.instance.fight.curCharacter < 3)
                    {
                        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].objectName == "스마슈")
                        {
                            SoundManager.instance.PlaySFX(sfxName = _isCri ? "크리" : "검");
                        }
                        else SoundManager.instance.PlaySFX(sfxName = _isCri ? "크리" : "공격");
                    }
                    else SoundManager.instance.PlaySFX(sfxName = _isCri ? "크리" : "공격");
                }
                ani.SetTrigger("doHit");
            }
            else
            {
                _damage = 0;
                SoundManager.instance.PlaySFX("회피");
                ani.SetTrigger("doDodge");
            }
            // 죽는다면
            if (curHP <= 0)
            {
                curHP = 0;
            }
            UIManager.instance.info.statusInfo.Status();
            UIManager.instance.monsterInfo.Open();
        }
        // UI 데미지 폰트 생성
        UIManager.instance.StartCoroutine(UIManager.instance.DamageUI(_damage, _isCri, this.transform.position));
    }
    // 죽음
    public virtual void Dead()
    {
        isDie = true;
    }
    // HP 회복
    public void HPHeal(int _heal)
    {
        curHP += _heal;
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
    }
}