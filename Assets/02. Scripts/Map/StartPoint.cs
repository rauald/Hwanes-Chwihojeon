using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // ���� ������ ��ġ �̸�
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
        // ��(��)���� �Ѿ�� ������ ��ġ ����
        if(startPoint == characterMove.nextMapName)
        {
            // ĳ���� ��ġ ����
            characterMove.transform.position = this.transform.position;
            // ī�޶� ��ġ ����
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        }
        */
    }
}
