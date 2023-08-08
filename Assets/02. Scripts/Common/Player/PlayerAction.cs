using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MoveObject
{
    // ������Ʈ ����
    private BoxCollider2D boxCollider;          // �÷��̾� �浹 �ڽ�
    // 2,3 ĳ���� ��������ϱ�
    public int moveCount = 0;
    public string[] direction = new string[4];

    private RaycastHit2D hit;                   // �տ� ���� �ִ��� ������ ���� Ȯ���ϱ�
    private RaycastHit2D[] rayEnt;              // NPC Ȯ��
    public NPCManager npc;
    private int layerMask;                      // �̵��Ұ����� �ƴ��� Ȯ���� ���̾��ũ

    public GameObject door;                     // ���� �ݱ�
    // ����
    public int fightPercent;                    // ������ �Ͼ Ȯ��
    public float curX;                      // ������ �Ͼ�� ĳ������ ���� X ��ġ ����
    public float curY;                      // ������ �Ͼ�� ĳ������ ���� Y ��ġ ����

    private void Awake()
    {
        boxCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        walkCount = 4;
        currentWalkCount = 0;
        speed = 0.25f;

        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (PlayerManager.instance.characterList[i].isParty)
            {
                PlayerManager.instance.characterList[i].ani.SetFloat("DirY", -1);
            }
        }

        layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Door"));
    }
    private void Update()
    {
        //if (canMove) Move();
    }
    #region ĳ���� �̵� �� �̵��� ���� ����
    // �̵�Ű ��ư ������
    public void ButtonDown(string type)
    {
        // �̵��� �ϰ� ���� �ʴٸ�
        if (!canMove)
        {
            switch (type)
            {
                // ��
                case "U":
                    up_Value = 1;
                    break;
                // �Ʒ�
                case "D":
                    down_Value = -1;
                    break;
                // ������
                case "R":
                    right_Value = 1;
                    break;
                // ����
                case "L":
                    left_Value = -1;
                    break;
            }
            // h �¿� | v ���Ʒ� ���� �����°� ���� | ���� ������ 0���� ���� ����
            h[0] = right_Value + left_Value;
            v[0] = up_Value + down_Value;

            // �ٸ� �̵�Ű�� ������ ���� �̵��� �ٳ����� �����̰� ����
            // �������� or ��(��)/�ʵ� �̵��� �̵��� ����
            canMove = true;
            StartCoroutine(Move());
        }
    }
    // �̵�Ű ��ư ����
    public void ButtonUp(string type)
    {
        switch (type)
        {
            // ��
            case "U":
                up_Value = 0;
                break;
            // �Ʒ�
            case "D":
                down_Value = 0;
                break;
            // ������
            case "R":
                right_Value = 0;
                break;
            // ����
            case "L":
                left_Value = 0;
                break;
        }
        canMove = false;
    }

    // �÷��̾� �ʵ� �̵��� �̵� ����
    public void MoveStop()
    {
        for(int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            h[i] = 0;
            v[i] = 0;
        }
        moveCount = 0;
    }

    // �÷��̾� ������
    private void CharacterMove(bool _move)
    {
        for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            if (PlayerManager.instance.characterList[i].isParty)
            {
                PlayerManager.instance.characterList[i].ani.SetBool("isWalking", _move);
            }
        }
    }

    // �÷��̾� �����̱�
    private IEnumerator Move()
    {
        while (canMove)
        {
            // ������ ���⿡ ���� �ִ��� Ȯ�� (�ڽŰ� �浹�ϸ� �ȵǴ� �ڽŰ� ��� ����)
            boxCollider.enabled = false;
            if (h[0] != 0)
            {
                // �ڽ� ĳ��Ʈ(���� ��ġ, �浹 �ڽ� ũ��, ȸ���� ����, ����, �Ÿ�)
                hit = Physics2D.BoxCast(PlayerManager.instance.characterList[0].gameObject.transform.position, new Vector2(0.5f, 1.5f), 0, new Vector2(h[0], 0), 2);
            }
            else if (v[0] != 0)
            {
                hit = Physics2D.BoxCast(PlayerManager.instance.characterList[0].gameObject.transform.position, new Vector2(2, 0.5f), 0, new Vector2(0, v[0]), 1.5f);
            }
            boxCollider.enabled = true;

            if (hit.transform != null)
            {
                // ���̸�
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Door"))
                {
                    if (v[0] == 1)
                    {
                        // ����
                        door = hit.transform.gameObject;
                        door.SetActive(false);
                        CharacterMove(false);
                    }
                    break;
                }
                // ���̸�
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {   // �������� �����
                    CharacterMove(false);
                    break;
                }
            }
            else
            {
                // ���� �������µ� �ȵ��� ������
                if (door != null)
                {
                    if (v[0] != 1 || h[0] != 0)
                    {
                        door.SetActive(true);
                        door = null;
                    }
                }
            }

            moveCount++;

            if (moveCount > 2)
            {
                v[1] = 0;
                h[1] = 0;
                switch (direction[1])
                {
                    // ��
                    case "U":
                        v[1] = 1;
                        break;
                    // �Ʒ�
                    case "D":
                        v[1] = -1;
                        break;
                    // ������
                    case "R":
                        h[1] = 1;
                        break;
                    // ����
                    case "L":
                        h[1] = -1;
                        break;
                }
            }
            if (moveCount > 4)
            {
                v[2] = 0;
                h[2] = 0;
                switch (direction[3])
                {
                    // ��
                    case "U":
                        v[2] = 1;
                        break;
                    // �Ʒ�
                    case "D":
                        v[2] = -1;
                        break;
                    // ������
                    case "R":
                        h[2] = 1;
                        break;
                    // ����
                    case "L":
                        h[2] = -1;
                        break;
                }
            }
            CharacterMove(true);
            for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
            {
                PlayerManager.instance.characterList[i].ani.SetFloat("DirX", h[i]);
                PlayerManager.instance.characterList[i].ani.SetFloat("DirY", v[i]);
            }
            // ��ĭ(1) ���µ� ������(walkCount : 4) x ��ŭ �̵�(speed : 0.25) = ��ĭ(1)
            while (currentWalkCount < walkCount)
            {
                for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
                {
                    // �� ��
                    if (h[i] != 0)
                    {
                        PlayerManager.instance.characterList[i].gameObject.transform.Translate(h[i] * speed, 0, 0);
                        if (i > 0)
                        {
                            PlayerManager.instance.characterList[i].sprite.sortingOrder = PlayerManager.instance.characterList[i - 1].sprite.sortingOrder - 1;
                        }
                    }
                    // �� ��
                    else if (v[i] != 0)
                    {
                        PlayerManager.instance.characterList[i].gameObject.transform.Translate(0, v[i] * speed, 0);
                        if (i > 0)
                        {
                            if (PlayerManager.instance.characterList[i - 1].transform.position.y - PlayerManager.instance.characterList[i].transform.position.y > 0)
                            {
                                PlayerManager.instance.characterList[i].sprite.sortingOrder = PlayerManager.instance.characterList[i - 1].sprite.sortingOrder + 1;
                            }
                            else
                            {
                                PlayerManager.instance.characterList[i].sprite.sortingOrder = PlayerManager.instance.characterList[i - 1].sprite.sortingOrder - 1;
                            }
                        }
                    }
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.02f);
            }
            yield return null;
            // ������ �ʱ�ȭ
            currentWalkCount = 0;

            bool check = false;

            for (int i = 0; i < direction.Length; i++)
            {
                if(direction[i] == null)
                {
                    if (h[0] > 0) direction[i] = "R";
                    else if (h[0] < 0) direction[i] = "L";
                    else if (v[0] > 0) direction[i] = "U";
                    else if (v[0] < 0) direction[i] = "D";
                    check = true;
                    break;
                }
            }

            if(!check)
            {
                for (int i = (direction.Length - 1); i > 0; i--)
                {
                    direction[i] = direction[i - 1];
                }
                if (h[0] > 0) direction[0] = "R";
                else if (h[0] < 0) direction[0] = "L";
                else if (v[0] > 0) direction[0] = "U";
                else if (v[0] < 0) direction[0] = "D";
            }

            // ������ ������ ������ ������ �ȵ�����
            FightParcent();
        }
        // �� �̵� �Ǹ� �ִϸ��̼� ���߱�
        if (!canMove) CharacterMove(false);
    }
    
    // �÷��̾� ����Ű
    public void Enter()
    {
        if (h[0] != 0)
        {
            // �ڽ� ĳ��Ʈ(���� ��ġ, �浹 �ڽ� ũ��, ȸ���� ����, ����, �Ÿ�)
            rayEnt = Physics2D.RaycastAll(PlayerManager.instance.characterList[0].transform.position, new Vector2(h[0], 0), 4);
            Debug.DrawRay(PlayerManager.instance.characterList[0].transform.position, new Vector2(h[0], 0) * 4, Color.red);
        }
        else if (v[0] != 0)
        {
            rayEnt = Physics2D.RaycastAll(PlayerManager.instance.characterList[0].transform.position, new Vector2(0, v[0]), 4);
            Debug.DrawRay(PlayerManager.instance.characterList[0].transform.position, new Vector2(0, v[0]) * 4, Color.red);
        }

        for (int i=0;i<rayEnt.Length;i++)
        {
            if(rayEnt[i].transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
            {
                if(rayEnt[i].transform.gameObject.CompareTag("Portion"))
                {
                    npc = rayEnt[i].transform.gameObject.GetComponent<NPCManager>();
                    UIManager.instance.ShopUI();
                    UIManager.instance.shop.ShopOpen("�Ҹ�ǰ");
                }
                else if (rayEnt[i].transform.gameObject.CompareTag("Equip"))
                {
                    npc = rayEnt[i].transform.gameObject.GetComponent<NPCManager>();
                    UIManager.instance.ShopUI();
                    UIManager.instance.shop.ShopOpen("���");
                }
            }
        }
    }
    #endregion

    #region ��������
    // ���� ���� Ȯ��
    private void FightParcent()
    {
        if (PlayerManager.instance.isTown) return;
        fightPercent = Random.Range(0, 100);
        if (fightPercent < 5)
        {
            for (int i = 0; i < direction.Length; i++)
            {
                direction[i] = null;
            }
            FightReady();
            PlayerManager.instance.FightStart();
        }
    }
    private void FightReady()
    {
        // ĳ������ �������� �����
        canMove = false;
        CharacterMove(false);
        // ĳ������ ��ġ�� �̵��� ���� ��ġ�� �̵���Ų��
        curX = PlayerManager.instance.characterList[0].gameObject.transform.position.x;
        curY = PlayerManager.instance.characterList[0].gameObject.transform.position.y;
    }
    #endregion
}