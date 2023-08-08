using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 씬 이동시 객채 생성 방지를 위한 변수
    static public CameraManager instance;

    // 카메라의 반높이값을 구할 속성
    public Camera theCamera;

    // 대상의 현재 위치값
    private Vector3 targetPosition;
    // 영역의 최소값 | 최대값
    private Vector3 minBound;
    private Vector3 maxBound;
    // 카메라의 가로 | 세로
    private float halfWidth;
    private float halfHeight;

    // 카메라가 얼마나 빠른 속도로
    public float moveSpeed;

    // 카메라 싸움맵으로 이동
    public bool isFight;

    // 맵 전체 영역
    [SerializeField]
    private CompositeCollider2D bound;

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
        theCamera = this.GetComponent<Camera>();
    }
    private void Start()
    {
        isFight = false;
        // 추적할 좌표
        this.transform.position = new Vector3(PlayerManager.instance.characterList[0].gameObject.transform.position.x, PlayerManager.instance.characterList[0].gameObject.transform.position.y, this.transform.position.z);
        
        // 하단 Y축 | 좌 X축 (마이너스 값)
        minBound = bound.bounds.min;
        // 상단 Y축 | 우 X측 (플러스 값)
        maxBound = bound.bounds.max;
        
        Rect rect = theCamera.rect;

        // 화면 비율 고정
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        theCamera.rect = rect;
        //theCamera.orthographicSize -> 카메라 세로 반크기
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * ((float)16/9);
    }
    void LateUpdate()
    {
        if (!isFight)
        {
            if (PlayerManager.instance.gameObject != null)
            {
                targetPosition.Set(PlayerManager.instance.characterList[0].gameObject.transform.position.x, PlayerManager.instance.characterList[0].gameObject.transform.position.y, this.transform.position.z);

                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed 만큼 이동
            }
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
        else
        {
            // 전투 좌표
            this.transform.position = new Vector3(13.5f, -18, this.transform.position.z);
        }
    }
    // 새로운 맵(씬) or 필드를 이동했다면 새로운 바운드를 받아서 넘어가기
    public void SetBound(CompositeCollider2D _newBound)
    {
        bound = _newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}