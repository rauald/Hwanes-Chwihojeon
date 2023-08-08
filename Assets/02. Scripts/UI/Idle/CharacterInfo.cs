using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    public GameObject characterState;
    public GameObject characterSkill;

    public Image stateBtnImg;
    public Image skillBtnImg;

    private int page;

    // ĳ���� ����â
    public Text characterName;      // �̸�
    public Image profile;           // ������
    public Image weaponImg;        // ����
    public Text weaponName;         // ���� �̸�
    public Image armorImg;         // ��
    public Text armorName;          // �� �̸�

    public Text level;              // ����
    public Text curHp;              // ���� ü��
    public Text maxHp;              // �ִ� ü��
    public Text curMp;              // ���� ����
    public Text maxMp;              // �ִ� ����
    public Text curExp;             // ���� ����ġ
    public Text maxExp;             // �ִ� ����ġ
    public Text atk;                // ���ݷ�
    public Text def;                // ����
    public Text hit;                // ����� (���߷�)
    public Text dodge;              // ���߷� (ȸ����)
    public Text cri;                // �� (ũ��Ƽ�� Ȯ��) 

    // ĳ���� ��ųâ
    public SkillSlot[] basicSkillSlot = new SkillSlot[5];
    public SkillSlot[] targetSkillSlot = new SkillSlot[5];
    public SkillSlot[] allSkillSlot = new SkillSlot[5];

    public void Open()
    {
        characterName.text = PlayerManager.instance.characterList[page].objectName;
        profile.sprite = PlayerManager.instance.characterList[page].profile;
        weaponImg.sprite = PlayerManager.instance.characterList[page].curWeapon.sprite;
        weaponName.text = PlayerManager.instance.characterList[page].curWeapon.itemName;
        armorImg.sprite = PlayerManager.instance.characterList[page].curArmor.sprite;
        armorName.text = PlayerManager.instance.characterList[page].curArmor.itemName;

        level.text = PlayerManager.instance.characterList[page].level.ToString();
        curHp.text = PlayerManager.instance.characterList[page].curHP.ToString();
        maxHp.text = PlayerManager.instance.characterList[page].maxHP.ToString();
        curMp.text = PlayerManager.instance.characterList[page].curMP.ToString();
        maxMp.text = PlayerManager.instance.characterList[page].maxMP.ToString();
        curExp.text = PlayerManager.instance.characterList[page].curEXP.ToString();
        maxExp.text = PlayerManager.instance.characterList[page].maxEXP.ToString();
        atk.text = PlayerManager.instance.characterList[page].totalAtk.ToString();
        def.text = PlayerManager.instance.characterList[page].totalDef.ToString();
        hit.text = PlayerManager.instance.characterList[page].totalHit.ToString();
        dodge.text = PlayerManager.instance.characterList[page].totalDodge.ToString();
        cri.text = PlayerManager.instance.characterList[page].totalCri.ToString();
    }
    // ������ ���ý�
    public void CharacterStateOpen(int _page)
    {
        page = _page;

        characterSkill.SetActive(false);
        characterState.SetActive(true);

        stateBtnImg.color = new Color(255, 255, 255, 0.25f);
        skillBtnImg.color = new Color(0, 0, 0, 1f);

        Open();
    }
    // ����â Ŭ����
    public void CharacterStateOpen()
    {
        characterSkill.SetActive(false);
        characterState.SetActive(true);

        stateBtnImg.color = new Color(255, 255, 255, 0.25f);
        skillBtnImg.color = new Color(0, 0, 0, 1f);

        Open();
    }
    public void CharacterSkillOpen()
    {
        characterState.SetActive(false);
        characterSkill.SetActive(true);

        stateBtnImg.color = new Color(0, 0, 0, 1f);
        skillBtnImg.color = new Color(255, 255, 255, 0.25f);

        SkillOpen(page);
    }

    private void SkillOpen(int _page)
    {
        // ���� �ʱ�ȭ
        for (int i = 0; i < basicSkillSlot.Length; i++)
        {
            basicSkillSlot[i].SkillClear();
            targetSkillSlot[i].SkillClear();
            allSkillSlot[i].SkillClear();
        }

        for (int i = 0; i < PlayerManager.instance.characterList[_page].basicSkill.Count; i++)
        {
            basicSkillSlot[i].gameObject.SetActive(true);
            basicSkillSlot[i].SkillOpen(PlayerManager.instance.characterList[_page].basicSkill[i]);
        }

        for (int i = 0; i < PlayerManager.instance.characterList[_page].targetSkill.Count; i++)
        {
            targetSkillSlot[i].gameObject.SetActive(true);
            targetSkillSlot[i].SkillOpen(PlayerManager.instance.characterList[_page].targetSkill[i]);
        }

        for (int i = 0; i < PlayerManager.instance.characterList[_page].allSkill.Count; i++)
        {
            allSkillSlot[i].gameObject.SetActive(true);
            allSkillSlot[i].SkillOpen(PlayerManager.instance.characterList[_page].allSkill[i]);
        }
    }
    
    public void SkillSlotCoice(int _num)
    {
        theInventory.SlotChoiceCancle();

        for (int i = 0; i < basicSkillSlot.Length; i++)
        {
            basicSkillSlot[i].SkillChoiceCancle();
            targetSkillSlot[i].SkillChoiceCancle();
            allSkillSlot[i].SkillChoiceCancle();

            if (basicSkillSlot[i].number == _num)
            {
                basicSkillSlot[i].SkillExplan();
            }
            else if (targetSkillSlot[i].number == _num)
            {
                targetSkillSlot[i].SkillExplan();
            }
            else if (allSkillSlot[i].number == _num)
            {
                allSkillSlot[i].SkillExplan();
            }
        }
    }

    public void SkillSlotChoiceCancle()
    {
        for (int i = 0; i < basicSkillSlot.Length; i++)
        {
            basicSkillSlot[i].SkillChoiceCancle();
            targetSkillSlot[i].SkillChoiceCancle();
            allSkillSlot[i].SkillChoiceCancle();
        }
    }
}