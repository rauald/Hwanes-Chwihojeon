using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSlot : MonoBehaviour
{
    private CharacterSkill skill;
    private ConsumData consum;

    public bool choice;
    public Image img;
    private Image icon;
    private Text slotName;
    private Text count;

    private void Awake()
    {
        icon = this.transform.GetChild(0).GetComponent<Image>();
        slotName = this.transform.GetChild(1).GetComponent<Text>();
        count = this.transform.GetChild(2).GetComponent<Text>();
    }
    public void SlotClear()
    {
        skill = null;
        consum = null;
        icon.sprite = null;
        slotName.text = null;
        count.text = null;
        this.gameObject.SetActive(false);
    }
    public void SlotOn(CharacterSkill _skill)
    {
        skill = _skill;
        icon.sprite = skill.icon;
        slotName.text = skill.skillName;
        if (skill.removeMp > 0)
        {
            if (PlayerManager.instance.characterList[UIManager.instance.turnUI.characterIdx].curMP < skill.removeMp)
            {
                count.color = new Color(255, 0, 0);
            }
            else count.color = new Color(255, 255, 255);
            count.text = skill.removeMp.ToString();
        }
        else count.text = null;
    }

    public void SlotOn(ConsumData _consum)
    {
        consum = _consum;
        icon.sprite = consum.sprite;
        slotName.text = consum.itemName;
        count.text = consum.count.ToString();
    }
    // � ��ų ���� ����
    public void Choice()
    {
        StartCoroutine(FightSlotFade());

        if (skill != null)
        {
            if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curMP >= skill.removeMp)
            {
                PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].isAtkCon = true;
                SoundManager.instance.PlaySFX("����");
                // ĳ�������� � ��ų ���� �˷��ֱ�
                PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill = skill;
                // ��ü ��ų�� �ƴϸ� ���� Ÿ����
                if (skill.type == Skill.Type.BASIC || skill.type == Skill.Type.TARGET)
                {
                    PlayerManager.instance.fight.MonsterTarget();
                }
                else
                {
                    PlayerManager.instance.fight.NextTurn();
                }
            }
            else SoundManager.instance.PlaySFX("���� �Ұ�");
        }
        else if (consum != null)
        {
            PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].isAtkCon = false;
            SoundManager.instance.PlaySFX("����");
            PlayerManager.instance.ConsumUse(consum);
            PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curConsum = consum;

            PlayerManager.instance.fight.NextTurn();
        }
    }
    public void ChoiceCancel()
    {
        choice = false;
        StopCoroutine(FightSlotFade());
        img.color = new Color(255, 255, 255, 0f);
    }
    private IEnumerator FightSlotFade()
    {
        choice = true;
        while (choice)
        {
            img.color = new Color(255, 255, 255, 0.3f);
            yield return new WaitForSeconds(0.2f);
            img.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}