using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private CharacterInfo theCharacterInfo;

    public ExplanationInfo explan;
    public Dictionary<string, List<CharacterSkill>> skillSlot = new Dictionary<string, List<CharacterSkill>>();
    public Dictionary<string, ConsumData[]> consumSlot = new Dictionary<string, ConsumData[]>();
    public Dictionary<string, List<Equip>> equipSlot = new Dictionary<string, List<Equip>>();

    public string menuName;
    public List<string> invenName = new List<string>();
    private string invenNameT;
    public Text invenNameText;

    public Slot[] slot;

    public GameObject mpImg;
    public GameObject nextButton;
    public GameObject beforeButton;

    public int slotPage;

    #region ���� ������ �� ��ư
    // �޴� ��ư ������ ù ���� ������ ����
    public void Open(string _menuName, int _idx)
    {
        menuName = _menuName;
        slotPage = 0;
        for (int i = 0; i < _idx; i++)
        {
            slot[i].gameObject.SetActive(true);
        }
        // ���� ���� ������ ������ ���� ��ư Ȱ��ȭ
        if((invenName.Count - 1) > 0)
        {
            nextButton.SetActive(true);
        }
        else // ���� ���� �������� ������ ���� ��ư ��Ȱ��ȭ
        {
            nextButton.SetActive(false);
        }
        // ù �������� ���� ��ư ��Ȱ��ȭ
        beforeButton.SetActive(false);
        SlotShow(invenName[slotPage]);
    }
    // ���� ���� ������ ��ư
    public void NextSlotPage()
    {
        slotPage++;
        // ���� ������ �ִٸ� ���� ��ư Ȱ��ȭ
        if((invenName.Count - 1) > slotPage)
        {
            nextButton.SetActive(true);
        }
        else // ���� ���� ������ �� ���ٸ� ���� ��ư ��Ȱ��ȭ
        {
            nextButton.SetActive(false);
        }
        // ���� ���� ������ Ȱ��ȭ
        beforeButton.SetActive(true);
        SlotShow(invenName[slotPage]);
    }
    // ���� ���� ������ ��ư
    public void BeforeSlotPage()
    {
        slotPage--;
        nextButton.SetActive(true);
        if (slotPage == 0) beforeButton.SetActive(false);
        else beforeButton.SetActive(true);
        SlotShow(invenName[slotPage]);
    }
    #endregion

    // ���� ä���
    public void SlotShow(string _invenName)
    {
        invenNameT = _invenName;
        invenNameText.text = invenNameT;
        mpImg.SetActive(false);
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].SlotClear();
        }
        if (menuName == "�Ҹ�ǰ")
        {
            for (int i = 0; i < consumSlot[invenNameT].Length; i++)
            {
                if (consumSlot[invenNameT][i].consum == null) continue;

                slot[i].gameObject.SetActive(true);
                slot[i].SlotOn(consumSlot[invenNameT][i]);
            }
        }
        else if (menuName == "���")
        {
            for (int i = 0; i < equipSlot[invenNameT].Count; i++)
            {
                slot[i].gameObject.SetActive(true);
                slot[i].SlotOn(equipSlot[invenNameT][i]);
            }
        }
    }

    public void SlotChoice(int _num)
    {
        theCharacterInfo.SkillSlotChoiceCancle();

        for (int i = 0; i < slot.Length; i++)
        {
            if(slot[i].number == _num) slot[i].ExplanShow();
            else slot[i].ChoiceCancle();
        }
    }

    public void SlotChoiceCancle()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].ChoiceCancle();
        }
    }
    // ���� ����(���ΰ�ħ)
    public void SlotRefresh()
    {
        SlotShow(invenNameT);
    }

    // �κ��丮 ��Ȱ��ȭ�� ���� ��Ȱ��ȭ
    private void OnDisable()
    {
        for(int i = 0; i< slot.Length; i++)
        {
            slot[i].gameObject.SetActive(false);
        }
    }
}