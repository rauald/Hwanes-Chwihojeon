using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private string[] monsterName = new string[] { "원숭이", "멧돼지" };             // 몬스터 이름
    public Monster[] monsterArray = new Monster[4];                                 // 전투 들어온 몬스터 배열
    public int monsterIdx;                                                          // 전투 몬스터 총 수
    public GameObject[] targetButton = new GameObject[4];                           // 타겟팅 버튼
    public RectTransform[] trans = new RectTransform[4];                            // 타겟팅 버튼 위치

    public int curCharacter;                                                        // 현재 캐릭터 번호
    public int curMonster;                                                          // 현재 몬스터 번호

    private void Awake()
    {
        // 터치 버튼 숨기기
        for (int i = 0; i < targetButton.Length; i++)
        {
            trans[i] = targetButton[i].transform.GetComponent<RectTransform>();
            targetButton[i].SetActive(false);
        }
    }
    // 싸움 시작
    public void FightReady()
    {
        curCharacter = 0;
        FightPosition();
    }
    #region 전투 인원 및 자리 선정
    public void FightPosition()
    {
        // 싸움 UI 켜기
        UIManager.instance.FightUI();

        // 캐릭터 수 및 위치 선정
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (PlayerManager.instance.characterList.Count == 1)
            {
                PlayerManager.instance.characterList[i].FightPosition(new Vector2(-2, -16));
            }
            else if (PlayerManager.instance.characterList.Count == 2)
            {
                PlayerManager.instance.characterList[i].FightPosition(new Vector2(-1 - (2 * i), -13 - (6 * i)));
            }
            else if (PlayerManager.instance.characterList.Count == 3)
            {
                PlayerManager.instance.characterList[i].FightPosition(new Vector2((-2 * i), -12 - (4 * i)));
            }
        }
        // 몬스터 수(랜덤1 ~ 4 마리) 정하기 및 위치 선정(타겟팅 버튼도 같이 위치 선정)
        monsterIdx = Random.Range(1, 5);
        for (int i = 0; i < monsterIdx; i++)
        {
            int rand = Random.Range(0, monsterName.Length);
            monsterArray[i] = ObjectPoolManager.instance.GetQueue(monsterName[rand]);
        }
        UIManager.instance.monsterInfo.Open();
        for (int i = 0; i < monsterIdx; i++)
        {
            if (monsterIdx == 1)
            {
                monsterArray[i].FightPosition(new Vector2(30, -16));
                trans[i].anchoredPosition = new Vector2(1040, 180);
            }
            else if (monsterIdx == 2)
            {
                monsterArray[i].FightPosition(new Vector2(30, -13 - (6 * i)));
                trans[i].anchoredPosition = new Vector2(1040, 365 - (365 * i));
            }
            else if (monsterIdx == 3)
            {
                if (i == 1)
                {
                    monsterArray[i].FightPosition(new Vector2(23, -16.5f));
                    trans[i].anchoredPosition = new Vector2(620, 150);
                }
                else
                {
                    monsterArray[i].FightPosition(new Vector2(30, -13 - (3.5f * i)));
                    trans[i].anchoredPosition = new Vector2(1040, 360 - (210 * i));
                }
            }
            else if (monsterIdx == 4)
            {
                if (i < 2)
                {
                    monsterArray[i].FightPosition(new Vector2(30 - (7 * i), -12 - i));
                    trans[i].anchoredPosition = new Vector2(1040 - (420 * i), 425 - (60 * i));
                }
                else
                {
                    monsterArray[i].FightPosition(new Vector2(23 + (6 * (i - 2)), -19 - (i - 2)));
                    trans[i].anchoredPosition = new Vector2(620 + (420 * (i - 2)), -55 * (i - 2));
                }
            }
        }
    }
    #endregion
    #region 턴 메뉴 선택
    private void MenuChoice()
    {
        // 공격 메뉴 켜기
        UIManager.instance.TurnStart();
    }
    // 몬스터 타겟팅
    public void MonsterTarget()
    {
        // 몬스터 타겟팅 버튼 인원수에 맞게 키기 (죽은 몬스터는 키지 않는다)
        for (int i = 0; i < monsterIdx; i++)
        {
            if (monsterArray[i].isDie) continue;

            targetButton[i].SetActive(true);
        }
    }
    // 타겟팅 완료
    public void ChoiceFinish(int _target)
    {
        for (int i = 0; i < UIManager.instance.turnUI.fightSlot.Length; i++)
        {
            UIManager.instance.turnUI.fightSlot[i].ChoiceCancel();
        }
        
        // 누굴 타겟팅 한건지 알려주기
        PlayerManager.instance.characterList[curCharacter].target = _target;
        // 타겟 버튼 비활성화
        for (int i = 0; i < monsterArray.Length; i++)
        {
            if (monsterArray[i] != null)
            {
                targetButton[i].SetActive(false);
            }
        }
        NextTurn();
    }
    // 타겟팅 한 적이 죽었으면 남은 적 랜덤으로 때리기
    public void ReTarget(int _target)
    {
        if(monsterArray[_target].isDie)
        {
            bool targetOn = false;
            while (!targetOn)
            {
                int rand = Random.Range(0, monsterIdx);
                if(!monsterArray[rand].isDie)
                {
                    PlayerManager.instance.characterList[curCharacter].target = rand;
                    targetOn = true;
                }
            }
        }
    }
    public void MonReTarget(int _target)
    {
        if (PlayerManager.instance.characterList[_target].isDie)
        {
            bool targetOn = false;
            while (!targetOn)
            {
                int rand = Random.Range(0, PlayerManager.instance.characterList.Count);
                if (!PlayerManager.instance.characterList[rand].isDie)
                {
                    monsterArray[curMonster].target = rand;
                    targetOn = true;
                }
            }
        }
    }
    // 다음턴
    public void NextTurn()
    {
        // 다음 캐릭터가 있으면 다음 캐릭터 메뉴 생성
        if (PlayerManager.instance.characterList.Count > (curCharacter + 1))
        {
            curCharacter++;
            if (PlayerManager.instance.characterList[curCharacter].isDie) curCharacter++;
            MenuChoice();
        }
        // 다음 캐릭터가 없으면 싸운다
        else
        {
            curCharacter = 0;
            curMonster = 0;
            UIManager.instance.TurnEnd();
            FightStart();
        }
    }
    // 싸움 시작
    public void FightStart()
    {
        int count = 0;
        for (int i = 0; i < monsterIdx; i++)
        {
            if(monsterArray[i].isDie)
            {
                count++;
            }
        }
        if (count == monsterIdx)
        {
            StartCoroutine(FightWin());
            return;
        }
        // 캐릭터 턴
        if (PlayerManager.instance.characterList.Count > curCharacter)
        {
            if (PlayerManager.instance.characterList[curCharacter].isAtkCon) PlayerManager.instance.characterList[curCharacter].StartCoroutine(PlayerManager.instance.characterList[curCharacter].SkillShow());
            else PlayerManager.instance.characterList[curCharacter].StartCoroutine(PlayerManager.instance.characterList[curCharacter].ConsumShow());
            return;
        }
        // 몬스터 턴
        else if(curMonster < monsterIdx)
        {
            for (int i = curMonster; i < monsterIdx; i++)
            {
                if (!monsterArray[i].isDie)
                {
                    curMonster = i;
                    monsterArray[curMonster].SkillShow();
                    //break;
                    return;
                }
            }
        }
        // 살아있는 캐릭부터 다시 시작
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (!PlayerManager.instance.characterList[i].isDie)
            {
                curCharacter = i;
                break;
            }
        }
        // 턴이 모두 끝났으면 다시 메뉴 출력
        for (int i = 0; i < monsterIdx; i++)
        {
            if(!monsterArray[i].isDie)
            {
                MenuChoice();
                return;
            }
        }
    }
    public IEnumerator CharacterTurnEnd()
    {
        bool isDieCheck = false;
        for (int i = 0; i < monsterIdx; i++)
        {
            if (!monsterArray[i].isDie)
            {
                if (monsterArray[i].curHP <= 0)
                {
                    monsterArray[i].Dead();
                    isDieCheck = true;
                }
            }
        }
        if (isDieCheck) yield return new WaitForSeconds(0.75f);
        else yield return null;
        curCharacter++;
        FightStart();
    }
    public IEnumerator MonsterTurnEnd()
    {
        bool isDieCheck = false;
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (!PlayerManager.instance.characterList[i].isDie)
            {
                if (PlayerManager.instance.characterList[i].curHP <= 0)
                {
                    PlayerManager.instance.characterList[i].Dead();
                    int dieIdx = 0;
                    for (int j = 0; j < PlayerManager.instance.characterList.Count; j++)
                    {
                        if (PlayerManager.instance.characterList[j].isDie)
                        {
                            dieIdx++;
                            if(dieIdx == 3)
                            {
                                FightEnd();
                                StopAllCoroutines();
                            }
                        }
                    }
                    isDieCheck = true;
                }
            }
        }
        if (isDieCheck) yield return new WaitForSeconds(0.75f);
        else yield return null;
        curMonster++;
        FightStart();
}
    // 싸움이 다 끝났다면 승리모션
    public IEnumerator FightWin()
    {
        SoundManager.instance.PlayBGM("승리");
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            PlayerManager.instance.characterList[i].ani.SetTrigger("doFightWin");
        }
        yield return new WaitForSeconds(1.5f);
        FightEnd();
    }
    #endregion
    // 싸움 끝
    private void FightEnd()
    {
        for (int i = 0; i < monsterArray.Length; i++)
        {
            if (monsterArray[i] != null)
            {
                ObjectPoolManager.instance.InsertQueue(monsterArray[i].objectName, monsterArray[i]);
                monsterArray[i] = null;
            }
        }
        PlayerManager.instance.FightEnd();
    }
}