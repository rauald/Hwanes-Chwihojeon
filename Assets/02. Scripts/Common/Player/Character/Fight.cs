using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private string[] monsterName = new string[] { "������", "�����" };             // ���� �̸�
    public Monster[] monsterArray = new Monster[4];                                 // ���� ���� ���� �迭
    public int monsterIdx;                                                          // ���� ���� �� ��
    public GameObject[] targetButton = new GameObject[4];                           // Ÿ���� ��ư
    public RectTransform[] trans = new RectTransform[4];                            // Ÿ���� ��ư ��ġ

    public int curCharacter;                                                        // ���� ĳ���� ��ȣ
    public int curMonster;                                                          // ���� ���� ��ȣ

    private void Awake()
    {
        // ��ġ ��ư �����
        for (int i = 0; i < targetButton.Length; i++)
        {
            trans[i] = targetButton[i].transform.GetComponent<RectTransform>();
            targetButton[i].SetActive(false);
        }
    }
    // �ο� ����
    public void FightReady()
    {
        curCharacter = 0;
        FightPosition();
    }
    #region ���� �ο� �� �ڸ� ����
    public void FightPosition()
    {
        // �ο� UI �ѱ�
        UIManager.instance.FightUI();

        // ĳ���� �� �� ��ġ ����
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
        // ���� ��(����1 ~ 4 ����) ���ϱ� �� ��ġ ����(Ÿ���� ��ư�� ���� ��ġ ����)
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
    #region �� �޴� ����
    private void MenuChoice()
    {
        // ���� �޴� �ѱ�
        UIManager.instance.TurnStart();
    }
    // ���� Ÿ����
    public void MonsterTarget()
    {
        // ���� Ÿ���� ��ư �ο����� �°� Ű�� (���� ���ʹ� Ű�� �ʴ´�)
        for (int i = 0; i < monsterIdx; i++)
        {
            if (monsterArray[i].isDie) continue;

            targetButton[i].SetActive(true);
        }
    }
    // Ÿ���� �Ϸ�
    public void ChoiceFinish(int _target)
    {
        for (int i = 0; i < UIManager.instance.turnUI.fightSlot.Length; i++)
        {
            UIManager.instance.turnUI.fightSlot[i].ChoiceCancel();
        }
        
        // ���� Ÿ���� �Ѱ��� �˷��ֱ�
        PlayerManager.instance.characterList[curCharacter].target = _target;
        // Ÿ�� ��ư ��Ȱ��ȭ
        for (int i = 0; i < monsterArray.Length; i++)
        {
            if (monsterArray[i] != null)
            {
                targetButton[i].SetActive(false);
            }
        }
        NextTurn();
    }
    // Ÿ���� �� ���� �׾����� ���� �� �������� ������
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
    // ������
    public void NextTurn()
    {
        // ���� ĳ���Ͱ� ������ ���� ĳ���� �޴� ����
        if (PlayerManager.instance.characterList.Count > (curCharacter + 1))
        {
            curCharacter++;
            if (PlayerManager.instance.characterList[curCharacter].isDie) curCharacter++;
            MenuChoice();
        }
        // ���� ĳ���Ͱ� ������ �ο��
        else
        {
            curCharacter = 0;
            curMonster = 0;
            UIManager.instance.TurnEnd();
            FightStart();
        }
    }
    // �ο� ����
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
        // ĳ���� ��
        if (PlayerManager.instance.characterList.Count > curCharacter)
        {
            if (PlayerManager.instance.characterList[curCharacter].isAtkCon) PlayerManager.instance.characterList[curCharacter].StartCoroutine(PlayerManager.instance.characterList[curCharacter].SkillShow());
            else PlayerManager.instance.characterList[curCharacter].StartCoroutine(PlayerManager.instance.characterList[curCharacter].ConsumShow());
            return;
        }
        // ���� ��
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
        // ����ִ� ĳ������ �ٽ� ����
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (!PlayerManager.instance.characterList[i].isDie)
            {
                curCharacter = i;
                break;
            }
        }
        // ���� ��� �������� �ٽ� �޴� ���
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
    // �ο��� �� �����ٸ� �¸����
    public IEnumerator FightWin()
    {
        SoundManager.instance.PlayBGM("�¸�");
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            PlayerManager.instance.characterList[i].ani.SetTrigger("doFightWin");
        }
        yield return new WaitForSeconds(1.5f);
        FightEnd();
    }
    #endregion
    // �ο� ��
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