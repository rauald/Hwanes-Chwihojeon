using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // �̱���
    static public PlayerManager instance;   // �� �̵��� ��ä ���� ������ ���� ����

    // �� ĳ����
    public PlayerAction player;
    public Fight fight;

    // ���� ĳ���� ��
    public Character[] character;           // ��ü ĳ���� ��ũ��Ʈ
    public List<Character> characterList = new List<Character>();   // ���� ĳ���� ��ũ��Ʈ

    // ��
    public Map mapInfo;
    public string nextMapStartPointName;    // �����ʿ��� ������ ����Ʈ �̸�
    public CompositeCollider2D curBound;    // ���� �� ����
    public CompositeCollider2D fightBound;  // ���� �� ����
    public string curMapName;           // ���� �� �̸�

    public bool isTown;                     // �������� �ƴ���
    public bool isFight;                    // �ο� ���� ��

    // �Ҹ�ǰ
    public List<int> consumIdx = new List<int>();
    //public List<ConsumData> consumList = new List<ConsumData>();
    public ConsumData[] consumList = new ConsumData[6];

    public int gold;

    private void Awake()
    {
        // �̱���
        // �ڽ��� ���ٸ� �ڽ��� ����
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        // �ڽ��� �ִٸ� �����ϰ� �ٸ��� ����
        else
        {
            Destroy(this.gameObject);
        }
        character[0].isParty = true;
        character[1].isParty = true;
        character[2].isParty = true;
        CharacterCurNumber();
    }
    private void Start()
    {
        //DataController.instance.LoadGameData();
        mapInfo = GameObject.Find("Map").GetComponent<Map>();

        if (!DataController.instance.gameData.save) NewStart();
        else ContinueStart();
    }
    // ���� ���� �ϱ�
    public void NewStart()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].transform.position = new Vector2(0.5f, -2);
        }
        curMapName = "�ʽ��� ����";
        for (int i = 0; i < mapInfo.mapName.Length; i++)
        {
            if(mapInfo.mapName[i] == curMapName) CameraManager.instance.SetBound(mapInfo.mapBound[i]);
        }
        gold = 5000;
        for (int i = 0; i < consumList.Length; i++)
        {
            consumList[i].idx = 99;
        }
        isTown = true;
        isFight = false;
        if (isTown) SoundManager.instance.PlayBGM("����");
        else SoundManager.instance.PlayBGM("�ʵ�");
    }
    // �̾� �ϱ�
    public void ContinueStart()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].transform.position = DataController.instance.gameData.curPosition;
        }
        gold = DataController.instance.gameData.curGold;
        curMapName = DataController.instance.gameData.curMap;
        for (int i = 0; i < mapInfo.mapName.Length; i++)
        {
            if (mapInfo.mapName[i] == curMapName)
            {
                CameraManager.instance.SetBound(mapInfo.mapBound[i]);
            }
        }
        isTown = DataController.instance.gameData.isTown;
        isFight = DataController.instance.gameData.isFight;
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].objectName = DataController.instance.gameData.charName[i];
            characterList[i].level = DataController.instance.gameData.level[i];
            characterList[i].maxHP = DataController.instance.gameData.maxHP[i];
            characterList[i].curHP = DataController.instance.gameData.curHP[i];
            characterList[i].maxMP = DataController.instance.gameData.maxMP[i];
            characterList[i].curMP = DataController.instance.gameData.curMP[i];
            characterList[i].maxEXP = DataController.instance.gameData.maxEXP[i];
            characterList[i].curEXP = DataController.instance.gameData.curEXP[i];
            characterList[i].atk = DataController.instance.gameData.atk[i];
            characterList[i].def = DataController.instance.gameData.def[i];
            characterList[i].hit = DataController.instance.gameData.hit[i];
            characterList[i].dodge = DataController.instance.gameData.dodge[i];
            characterList[i].cri = DataController.instance.gameData.cri[i];

            characterList[i].weaponList.Clear();
            characterList[i].armorList.Clear();

            if (i == 0)
            {
                characterList[i].weaponIdx = DataController.instance.gameData.weaponList1;

                for (int j = 0; j < characterList[i].weaponIdx.Count; j++)
                {
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "����"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList1;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "��"));
                }
            }
            else if (i == 1)
            {
                characterList[i].weaponIdx = DataController.instance.gameData.weaponList2;
                for (int j = 0; j < characterList[i].weaponIdx.Count; j++)
                {
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "����"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList2;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "��"));
                }
            }
            else if (i == 2)
            {
                characterList[i].weaponIdx = DataController.instance.gameData.weaponList3;
                for (int j = 0; j < characterList[i].weaponIdx.Count; j++)
                {
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "����"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList3;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "��"));
                }
            }

            characterList[i].curWeapon = ItemData.instance.EquipFindAdd(characterList[i].objectName, DataController.instance.gameData.curWeapon[i], "����");
            characterList[i].curArmor = ItemData.instance.EquipFindAdd(characterList[i].objectName, DataController.instance.gameData.curArmor[i], "��");

            characterList[i].TotalState();
        }

        consumIdx = DataController.instance.gameData.consumList;
        for (int i = 0; i < consumIdx.Count; i++)
        {
            consumList[i].ConsumD(ItemData.instance.ConsumFindAdd(consumIdx[i]));
            consumList[i].count = DataController.instance.gameData.consumCount[i];
        }

        if (isTown) SoundManager.instance.PlayBGM("����");
        else SoundManager.instance.PlayBGM("�ʵ�");
    }
    // ����� ĳ���� ����Ʈ�� �ֱ�
    public void CharacterCurNumber()
    {
        characterList.Clear();
        for (int i = 0; i < character.Length; i++)
        {
            if (character[i].isParty) characterList.Add(character[i]);
        }
    }
    // �Ҹ�ǰ �߰�
    public void ConsumAdd(Consum _add, int _buyCount)
    {
        int _idx = 0;

        for (int i = 0; i < consumList.Length; i++)
        {
            if (consumList[i].consum == _add)
            {
                consumList[i].count += _buyCount;
                _idx = i;
                break;
            }
            else if( consumList[i].consum == null)
            {
                consumIdx.Add(_add.idx);
                consumList[i].ConsumD(_add);
                consumList[i].count += _buyCount;

                consumIdx.Sort();
                ConsumData[] sortConsum = consumList.OrderBy(x => x.idx).ToArray();
                consumList = sortConsum;
                break;
            }
        }
    }
    // �Ҹ�ǰ ���
    public void ConsumUse(ConsumData _consum)
    {
        int idx = 0;
        for (int i = 0; i < consumList.Length; i++)
        {
            if(consumList[i] == _consum)
            {
                idx = i;
                break;
            }
        }
        consumList[idx].count--;
        if (consumList[idx].count <= 0)
        {
            consumList[idx].count = 0;
            consumList[idx].ConsumClear();

            ConsumData[] sortConsum = consumList.OrderBy(x => x.idx).ToArray();
            consumList = sortConsum;
        }
    }
    // ���� ����
    public void FightStart()
    {
        Save();
        isFight = true;
        // ���̵� ��
        UIManager.instance.Fade();
    }
    // ���� ĳ���͵� ���� ���� ����
    public void FightReadyMove()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].ani.SetBool("isFight", true);
        }
        CameraManager.instance.SetBound(fightBound);
        fight.FightReady();
    }
    // ���� ��
    public void FightEnd()
    {
        isFight = false;
        UIManager.instance.Fade();
    }
    // ���� ���� �� �Ϲ� ���� ����
    public void IdleReadyMove()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].isDie = false;
            characterList[i].ani.SetBool("isDie", false);
            if (characterList[i].curHP == 0) characterList[i].curHP = 1;
            characterList[i].ani.SetBool("isFight", false);
        }
        CameraManager.instance.SetBound(curBound);
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].gameObject.transform.position = new Vector2(player.curX, player.curY);
        }
        player.MoveStop();
        UIManager.instance.IdleUI();

        if (characterList[0].level >= 10)
        {
            StopAllCoroutines();

            StartCoroutine(UIManager.instance.EndFadeOut());
        }
    }
    // ����
    private void Save()
    {
        DataController.instance.gameData.save = true;
        DataController.instance.gameData.curPosition = characterList[0].transform.position;
        DataController.instance.gameData.sceneIdx = SceneManager.GetActiveScene().buildIndex;
        DataController.instance.gameData.curGold = gold;
        DataController.instance.gameData.curMap = curMapName;
        DataController.instance.gameData.isTown = isTown;
        DataController.instance.gameData.isFight = isFight;
        for (int i = 0; i < characterList.Count; i++)
        {
            DataController.instance.gameData.SaveCharacter(i, characterList[i]);
        }
        for(int i=0;i<consumIdx.Count; i++)
        {
            DataController.instance.gameData.SaveConsum(consumList[i]);
            //DataController.instance.gameData.consumList.Add(consumIdx[i]);
        }

        DataController.instance.SaveGameData();
    }
    // ����� �ڵ� ���� (�����߿� ���� X)
    private void OnApplicationQuit()
    {
        if(!isFight) Save();
    }
}