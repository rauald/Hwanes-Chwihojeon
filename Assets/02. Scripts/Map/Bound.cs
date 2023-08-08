using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    // �Ѿ������ �ٽ� �ٿ�� ���� ��
    [SerializeField]
    private CompositeCollider2D bound;

    // ���� �� �̸� �̸�
    public string curBound;

    private void Awake()
    {
        bound = this.GetComponent<CompositeCollider2D>();
    }

    private void Start()
    {
        if (curBound == PlayerManager.instance.curMapName)
        {
            PlayerManager.instance.curMapName = curBound;
            PlayerManager.instance.curBound = bound;
            CameraManager.instance.SetBound(bound);
        }
    }
}