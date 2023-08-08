using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPotal : MonoBehaviour
{
    // �̵��� �� �̸�
    public string transSceneName;
    // �̵��� �� �̸�
    public string transMapName;
    // �� �̵� �� ���� ��ġ �̸�
    public string transStartPointName;
    // �̵��� ������ ���� ���� �ƴ���
    public bool isTown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // ĳ������ �������� �����
            PlayerManager.instance.player.canMove = false;
            PlayerManager.instance.nextMapStartPointName = transStartPointName;
            PlayerManager.instance.curMapName = transMapName;
            PlayerManager.instance.isTown = isTown;
            if (isTown) SoundManager.instance.PlayBGM("����");
            else SoundManager.instance.PlayBGM("�ʵ�");
            PlayerManager.instance.player.MoveStop();
            // �̵��� ��(��)�� ȣ���Ѵ�
            SceneManager.LoadScene(transSceneName);
        }
    }
}