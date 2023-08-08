using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    // ĳ���� ����â (ù��° ���)
    private CharacterInfo characterInfo;

    public Image[] icon = new Image[3];// ������
    public Text[] iconName = new Text[3];         // ������ ĳ���� �̸�

    public int page;

    // ĳ���� ���¹̳� (�ι�° ���)
    public StatusInfo statusInfo;

    // ����â �޴� (����° ���)
    private MenuInfo menuInfo;

    // �κ��丮 â (�׹�° ���)
    private Inventory inventory;

    // ���� �� �� ��� ������ â (�ټ���° ���)
    private ExplanationInfo explanInfo;

    private void Awake()
    {
        characterInfo = this.transform.GetChild(1).GetComponent<CharacterInfo>();
        menuInfo = this.transform.GetChild(2).GetComponent<MenuInfo>();
        inventory = this.transform.GetChild(3).GetComponent<Inventory>();
        explanInfo = this.transform.GetChild(4).GetComponent<ExplanationInfo>();
    }
    // �κ��丮 ���� (���� ù��° ĳ���� ����)
    public void Open()
    {
        page = 0;
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            icon[i].sprite = PlayerManager.instance.characterList[i].icon;
            iconName[i].text = PlayerManager.instance.characterList[i].objectName;
        }
        characterInfo.CharacterStateOpen(page);
        menuInfo.characterIdx = page;
        menuInfo.Open();
        explanInfo.Open();
    }
    // ĳ���� ����
    public void ChoiceOpen(int _idx)
    {
        page = _idx;
        characterInfo.CharacterStateOpen(page);
        menuInfo.characterIdx = page;
        menuInfo.Inventory(menuInfo.curMenu);
    }
    // ���â ����
    public void ChangeEquip()
    {
        inventory.SlotRefresh();
        characterInfo.CharacterStateOpen(page);
    }
}