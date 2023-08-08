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
        Inventory("���");
        gold.text = PlayerManager.instance.gold.ToString();
    }

    public void Inventory(string _menuName)
    {
        inven.invenName.Clear();
        switch (_menuName)
        {
            case "����":
                inven.skillSlot.Clear();
                inven.invenName.Add("�⺻��");
                inven.invenName.Add("���� ��ų");
                inven.invenName.Add("��ü ��ų");
                inven.invenName.Add("Ư�� ��ų");
                inven.skillSlot.Add(inven.invenName[0], PlayerManager.instance.characterList[characterIdx].basicSkill);
                inven.skillSlot.Add(inven.invenName[1], PlayerManager.instance.characterList[characterIdx].targetSkill);
                inven.skillSlot.Add(inven.invenName[2], PlayerManager.instance.characterList[characterIdx].allSkill);
                inven.skillSlot.Add(inven.invenName[3], PlayerManager.instance.characterList[characterIdx].etcSkill);
                break;
            case "�Ҹ�ǰ":
                consumBtnImg.color = new Color(255, 255, 255, 0.25f);
                equipBtnImg.color = new Color(0, 0, 0, 1f);

                inven.consumSlot.Clear();
                inven.invenName.Add("�Ҹ�ǰ");
                inven.consumSlot.Add(inven.invenName[0], PlayerManager.instance.consumList);
                break;
            case "���":
                equipBtnImg.color = new Color(255, 255, 255, 0.25f);
                consumBtnImg.color = new Color(0, 0, 0, 1f);

                inven.equipSlot.Clear();
                inven.invenName.Add("����");
                inven.invenName.Add("����");
                inven.equipSlot.Add(inven.invenName[0], PlayerManager.instance.characterList[characterIdx].weaponList);
                inven.equipSlot.Add(inven.invenName[1], PlayerManager.instance.characterList[characterIdx].armorList);
                break;
            case "��Ÿ":
                inven.invenName.Add("��Ÿ");
                break;
            case "���� ���":
                inven.invenName.Add("���� ���");
                break;
            case "ȯ�� ����":
                inven.invenName.Add("ȯ�� ����");
                break;
        }
        curMenu = _menuName;
        inven.Open(_menuName, 0);
    }
}