using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // �� �̵��� ��ä ���� ������ ���� ����
    static public CameraManager instance;

    // ī�޶��� �ݳ��̰��� ���� �Ӽ�
    public Camera theCamera;

    // ����� ���� ��ġ��
    private Vector3 targetPosition;
    // ������ �ּҰ� | �ִ밪
    private Vector3 minBound;
    private Vector3 maxBound;
    // ī�޶��� ���� | ����
    private float halfWidth;
    private float halfHeight;

    // ī�޶� �󸶳� ���� �ӵ���
    public float moveSpeed;

    // ī�޶� �ο������ �̵�
    public bool isFight;

    // �� ��ü ����
    [SerializeField]
    private CompositeCollider2D bound;

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
        theCamera = this.GetComponent<Camera>();
    }
    private void Start()
    {
        isFight = false;
        // ������ ��ǥ
        this.transform.position = new Vector3(PlayerManager.instance.characterList[0].gameObject.transform.position.x, PlayerManager.instance.characterList[0].gameObject.transform.position.y, this.transform.position.z);
        
        // �ϴ� Y�� | �� X�� (���̳ʽ� ��)
        minBound = bound.bounds.min;
        // ��� Y�� | �� X�� (�÷��� ��)
        maxBound = bound.bounds.max;
        
        Rect rect = theCamera.rect;

        // ȭ�� ���� ����
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (���� / ����)
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
        //theCamera.orthographicSize -> ī�޶� ���� ��ũ��
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

                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1�ʿ� moveSpeed ��ŭ �̵�
            }
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
        else
        {
            // ���� ��ǥ
            this.transform.position = new Vector3(13.5f, -18, this.transform.position.z);
        }
    }
    // ���ο� ��(��) or �ʵ带 �̵��ߴٸ� ���ο� �ٿ�带 �޾Ƽ� �Ѿ��
    public void SetBound(CompositeCollider2D _newBound)
    {
        bound = _newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}