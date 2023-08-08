using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEffect : MonoBehaviour
{
    SpriteRenderer img;
    public Sprite[] sprite = new Sprite[3];
    float curX;
    float curY;
    float x;
    float y;
    float angle;

    private void Awake()
    {
        img = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        int time = 0;
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level == 1)
        {
            img.sprite = sprite[0];
            while (time < 40)
            {
                this.transform.Translate(new Vector2(0, 0.5f));
                yield return new WaitForSeconds(0.0125f);
                time++;
            }
        }
        else if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level == 2)
        {
            img.sprite = sprite[0];
            while (time < 18)
            {
                this.transform.Translate(new Vector2(-0.1f, 0.5f));
                yield return new WaitForSeconds(0.0125f);
                time++;
            }
            time = 0;
            while (time < 4)
            {
                this.transform.Translate(new Vector2(0, 0.5f));
                yield return new WaitForSeconds(0.0125f);
                time++;
            }
            time = 0;
            while (time < 18)
            {
                this.transform.Translate(new Vector2(0.1f, 0.5f));
                yield return new WaitForSeconds(0.0125f);
                time++;
            }
        }
        else
        {
            if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level == 3) img.sprite = sprite[1];
            else img.sprite = sprite[2];
            while (time < 10)
            {
                this.transform.Translate(new Vector2(0, 0.5f));
                yield return new WaitForSeconds(0.025f);
                time++;
            }
            time = 0;
            curX = this.transform.position.x - 1;
            curY = this.transform.position.y;
            while (time < 10)
            {
                angle = Mathf.Deg2Rad * time * 36;
                x = Mathf.Cos(angle);
                y = Mathf.Sin(angle);

                this.transform.position = new Vector2(curX, curY) + new Vector2(x, y);
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (time + 1) * 36));
                //this.transform.Translate(new Vector2(0, 0.5f));
                yield return new WaitForSeconds(0.05f);
                time++;
            }
            time = 0;
            while (time < 20)
            {
                this.transform.Translate(new Vector2(0, 0.5f));
                yield return new WaitForSeconds(0.025f);
                time++;
            }
        }
        ObjectPoolManager.instance.InsertQueueSkill("µå·¡°ï", gameObject);
    }
}