using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEffect : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        ani = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            ani.SetFloat("Level", PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level);
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        int time = 0;
        while(time < 10)
        {
            this.transform.Translate(new Vector2(0.5f, -1f));
            yield return new WaitForSeconds(0.05f);
            time++;
        }
        ObjectPoolManager.instance.InsertQueueSkill("À¯¼º", gameObject);
    }
}