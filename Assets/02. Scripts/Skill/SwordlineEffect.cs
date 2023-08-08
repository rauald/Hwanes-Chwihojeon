using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordlineEffect : MonoBehaviour
{
    SpriteRenderer img;
    int level = 0;

    private void Awake()
    {
        img = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            img.size = new Vector2(1, 0.15f);
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        level = PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level;
        int time = 0;
        while (time < 20)
        {
            img.size = new Vector2(img.size.x + level/2f, img.size.y);
            yield return new WaitForSeconds(0.025f);
            time++;
        }
        yield return new WaitForSeconds(0.5f);
        ObjectPoolManager.instance.InsertQueueSkill("¼±", gameObject);
    }
}