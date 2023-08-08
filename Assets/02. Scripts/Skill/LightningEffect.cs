using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    SpriteRenderer img;
    Sprite sprite;
    int time;

    private void Awake()
    {
        img = this.GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            img.size = new Vector2(2, 2);
            time = 0;
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        while(time < 5)
        {
            img.size = new Vector2(img.size.x, img.size.y + 2);
            yield return new WaitForSeconds(0.05f);
            time++;
        }
        yield return new WaitForSeconds(0.25f);
        ObjectPoolManager.instance.InsertQueueSkill("¹ø°³", gameObject);
    }
}