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

    #region 슬롯 페이지 및 버튼
    // 메뉴 버튼 누르면 첫 슬롯 페이지 오픈
    public void Open(string _menuName, int _idx)
    {
        menuName = _menuName;
        slotPage = 0;
        for (int i = 0; i < _idx; i++)
        {
            slot[i].gameObject.SetActive(true);
        }
        // 슬롯 다음 페이지 있으면 다음 버튼 활성화
        if((invenName.Count - 1) > 0)
        {
            nextButton.SetActive(true);
        }
        else // 슬롯 다음 페이지가 없으면 다음 버튼 비활성화
        {
            nextButton.SetActive(false);
        }
        // 첫 페이지는 이전 버튼 비활성화
        beforeButton.SetActive(false);
        SlotShow(invenName[slotPage]);
    }
    // 다음 슬롯 페이지 버튼
    public void NextSlotPage()
    {
        slotPage++;
        // 다음 슬롯이 있다면 다음 버튼 활성화
        if((invenName.Count - 1) > slotPage)
        {
            nextButton.SetActive(true);
        }
        else // 다음 슬롯 페이지 이 없다면 다음 버튼 비활성화
        {
            nextButton.SetActive(false);
        }
        // 이전 슬롯 페이지 활성화
        beforeButton.SetActive(true);
        SlotShow(invenName[slotPage]);
    }
    // 이전 슬롯 페이지 버튼
    public void BeforeSlotPage()
    {
        slotPage--;
        nextButton.SetActive(true);
        if (slotPage == 0) beforeButton.SetActive(false);
        else beforeButton.SetActive(true);
        SlotShow(invenName[slotPage]);
    }
    #endregion

    // 슬롯 채우기
    public void SlotShow(string _invenName)
    {
        invenNameT = _invenName;
        invenNameText.text = invenNameT;
        mpImg.SetActive(false);
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].SlotClear();
        }
        if (menuName == "소모품")
        {
            for (int i = 0; i < consumSlot[invenNameT].Length; i++)
            {
                if (consumSlot[invenNameT][i].consum == null) continue;

                slot[i].gameObject.SetActive(true);
                slot[i].SlotOn(consumSlot[invenNameT][i]);
            }
        }
        else if (menuName == "장비")
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
    // 슬롯 갱신(새로고침)
    public void SlotRefresh()
    {
        SlotShow(invenNameT);
    }

    // 인벤토리 비활성화시 슬롯 비활성화
    private void OnDisable()
    {
        for(int i = 0; i< slot.Length; i++)
        {
            slot[i].gameObject.SetActive(false);
        }
    }
}