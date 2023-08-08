using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEffect : MonoBehaviour
{
    Rigidbody2D rd;
    SpriteRenderer img;
    public Sprite[] sprite = new Sprite[7];

    private void Awake()
    {
        rd = this.GetComponent<Rigidbody2D>();
        img = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            img.sprite = sprite[0];
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        img.sprite = sprite[Random.Range(1, sprite.Length)];
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rd.AddForce(dir * 1000f);

        yield return new WaitForSeconds(PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.atkCount * 0.5f + 0.25f);
        rd.velocity = Vector2.zero;
        ObjectPoolManager.instance.InsertQueueSkill("ºÐ½Å", gameObject);
    }
}