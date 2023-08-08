using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPotal : MonoBehaviour
{
    // 이동할 씬 이름
    public string transSceneName;
    // 이동할 맵 이름
    public string transMapName;
    // 맵 이동 후 시작 위치 이름
    public string transStartPointName;
    // 이동한 지역이 마을 인지 아닌지
    public bool isTown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // 캐릭터의 움직임을 멈춘다
            PlayerManager.instance.player.canMove = false;
            PlayerManager.instance.nextMapStartPointName = transStartPointName;
            PlayerManager.instance.curMapName = transMapName;
            PlayerManager.instance.isTown = isTown;
            if (isTown) SoundManager.instance.PlayBGM("마을");
            else SoundManager.instance.PlayBGM("필드");
            PlayerManager.instance.player.MoveStop();
            // 이동할 맵(씬)을 호출한다
            SceneManager.LoadScene(transSceneName);
        }
    }
}