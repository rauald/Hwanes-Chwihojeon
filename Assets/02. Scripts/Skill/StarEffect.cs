using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffect : MonoBehaviour
{
    private void OnEnable()
    {
        if (!PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].isAtkCon)
        {
            StartCoroutine(ConsumMove());
        }
        else if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        int time = 0;
        float rand = Random.Range(-0.2f, 0.2f);
        while(time < 10)
        {
            this.transform.Translate(new Vector2(-0.2f, rand));
            yield return new WaitForSeconds(0.025f);
            time++;
        }
        ObjectPoolManager.instance.InsertQueueSkill("º°", gameObject);
    }
    private IEnumerator ConsumMove()
    {
        int time = 0;
        float rand = Random.Range(-0.3f, 0.3f);
        while(time < 10)
        {
            if(time < 5) this.transform.Translate(new Vector2(rand, 0.5f));
            else this.transform.Translate(new Vector2(rand, -0.2f));
            yield return new WaitForSeconds(0.025f);
            time++;
        }
        ObjectPoolManager.instance.InsertQueueSkill("º°", gameObject);
    }
}