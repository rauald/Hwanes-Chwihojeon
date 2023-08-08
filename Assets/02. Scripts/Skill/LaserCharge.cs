using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCharge : MonoBehaviour
{
    float x;
    float y;
    float rad;
    Vector3 cur;
    Vector3 center;
    Vector3 end;
    public int th;

    private void OnEnable()
    {
        if(PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            end = PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].transform.position + new Vector3(-1.3f, 0.75f);
            this.transform.position = end + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            //this.transform.position = cur;
            //center = (end + cur)/2;
            //rad = Vector3.Distance(center, cur);
            StartCoroutine(EffectMove());
        }
    }
    private IEnumerator EffectMove()
    {
        int time = 0;
        while (time < 10)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, end, 0.5f);
            yield return new WaitForSeconds(0.1f);
            time++;
        }
        ObjectPoolManager.instance.InsertQueueSkill("ÃæÀü", gameObject);
        /*
        int time = 0;
        while (time < 180)
        {
            var angle = Mathf.Deg2Rad * th + Vector2.Angle(cur, end);
            x = rad * Mathf.Cos(angle + Vector2.Angle(cur, end));
            y = rad * Mathf.Sin(angle + Vector2.Angle(cur, end));
            yield return new WaitForSeconds(0.005f);
            this.transform.position = center + new Vector3(x, y);
            if (th == 360) th = 0;
            else th++;
            time++;
        }
        yield return new WaitForSeconds(0.1f);
        */
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
