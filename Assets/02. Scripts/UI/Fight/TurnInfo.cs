using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnInfo : MonoBehaviour
{
    public FightSlot[] fightSlot;

    public Text chatacterName;
    public Text menuName;
    public GameObject mpImg;

    public int characterIdx;

    public void Open()
    {
        FightInfo("�⺻��");
    }

    public void FightInfo(string _menuName)
    {
        SoundManager.instance.PlaySFX("����");

        characterIdx = PlayerManager.instance.fight.curCharacter;
        for (int i = 0; i < fightSlot.Length; i++)
        {
            fightSlot[i].SlotClear();
            fightSlot[i].ChoiceCancel();
        }
        switch (_menuName)
        {
            case "�⺻��":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].basicSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].basicSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "���� ��ų":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].targetSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].targetSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "��ü ��ų":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].allSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].allSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "�Ҹ�ǰ":
                mpImg.SetActive(false);
                for (int i = 0; i < PlayerManager.instance.consumList.Length; i++)
                {
                    if (PlayerManager.instance.consumList[i].consum == null) break;

                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.consumList[i]);
                }
                mpImg.SetActive(true);
                break;
            case "���� ���":
                mpImg.SetActive(false);
                break;
            case "Ư�� ��ų":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].etcSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].etcSkill[i]);
                }
                mpImg.SetActive(true);
                break;
        }
        menuName.text = _menuName;
    }

    public void FightSlotChoice(int _num)
    {
        StopAllCoroutines();
        for (int i = 0; i < fightSlot.Length; i++)
        {
            fightSlot[i].ChoiceCancel();
        }

        fightSlot[_num].Choice();
    }
}