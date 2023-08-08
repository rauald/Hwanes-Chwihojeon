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
        FightInfo("기본기");
    }

    public void FightInfo(string _menuName)
    {
        SoundManager.instance.PlaySFX("선택");

        characterIdx = PlayerManager.instance.fight.curCharacter;
        for (int i = 0; i < fightSlot.Length; i++)
        {
            fightSlot[i].SlotClear();
            fightSlot[i].ChoiceCancel();
        }
        switch (_menuName)
        {
            case "기본기":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].basicSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].basicSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "개인 스킬":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].targetSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].targetSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "단체 스킬":
                for (int i = 0; i < PlayerManager.instance.characterList[characterIdx].allSkill.Count; i++)
                {
                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.characterList[characterIdx].allSkill[i]);
                }
                mpImg.SetActive(true);
                break;
            case "소모품":
                mpImg.SetActive(false);
                for (int i = 0; i < PlayerManager.instance.consumList.Length; i++)
                {
                    if (PlayerManager.instance.consumList[i].consum == null) break;

                    fightSlot[i].gameObject.SetActive(true);
                    fightSlot[i].SlotOn(PlayerManager.instance.consumList[i]);
                }
                mpImg.SetActive(true);
                break;
            case "공격 모드":
                mpImg.SetActive(false);
                break;
            case "특수 스킬":
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