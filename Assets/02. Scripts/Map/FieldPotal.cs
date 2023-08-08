using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldPotal : MonoBehaviour
{
    public Transform transferStartPoint;    // 캐릭터가 이동할 위치
    public CompositeCollider2D transBound;  // 캐릭터가 이동될 필드

    public string transFieldName;           // 이동할 필드 이름
    public bool isTown;                     // 이동할 필드가 마을인지 아닌지

    // 필드 이동시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 캐릭터가 해당 필드 안에 들어갔으면
        if (collision.CompareTag("Player"))
        {
            // 캐릭터의 움직임을 멈춘다
            PlayerManager.instance.player.canMove = false;
            // 캐릭터의 위치를 이동한 시작 위치로 이동시킨다
            for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
            {
                PlayerManager.instance.characterList[i].gameObject.transform.position = transferStartPoint.position;
            }
            PlayerManager.instance.curMapName = transFieldName;
            PlayerManager.instance.curBound = transBound;
            PlayerManager.instance.isTown = isTown;
            PlayerManager.instance.player.MoveStop();
            // 카메라 이동(필드)
            CameraManager.instance.SetBound(transBound);
        }
    }
}
