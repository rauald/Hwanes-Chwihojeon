using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MoveObject
{
    // 컴포넌트 연결
    private BoxCollider2D boxCollider;          // 플레이어 충돌 박스
    // 2,3 캐릭터 따라오게하기
    public int moveCount = 0;
    public string[] direction = new string[4];

    private RaycastHit2D hit;                   // 앞에 뭐가 있는지 레이저 쏴서 확인하기
    private RaycastHit2D[] rayEnt;              // NPC 확인
    public NPCManager npc;
    private int layerMask;                      // 이동불가인지 아닌지 확인할 레이어마스크

    public GameObject door;                     // 연문 닫기
    // 전투
    public int fightPercent;                    // 전투가 일어날 확률
    public float curX;                      // 전투가 일어나면 캐릭터의 현재 X 위치 저장
    public float curY;                      // 전투가 일어나면 캐릭터의 현재 Y 위치 저장

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
    #region 캐릭터 이동 및 이동시 전투 진입
    // 이동키 버튼 누를시
    public void ButtonDown(string type)
    {
        // 이동을 하고 있지 않다면
        if (!canMove)
        {
            switch (type)
            {
                // 위
                case "U":
                    up_Value = 1;
                    break;
                // 아래
                case "D":
                    down_Value = -1;
                    break;
                // 오른쪽
                case "R":
                    right_Value = 1;
                    break;
                // 왼쪽
                case "L":
                    left_Value = -1;
                    break;
            }
            // h 좌우 | v 위아래 동시 누르는거 방지 | 동시 누르면 0으로 인해 멈춤
            h[0] = right_Value + left_Value;
            v[0] = up_Value + down_Value;

            // 다른 이동키를 눌러도 현재 이동을 다끝낸뒤 움직이게 만듦
            // 전투진입 or 맵(씬)/필드 이동시 이동을 멈춤
            canMove = true;
            StartCoroutine(Move());
        }
    }
    // 이동키 버튼 땔시
    public void ButtonUp(string type)
    {
        switch (type)
        {
            // 위
            case "U":
                up_Value = 0;
                break;
            // 아래
            case "D":
                down_Value = 0;
                break;
            // 오른쪽
            case "R":
                right_Value = 0;
                break;
            // 왼쪽
            case "L":
                left_Value = 0;
                break;
        }
        canMove = false;
    }

    // 플레이어 필드 이동시 이동 멈춤
    public void MoveStop()
    {
        for(int i = 0; i < PlayerManager.instance.characterList.Count; i++)
        {
            h[i] = 0;
            v[i] = 0;
        }
        moveCount = 0;
    }

    // 플레이어 움직임
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

    // 플레이어 움직이기
    private IEnumerator Move()
    {
        while (canMove)
        {
            // 움직일 방향에 뭐가 있는지 확인 (자신과 충돌하면 안되니 자신건 잠깐 끄기)
            boxCollider.enabled = false;
            if (h[0] != 0)
            {
                // 박스 캐스트(시작 위치, 충돌 박스 크기, 회전할 각도, 방향, 거리)
                hit = Physics2D.BoxCast(PlayerManager.instance.characterList[0].gameObject.transform.position, new Vector2(0.5f, 1.5f), 0, new Vector2(h[0], 0), 2);
            }
            else if (v[0] != 0)
            {
                hit = Physics2D.BoxCast(PlayerManager.instance.characterList[0].gameObject.transform.position, new Vector2(2, 0.5f), 0, new Vector2(0, v[0]), 1.5f);
            }
            boxCollider.enabled = true;

            if (hit.transform != null)
            {
                // 문이면
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Door"))
                {
                    if (v[0] == 1)
                    {
                        // 연다
                        door = hit.transform.gameObject;
                        door.SetActive(false);
                        CharacterMove(false);
                    }
                    break;
                }
                // 벽이면
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {   // 움직임을 멈춘다
                    CharacterMove(false);
                    break;
                }
            }
            else
            {
                // 문을 열었었는데 안들어가고 나오면
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
                    // 위
                    case "U":
                        v[1] = 1;
                        break;
                    // 아래
                    case "D":
                        v[1] = -1;
                        break;
                    // 오른쪽
                    case "R":
                        h[1] = 1;
                        break;
                    // 왼쪽
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
                    // 위
                    case "U":
                        v[2] = 1;
                        break;
                    // 아래
                    case "D":
                        v[2] = -1;
                        break;
                    // 오른쪽
                    case "R":
                        h[2] = 1;
                        break;
                    // 왼쪽
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
            // 한칸(1) 가는데 걸음수(walkCount : 4) x 얼만큼 이동(speed : 0.25) = 한칸(1)
            while (currentWalkCount < walkCount)
            {
                for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
                {
                    // 좌 우
                    if (h[i] != 0)
                    {
                        PlayerManager.instance.characterList[i].gameObject.transform.Translate(h[i] * speed, 0, 0);
                        if (i > 0)
                        {
                            PlayerManager.instance.characterList[i].sprite.sortingOrder = PlayerManager.instance.characterList[i - 1].sprite.sortingOrder - 1;
                        }
                    }
                    // 상 하
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
            // 걸음수 초기화
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

            // 걸음이 끝난뒤 전투에 들어가는지 안들어가는지
            FightParcent();
        }
        // 맵 이동 되면 애니메이션 멈추기
        if (!canMove) CharacterMove(false);
    }
    
    // 플레이어 엔터키
    public void Enter()
    {
        if (h[0] != 0)
        {
            // 박스 캐스트(시작 위치, 충돌 박스 크기, 회전할 각도, 방향, 거리)
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
                    UIManager.instance.shop.ShopOpen("소모품");
                }
                else if (rayEnt[i].transform.gameObject.CompareTag("Equip"))
                {
                    npc = rayEnt[i].transform.gameObject.GetComponent<NPCManager>();
                    UIManager.instance.ShopUI();
                    UIManager.instance.shop.ShopOpen("장비");
                }
            }
        }
    }
    #endregion

    #region 전투관련
    // 전투 진입 확률
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
        // 캐릭터의 움직임을 멈춘다
        canMove = false;
        CharacterMove(false);
        // 캐릭터의 위치를 이동한 시작 위치로 이동시킨다
        curX = PlayerManager.instance.characterList[0].gameObject.transform.position.x;
        curY = PlayerManager.instance.characterList[0].gameObject.transform.position.y;
    }
    #endregion
}