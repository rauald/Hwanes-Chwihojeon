using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInfo : MonoBehaviour
{
    public Image equipBtnImg;
    public Image consumBtnImg;

    public Inventory inven;
    public string curMenu;
    public Text gold;

    public int characterIdx;

    public void Open()
    {
        Inventory("장비");
        gold.text = PlayerManager.instance.gold.ToString();
    }

    public void Inventory(string _menuName)
    {
        inven.invenName.Clear();
        switch (_menuName)
        {
            case "공격":
                inven.skillSlot.Clear();
                inven.invenName.Add("기본기");
                inven.invenName.Add("개인 스킬");
                inven.invenName.Add("전체 스킬");
                inven.invenName.Add("특수 스킬");
                inven.skillSlot.Add(inven.invenName[0], PlayerManager.instance.characterList[characterIdx].basicSkill);
                inven.skillSlot.Add(inven.invenName[1], PlayerManager.instance.characterList[characterIdx].targetSkill);
                inven.skillSlot.Add(inven.invenName[2], PlayerManager.instance.characterList[characterIdx].allSkill);
                inven.skillSlot.Add(inven.invenName[3], PlayerManager.instance.characterList[characterIdx].etcSkill);
                break;
            case "소모품":
                consumBtnImg.color = new Color(255, 255, 255, 0.25f);
                equipBtnImg.color = new Color(0, 0, 0, 1f);

                inven.consumSlot.Clear();
                inven.invenName.Add("소모품");
                inven.consumSlot.Add(inven.invenName[0], PlayerManager.instance.consumList);
                break;
            case "장비":
                equipBtnImg.color = new Color(255, 255, 255, 0.25f);
                consumBtnImg.color = new Color(0, 0, 0, 1f);

                inven.equipSlot.Clear();
                inven.invenName.Add("무기");
                inven.invenName.Add("갑옷");
                inven.equipSlot.Add(inven.invenName[0], PlayerManager.instance.characterList[characterIdx].weaponList);
                inven.equipSlot.Add(inven.invenName[1], PlayerManager.instance.characterList[characterIdx].armorList);
                break;
            case "기타":
                inven.invenName.Add("기타");
                break;
            case "공격 모드":
                inven.invenName.Add("공격 모드");
                break;
            case "환경 설정":
                inven.invenName.Add("환경 설정");
                break;
        }
        curMenu = _menuName;
        inven.Open(_menuName, 0);
    }
}