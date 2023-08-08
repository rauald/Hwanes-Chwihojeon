using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusInfo : MonoBehaviour
{
    public Text[] characterName;
    public Text[] curHp;
    public Text[] maxHp;
    public Slider[] hpSlider;
    public Text[] curMp;
    public Text[] maxMp;
    public Slider[] mpSlider;
    public Text[] curExp;
    public Text[] maxExp;
    public Slider[] expSlider;

    // 스테미나 보여짐
    public void Status()
    {
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            characterName[i].text = PlayerManager.instance.characterList[i].objectName;

            curHp[i].text = PlayerManager.instance.characterList[i].curHP.ToString();
            maxHp[i].text = PlayerManager.instance.characterList[i].maxHP.ToString();
            hpSlider[i].value = (float)PlayerManager.instance.characterList[i].curHP / (float)PlayerManager.instance.characterList[i].maxHP;

            curMp[i].text = PlayerManager.instance.characterList[i].curMP.ToString();
            maxMp[i].text = PlayerManager.instance.characterList[i].maxMP.ToString();
            mpSlider[i].value = (float)PlayerManager.instance.characterList[i].curMP / (float)PlayerManager.instance.characterList[i].maxMP;

            curExp[i].text = PlayerManager.instance.characterList[i].curEXP.ToString();
            maxExp[i].text = PlayerManager.instance.characterList[i].maxEXP.ToString();
            expSlider[i].value = (float)PlayerManager.instance.characterList[i].curEXP / (float)PlayerManager.instance.characterList[i].maxEXP;
        }
    }
    // 맞을때 스킬쓸때 레벨업할때 회복쓸때
    public IEnumerator StatusUpdate(int _damage, int _target)
    {
        while(PlayerManager.instance.characterList[_target].curHP >= (PlayerManager.instance.characterList[_target].curHP - _damage))
        {
            curHp[_target].text = PlayerManager.instance.characterList[_target].curHP.ToString();
            hpSlider[_target].value = (float)PlayerManager.instance.characterList[_target].curHP / (float)PlayerManager.instance.characterList[_target].maxHP;
        }
        yield return null;
    }
}