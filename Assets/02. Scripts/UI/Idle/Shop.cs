using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    public ShopSlot[] slot;

    public Consum consum;
    public Equip equip;

    private string kind;            // 상점 종류
    public int curCount;            // 현재 아이템 개수
    public Text curCountText;       // 텍스트
    public int buyCount;            // 사려는 개수
    public int buyGold;             // 아이템 금액
    public Text buyCountText;       // 텍스트
    public int buyTotalGold;        // 지불 금액
    public Text buyGoldText;        // 텍스트
    public Text curGold;            // 현재 금액 텍스트

    public void ShopOpen(string _name)
    {
        // 슬롯 초기화
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].SlotClear();
        }
        // 소모품 or 장비 슬롯
        if (_name == "소모품")
        {
            kind = _name;
            for (int i = 0; i < PlayerManager.instance.player.npc.consum.Length; i++)
            {
                slot[i].gameObject.SetActive(true);
                slot[i].SlotOn(PlayerManager.instance.player.npc.consum[i]);
            }
        }
        else if(_name == "장비")
        {
            kind = _name;
            for (int i = 0; i < PlayerManager.instance.player.npc.equip.Length; i++)
            {
                slot[i].gameObject.SetActive(true);
                slot[i].SlotOn(PlayerManager.instance.player.npc.equip[i]);
            }
        }

        curCountText.text = null;
        buyCountText.text = null;
        buyGoldText.text = "G";
        curGold.text = PlayerManager.instance.gold.ToString() + " G";
    }

    // 상점 물품 클릭시
    public void ItemCount(Consum _consum)
    {
        consum = _consum;

        curCount = 0;

        for (int i = 0; i < PlayerManager.instance.consumList.Length; i++)
        {
            if (PlayerManager.instance.consumList[i].consum == consum)
            {
                curCount = PlayerManager.instance.consumList[i].count;
                break;
            }
        }

        if (curCount == 99) buyCount = 0;
        else buyCount = 1;

        buyGold = consum.gold;
        buyTotalGold = buyCount * buyGold;

        curCountText.text = curCount.ToString();
        buyCountText.text = buyCount.ToString();
        buyGoldText.text = buyTotalGold.ToString() + " G";
    }
    public void ItemCount(Equip _equip)
    {
        equip = _equip;

        curCount = 0;

        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (equip.type == Equip.Type.WEAPON)
            {
                for (int j = 0; j < PlayerManager.instance.characterList[i].weaponList.Count; j++)
                {
                    if (equip == PlayerManager.instance.characterList[i].weaponList[j])
                    {
                        curCount = 1;
                        break;
                    }
                }
            }
            else if (equip.type == Equip.Type.ARMOR)
            {
                for (int j = 0; j < PlayerManager.instance.characterList[i].armorList.Count; j++)
                {
                    if (equip == PlayerManager.instance.characterList[i].armorList[j])
                    {
                        curCount = 1;
                        break;
                    }
                }
            }
        }

        if (curCount == 1) buyCount = 0;
        else buyCount = 1;

        buyGold = equip.gold;
        buyTotalGold = buyCount * buyGold;

        curCountText.text = curCount.ToString();
        buyCountText.text = buyCount.ToString();
        buyGoldText.text = buyTotalGold.ToString() + " G";
    }

    // 상품 개수 조절
    public void AddItem()
    {
        if (equip != null) return;

        if(buyCount > 0 && buyCount < (99 - curCount))
        {
            buyCount++;
            buyTotalGold = buyCount * buyGold;

            buyCountText.text = buyCount.ToString();
            buyGoldText.text = buyTotalGold.ToString() + " G";
        }
    }
    public void RemoveItem()
    {
        if (equip != null) return;

        if (buyCount > 1 && buyCount < 100)
        {
            buyCount--;
            buyTotalGold = buyCount * buyGold;

            buyCountText.text = buyCount.ToString();
            buyGoldText.text = buyTotalGold.ToString() + " G";
        }
    }

    // 구매하기
    public void BuyItem()
    {
        if (buyCount == 0) return;

        PlayerManager.instance.gold -= buyTotalGold;

        if (kind == "소모품")
        {
            PlayerManager.instance.ConsumAdd(consum, buyCount);
            for (int i = 0; i < PlayerManager.instance.consumList.Length; i++)
            {
                if(PlayerManager.instance.consumList[i].consum == consum)
                {
                    curCount = PlayerManager.instance.consumList[i].count;
                    break;
                }
            }

            if (curCount == 99) buyCount = 0;
            else buyCount = 1;

            buyGold = consum.gold;
        }
        else if (kind == "장비")
        {
            if (equip.character == Equip.Character.Atachoe)
            {
                PlayerManager.instance.characterList[0].EquipAdd(equip);
            }
            else if (equip.character == Equip.Character.Linxiang)
            {
                PlayerManager.instance.characterList[1].EquipAdd(equip);
            }
            else if (equip.character == Equip.Character.Smash)
            {
                PlayerManager.instance.characterList[2].EquipAdd(equip);
            }

            curCount = 1;
            buyCount = 0;

            buyGold = equip.gold;
        }

        buyTotalGold = buyCount * buyGold;

        curCountText.text = curCount.ToString();
        buyCountText.text = buyCount.ToString();
        buyGoldText.text = buyTotalGold.ToString() + " G";
        curGold.text = PlayerManager.instance.gold.ToString() + " G";
    }

    // 상점 닫기
    public void ShopExit()
    {
        UIManager.instance.IdleUI();
    }
}