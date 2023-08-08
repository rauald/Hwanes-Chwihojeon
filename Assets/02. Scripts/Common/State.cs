using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public string objectName;          // ������Ʈ �̸�
    public Animator ani;               // ������Ʈ �ִϸ��̼�
    public SpriteRenderer sprite;      // ��������Ʈ ������ ����Ű��

    // �⺻
    public int level;                  // ����
    public int maxHP;                  // �ִ� ü��
    public int curHP;                  // ���� ü��

    public int atk;                    // ���ݷ�
    public int def;                    // ����
    public int hit;                    // ����� (���߷�)
    public int dodge;                  // ���߷� (ȸ����)
    public bool isDodge;               // ���߷� (ȸ����)
    public bool isCri;                 // ũ��Ƽ�� ������
    public int cri;                    // �� (ũ��Ƽ�� Ȯ��)
    protected float criAtk = 1.5f;     // ũ��Ƽ�� ������

    protected int damage;              // �� ������
    public int curAtkCount;            // ���� ������ ī��Ʈ
    protected bool dotDamage;          // ��Ʈ ������

    // �⺻ + ������ ����
    public int totalAtk;               // �⺻ + ������ ���� ���ݷ�
    public int totalDef;               // �⺻ + ������ ���� ����
    public int totalHit;               // �⺻ + ������ ���� ����� (���߷�)
    public int totalDodge;             // �⺻ + ������ ���� ���߷� (ȸ����)
    public int totalCri;               // �⺻ + ������ ���� �� (ũ��Ƽ�� Ȯ��)

    public bool isDie;                 // ����

    // ����
    protected Vector2 startPoint;          // ���� ���� ��ġ
    protected Vector2 movePoint;           // ��ų ���� �̵��� ��ġ
    public int target;                     // ���� Ÿ���� ����

    // ��ų
    protected GameObject[] skillSlot;

    protected virtual void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        ani = this.GetComponent<Animator>();
    }
    // ���� ��ġ
    public void FightPosition(Vector2 _position)
    {
        startPoint = _position;
        this.transform.position = startPoint;
    }
    // �ڸ��� ���ư���
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
    // ũ��Ƽ�� ���� �ƴ���
    protected void IsCritical()
    {
        int rand = Random.Range(0, 100) + 1;
        // ũ��Ƽ��
        if (rand < cri)
        {
            isCri = true;
            damage = (int)(damage * criAtk);
            return;
        }
        isCri = false;
    }
    /// <summary>
    /// �ǰ�
    /// </summary>
    /// <param name="_damage">�ڽ� ���ݷ�</param>
    /// <param name="_hit">�ڽ� ���߷�</param>
    public void HitDamage(int _damage, int _hit, bool _isCri, bool _sfx)
    {
        int rand = Random.Range(0, 100) + 1;
        // ȸ��
        if (rand > (_hit - dodge))
        {
            _damage = 0;
            SoundManager.instance.PlaySFX("ȸ��");
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
                        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].objectName == "������")
                        {
                            SoundManager.instance.PlaySFX(sfxName = _isCri ? "ũ��" : "��");
                        }
                        else SoundManager.instance.PlaySFX(sfxName = _isCri ? "ũ��" : "����");
                    }
                    else SoundManager.instance.PlaySFX(sfxName = _isCri ? "ũ��" : "����");
                }
                ani.SetTrigger("doHit");
            }
            else
            {
                _damage = 0;
                SoundManager.instance.PlaySFX("ȸ��");
                ani.SetTrigger("doDodge");
            }
            // �״´ٸ�
            if (curHP <= 0)
            {
                curHP = 0;
            }
            UIManager.instance.info.statusInfo.Status();
            UIManager.instance.monsterInfo.Open();
        }
        // UI ������ ��Ʈ ����
        UIManager.instance.StartCoroutine(UIManager.instance.DamageUI(_damage, _isCri, this.transform.position));
    }
    // ����
    public virtual void Dead()
    {
        isDie = true;
    }
    // HP ȸ��
    public void HPHeal(int _heal)
    {
        curHP += _heal;
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
    }
}