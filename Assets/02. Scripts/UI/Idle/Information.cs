using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    // 캐릭터 인포창 (첫번째 배경)
    private CharacterInfo characterInfo;

    public Image[] icon = new Image[3];// 아이콘
    public Text[] iconName = new Text[3];         // 아이콘 캐릭터 이름

    public int page;

    // 캐릭터 스태미나 (두번째 배경)
    public StatusInfo statusInfo;

    // 인포창 메뉴 (세번째 배경)
    private MenuInfo menuInfo;

    // 인벤토리 창 (네번째 배경)
    private Inventory inventory;

    // 설명 및 맵 골드 나오는 창 (다섯번째 배경)
    private ExplanationInfo explanInfo;

    private void Awake()
    {
        characterInfo = this.transform.GetChild(1).GetComponent<CharacterInfo>();
        menuInfo = this.transform.GetChild(2).GetComponent<MenuInfo>();
        inventory = this.transform.GetChild(3).GetComponent<Inventory>();
        explanInfo = this.transform.GetChild(4).GetComponent<ExplanationInfo>();
    }
    // 인벤토리 열기 (열면 첫번째 캐릭터 오픈)
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
    // 캐릭터 정보
    public void ChoiceOpen(int _idx)
    {
        page = _idx;
        characterInfo.CharacterStateOpen(page);
        menuInfo.characterIdx = page;
        menuInfo.Inventory(menuInfo.curMenu);
    }
    // 장비창 갱신
    public void ChangeEquip()
    {
        inventory.SlotRefresh();
        characterInfo.CharacterStateOpen(page);
    }
}