using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldPotal : MonoBehaviour
{
    public Transform transferStartPoint;    // ĳ���Ͱ� �̵��� ��ġ
    public CompositeCollider2D transBound;  // ĳ���Ͱ� �̵��� �ʵ�

    public string transFieldName;           // �̵��� �ʵ� �̸�
    public bool isTown;                     // �̵��� �ʵ尡 �������� �ƴ���

    // �ʵ� �̵���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ĳ���Ͱ� �ش� �ʵ� �ȿ� ������
        if (collision.CompareTag("Player"))
        {
            // ĳ������ �������� �����
            PlayerManager.instance.player.canMove = false;
            // ĳ������ ��ġ�� �̵��� ���� ��ġ�� �̵���Ų��
            for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
            {
                PlayerManager.instance.characterList[i].gameObject.transform.position = transferStartPoint.position;
            }
            PlayerManager.instance.curMapName = transFieldName;
            PlayerManager.instance.curBound = transBound;
            PlayerManager.instance.isTown = isTown;
            PlayerManager.instance.player.MoveStop();
            // ī�޶� �̵�(�ʵ�)
            CameraManager.instance.SetBound(transBound);
        }
    }
}
