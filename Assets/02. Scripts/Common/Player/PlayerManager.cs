using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // 싱글톤
    static public PlayerManager instance;   // 씬 이동시 객채 생성 방지를 위한 변수

    // 맵 캐릭터
    public PlayerAction player;
    public Fight fight;

    // 전투 캐릭터 수
    public Character[] character;           // 전체 캐릭터 스크립트
    public List<Character> characterList = new List<Character>();   // 현재 캐릭터 스크립트

    // 맵
    public Map mapInfo;
    public string nextMapStartPointName;    // 다음맵에서 시작할 포인트 이름
    public CompositeCollider2D curBound;    // 현재 맵 정보
    public CompositeCollider2D fightBound;  // 전투 맵 정보
    public string curMapName;           // 현재 맵 이름

    public bool isTown;                     // 마을인지 아닌지
    public bool isFight;                    // 싸움 시작 끝

    // 소모품
    public List<int> consumIdx = new List<int>();
    //public List<ConsumData> consumList = new List<ConsumData>();
    public ConsumData[] consumList = new ConsumData[6];

    public int gold;

    private void Awake()
    {
        // 싱글톤
        // 자신이 없다면 자신을 생성
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        // 자신이 있다면 유지하고 다른걸 삭제
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
    // 새로 시작 하기
    public void NewStart()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].transform.position = new Vector2(0.5f, -2);
        }
        curMapName = "초심자 마을";
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
        if (isTown) SoundManager.instance.PlayBGM("마을");
        else SoundManager.instance.PlayBGM("필드");
    }
    // 이어 하기
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
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "무기"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList1;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "방어구"));
                }
            }
            else if (i == 1)
            {
                characterList[i].weaponIdx = DataController.instance.gameData.weaponList2;
                for (int j = 0; j < characterList[i].weaponIdx.Count; j++)
                {
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "무기"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList2;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "방어구"));
                }
            }
            else if (i == 2)
            {
                characterList[i].weaponIdx = DataController.instance.gameData.weaponList3;
                for (int j = 0; j < characterList[i].weaponIdx.Count; j++)
                {
                    characterList[i].weaponList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].weaponIdx[j], "무기"));
                }
                characterList[i].armorIdx = DataController.instance.gameData.armorList3;
                for (int j = 0; j < characterList[i].armorIdx.Count; j++)
                {
                    characterList[i].armorList.Add(ItemData.instance.EquipFindAdd(characterList[i].objectName, characterList[i].armorIdx[j], "방어구"));
                }
            }

            characterList[i].curWeapon = ItemData.instance.EquipFindAdd(characterList[i].objectName, DataController.instance.gameData.curWeapon[i], "무기");
            characterList[i].curArmor = ItemData.instance.EquipFindAdd(characterList[i].objectName, DataController.instance.gameData.curArmor[i], "방어구");

            characterList[i].TotalState();
        }

        consumIdx = DataController.instance.gameData.consumList;
        for (int i = 0; i < consumIdx.Count; i++)
        {
            consumList[i].ConsumD(ItemData.instance.ConsumFindAdd(consumIdx[i]));
            consumList[i].count = DataController.instance.gameData.consumCount[i];
        }

        if (isTown) SoundManager.instance.PlayBGM("마을");
        else SoundManager.instance.PlayBGM("필드");
    }
    // 사용할 캐릭터 리스트에 넣기
    public void CharacterCurNumber()
    {
        characterList.Clear();
        for (int i = 0; i < character.Length; i++)
        {
            if (character[i].isParty) characterList.Add(character[i]);
        }
    }
    // 소모품 추가
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
    // 소모품 사용
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
    // 전투 시작
    public void FightStart()
    {
        Save();
        isFight = true;
        // 페이드 인
        UIManager.instance.Fade();
    }
    // 현재 캐릭터들 전투 모드로 변경
    public void FightReadyMove()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].ani.SetBool("isFight", true);
        }
        CameraManager.instance.SetBound(fightBound);
        fight.FightReady();
    }
    // 전투 끝
    public void FightEnd()
    {
        isFight = false;
        UIManager.instance.Fade();
    }
    // 전투 끝난 후 일반 모드로 변경
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
    // 저장
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
    // 종료시 자동 저장 (전투중엔 저장 X)
    private void OnApplicationQuit()
    {
        if(!isFight) Save();
    }
}