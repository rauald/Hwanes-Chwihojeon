using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    public GameObject[] objectNumber;

    public Text[] characterName;
    public Text[] curHp;
    public Text[] maxHp;
    public Slider[] hpSlider;

    // �κ��丮 ����
    public void Open()
    {
        Status();
    }
    // ���׹̳� ������
    public void Status()
    {
        for (int i = 0; i < PlayerManager.instance.fight.monsterIdx; i++)
        {
            objectNumber[i].SetActive(true);
            characterName[i].text = PlayerManager.instance.fight.monsterArray[i].objectName;

            curHp[i].text = PlayerManager.instance.fight.monsterArray[i].curHP.ToString();
            maxHp[i].text = PlayerManager.instance.fight.monsterArray[i].maxHP.ToString();
            hpSlider[i].value = (float)PlayerManager.instance.fight.monsterArray[i].curHP / (float)PlayerManager.instance.fight.monsterArray[i].maxHP;
        }
    }
    // �ο��� ������ ��Ȱ�� ��Ȱ��ȭ�� ���׹̳� ��Ȱ��ȭ
    private void OnDisable()
    {
        for (int i = 0; i < objectNumber.Length; i++)
        {
            characterName[i].text = null;
            curHp[i].text = null;
            maxHp[i].text = null;
            hpSlider[i].value = 0;
            objectNumber[i].SetActive(false);
        }
    }
}
