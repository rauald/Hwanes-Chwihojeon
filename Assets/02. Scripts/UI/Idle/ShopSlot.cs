using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    private Consum consum;
    private Equip equip;

    private Image icon;
    private Text slotName;
    private Text gold;
    public Text explan;

    private void Awake()
    {
        icon = this.transform.GetChild(0).GetComponent<Image>();
        slotName = this.transform.GetChild(1).GetComponent<Text>();
        gold = this.transform.GetChild(2).GetComponent<Text>();
    }

    // ���� ��ǰ �ʱ�ȭ
    public void SlotClear()
    {
        consum = null;
        equip = null;
        icon.sprite = null;
        slotName.text = null;
        gold.text = null;
        explan.text = null;
        this.gameObject.SetActive(false);
    }
    // �Ҹ�ǰ ���� ����
    public void SlotOn(Consum _consum)
    {
        consum = _consum;
        icon.sprite = consum.sprite;
        slotName.text = consum.itemName;
        gold.text = consum.gold.ToString();
    }
    // ��� ���� ����
    public void SlotOn(Equip _equip)
    {
        equip = _equip;
        icon.sprite = equip.sprite;
        slotName.text = equip.itemName;
        gold.text = equip.gold.ToString();
    }
    // ��ǰ Ŭ���� ������ ����
    public void ExplanOpen()
    {
        if (consum != null)
        {
            explan.text = consum.description;
            UIManager.instance.shop.ItemCount(consum);
        }
        else if (equip != null)
        {
            explan.text = equip.description;
            UIManager.instance.shop.ItemCount(equip);
        }
    }
}