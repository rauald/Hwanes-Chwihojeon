using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    private void OnEnable()
    {
        //if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.25f);
        ObjectPoolManager.instance.InsertQueueSkill("¸ÕÁö", gameObject);
    }
}