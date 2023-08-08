using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ExplanationInfo explan;

    private Equip equip;
    private ConsumData consum;

    public Image img;
    public int number;
    private Image icon;
    private Text slotName;
    private Text count;
    private GameObject btn;
    public Text wear;

    private void Awake()
    {
        icon = this.transform.GetChild(0).GetComponent<Image>();
        slotName = this.transform.GetChild(1).GetComponent<Text>();
        count = this.transform.GetChild(2).GetComponent<Text>();
        btn = this.transform.GetChild(3).gameObject;
    }
    public void SlotClear()
    {
        consum = null;
        equip = null;
        icon.sprite = null;
        slotName.text = null;
        count.text = null;
        wear.text = null;
        this.gameObject.SetActive(false);
        btn.SetActive(false);
    }

    public void SlotOn(ConsumData _consum)
    {
        consum = _consum;
        icon.sprite = consum.sprite;
        slotName.text = consum.itemName;
        count.text = consum.count.ToString();
    }

    public void SlotOn(Equip _equip)
    {
        equip = _equip;
        icon.sprite = equip.sprite;
        slotName.text = equip.itemName;
        count.text = null;
        if (equip.use) wear.text = "착용 중";
        else wear.text = "착용";
        btn.SetActive(true);
    }

    public void ChangeInvan()
    {
        if (equip != null)
        {
            if (equip.type == Equip.Type.WEAPON)
            {
                PlayerManager.instance.characterList[UIManager.instance.info.page].curWeapon.use = false;
                PlayerManager.instance.characterList[UIManager.instance.info.page].curWeapon = equip;
                PlayerManager.instance.characterList[UIManager.instance.info.page].TotalState();
                PlayerManager.instance.characterList[UIManager.instance.info.page].curWeapon.use = true;
            }
            else if (equip.type == Equip.Type.ARMOR)
            {
                PlayerManager.instance.characterList[UIManager.instance.info.page].curArmor.use = false;
                PlayerManager.instance.characterList[UIManager.instance.info.page].curArmor = equip;
                PlayerManager.instance.characterList[UIManager.instance.info.page].TotalState();
                PlayerManager.instance.characterList[UIManager.instance.info.page].curArmor.use = true;
            }
            UIManager.instance.info.ChangeEquip();
        }
        /*
        else if (setting != null)
        {
            if(setting.setName == "배경음")
            {
                if (SoundManager.instance.isBGM)
                {
                    SoundManager.instance.BGMOff();
                    wear.text = "OFF";
                }
                else
                {
                    SoundManager.instance.BGMOn();
                    wear.text = "ON";
                }
            }
            else if (setting.setName == "효과음")
            {
                if (SoundManager.instance.isSFX)
                {
                    SoundManager.instance.SFXOff();
                    wear.text = "OFF";
                }
                else
                {
                    SoundManager.instance.SFXOn();
                    wear.text = "ON";
                }
            }
            else if (setting.setName == "게임 종료")
            {
                Application.Quit();
            }
        }
        */
    }
    /*
    public void SlotOnSetting(Setting _setting)
    {
        setting = _setting;
        icon.sprite = setting.sprite;
        slotName.text = setting.setName;
        btn.SetActive(true);
        if(setting.setName == "배경음")
        {
            if (SoundManager.instance.isBGM) wear.text = "ON";
            else wear.text = "OFF";
        }
        else if (setting.setName == "효과음")
        {
            if (SoundManager.instance.isSFX) wear.text = "ON";
            else wear.text = "OFF";
        }
        else if (setting.setName == "저장하기")
        {
            wear.text = "Save";
        }
        else if (setting.setName == "불러오기")
        {
            wear.text = "Load";
        }
        else if (setting.setName == "게임 종료")
        {
            wear.text = "종료";
        }
    }
    */
    public void ExplanShow()
    {
        img.color = new Color(255, 255, 255, 0.3f);

        if (consum != null) explan.ExplanationShow(consum.description);
        else if (equip != null) explan.ExplanationShow(equip.description);
    }

    public void ChoiceCancle()
    {
        img.color = new Color(255, 255, 255, 0f);
    }
}