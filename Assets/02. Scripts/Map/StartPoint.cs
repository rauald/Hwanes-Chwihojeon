using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // 현재 시작할 위치 이름
    public string curStartPoint;

    private void Start()
    {
        if (PlayerManager.instance.nextMapStartPointName == curStartPoint)
        {
            for (int i = 0; i < PlayerManager.instance.characterList.Count; i++)
            {
                PlayerManager.instance.characterList[i].gameObject.transform.position = this.transform.position;
            }
        }
        /*
        // 맵(씬)으로 넘어간뒤 시작할 위치 지정
        if(startPoint == characterMove.nextMapName)
        {
            // 캐릭터 위치 지정
            characterMove.transform.position = this.transform.position;
            // 카메라 위치 조정
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        }
        */
    }
}
