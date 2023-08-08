using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public Transform[] monsterParent;
    public GameObject[] monster;

    // 몬스터 풀
    public Dictionary<string, Queue<Monster>> monsterDic = new Dictionary<string, Queue<Monster>>();
    public Queue<Monster> monkeyPool = new Queue<Monster>();
    public Queue<Monster> pigPool = new Queue<Monster>();
    
    // 스킬 이펙트 풀
    public Dictionary<string, Queue<GameObject>> skillEffectDic = new Dictionary<string, Queue<GameObject>>();
    public Queue<GameObject> tigerPool = new Queue<GameObject>();
    public Queue<GameObject> chargePool = new Queue<GameObject>();
    public Queue<GameObject> laserPool = new Queue<GameObject>();
    public Queue<GameObject> energyPool = new Queue<GameObject>();
    public Queue<GameObject> meteorPool = new Queue<GameObject>();
    public Queue<GameObject> dustPool = new Queue<GameObject>();
    public Queue<GameObject> spinPool = new Queue<GameObject>();
    public Queue<GameObject> starPool = new Queue<GameObject>();
    public Queue<GameObject> firePool = new Queue<GameObject>();
    public Queue<GameObject> dragonHeadPool = new Queue<GameObject>();
    public Queue<GameObject> dragonPool = new Queue<GameObject>();
    public Queue<GameObject> shockwavePool = new Queue<GameObject>();
    public Queue<GameObject> lightningPool = new Queue<GameObject>();
    public Queue<GameObject> swordlinePool = new Queue<GameObject>();
    public Queue<GameObject> clonePool = new Queue<GameObject>();

    public Transform skillParent;
    public GameObject[] skillEffect;
    
    // 데미지 텍스트 풀
    public Queue<Text> textPool = new Queue<Text>();

    public Transform textParent;
    public GameObject text;

    private void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 몬스터 생성 (최대 4마리)
        for (int i = 0; i < 4; i++)
        {
            var monkey = Instantiate(monster[0], Vector3.zero, Quaternion.identity, monsterParent[0]).GetComponent<Monster>();
            monkeyPool.Enqueue(monkey);
            monkey.gameObject.SetActive(false);
        }
        monsterDic.Add("원숭이", monkeyPool);
        for (int i = 0; i < 4; i++)
        {
            var pig = Instantiate(monster[1], Vector3.zero, Quaternion.identity, monsterParent[1]).GetComponent<Monster>();
            pigPool.Enqueue(pig);
            pig.gameObject.SetActive(false);
        }
        monsterDic.Add("멧돼지", pigPool);
        
        // 스킬 이펙트 생성
        // 사자
        var tiger = Instantiate(skillEffect[0], Vector3.zero, Quaternion.identity, skillParent);
        tigerPool.Enqueue(tiger);
        tiger.SetActive(false);
        skillEffectDic.Add("사자", tigerPool);

        // 폭전축
        var spin = Instantiate(skillEffect[1], Vector3.zero, Quaternion.identity, skillParent);
        spinPool.Enqueue(spin);
        spin.SetActive(false);
        skillEffectDic.Add("스핀", spinPool);

        // 레이처 차지
        for (int i = 0; i < 50; i++)
        {
            var charge = Instantiate(skillEffect[2], Vector3.zero, Quaternion.identity, skillParent);
            chargePool.Enqueue(charge);
            charge.SetActive(false);
        }
        skillEffectDic.Add("충전", chargePool);

        // 레이저 빔
        var laser = Instantiate(skillEffect[3], Vector3.zero, Quaternion.identity, skillParent);
        laserPool.Enqueue(laser);
        laser.SetActive(false);
        skillEffectDic.Add("빔", laserPool);
        
        // 에너지
        for (int i = 0; i < 4; i++)
        {
            var energy = Instantiate(skillEffect[4], Vector3.zero, Quaternion.identity, skillParent);
            energyPool.Enqueue(energy);
            energy.SetActive(false);
        }
        skillEffectDic.Add("에너지", energyPool);

        // 유성
        for (int i = 0; i < 10; i++)
        {
            var meteor = Instantiate(skillEffect[5], Vector3.zero, Quaternion.identity, skillParent);
            meteorPool.Enqueue(meteor);
            meteor.SetActive(false);
        }
        skillEffectDic.Add("유성", meteorPool);

        // 먼지
        for (int i = 0; i < 30; i++)
        {
            var dust = Instantiate(skillEffect[6], Vector3.zero, Quaternion.identity, skillParent);
            dustPool.Enqueue(dust);
            dust.SetActive(false);
        }
        skillEffectDic.Add("먼지", dustPool);

        // 별
        for (int i = 0; i < 30; i++)
        {
            var star = Instantiate(skillEffect[7], Vector3.zero, Quaternion.identity, skillParent);
            starPool.Enqueue(star);
            star.SetActive(false);
        }
        skillEffectDic.Add("별", starPool);
        
        // 불
        for (int i = 0; i < 4; i++)
        {
            var fire = Instantiate(skillEffect[8], Vector3.zero, Quaternion.identity, skillParent);
            firePool.Enqueue(fire);
            fire.SetActive(false);
        }
        skillEffectDic.Add("불", firePool);

        // 용 얼굴
        var dragonHead = Instantiate(skillEffect[9], Vector3.zero, Quaternion.identity, skillParent);
        dragonHeadPool.Enqueue(dragonHead);
        dragonHead.SetActive(false);
        skillEffectDic.Add("드래곤얼굴", dragonHeadPool);

        // 용 비늘
        for (int i = 0; i < 100; i++)
        {
            var dragon = Instantiate(skillEffect[10], Vector3.zero, Quaternion.identity, skillParent);
            dragonPool.Enqueue(dragon);
            dragon.SetActive(false);
        }
        skillEffectDic.Add("드래곤", dragonPool);

        // 충격파
        for (int i = 0; i < 2; i++)
        {
            var shockwave = Instantiate(skillEffect[11], Vector3.zero, Quaternion.identity, skillParent);
            if (i == 1)
            {
                SpriteRenderer flip = shockwave.GetComponent<SpriteRenderer>();
                flip.flipX = true;
            }
            shockwavePool.Enqueue(shockwave);
            shockwave.SetActive(false);
        }
        skillEffectDic.Add("충격파", shockwavePool);

        // 번개
        for (int i = 0; i < 10; i++)
        {
            var lightning = Instantiate(skillEffect[12], Vector3.zero, Quaternion.identity, skillParent);
            lightningPool.Enqueue(lightning);
            lightning.SetActive(false);
        }
        skillEffectDic.Add("번개", lightningPool);

        // 검선
        for (int i = 0; i < 4; i++)
        {
            var swordline = Instantiate(skillEffect[13], Vector3.zero, Quaternion.identity, skillParent);
            swordlinePool.Enqueue(swordline);
            swordline.SetActive(false);
        }
        skillEffectDic.Add("선", swordlinePool);

        // 분신
        for (int i = 0; i < 10; i++)
        {
            var clone = Instantiate(skillEffect[14], Vector3.zero, Quaternion.identity, skillParent);
            clonePool.Enqueue(clone);
            clone.SetActive(false);
        }
        skillEffectDic.Add("분신", clonePool);

        // 데미지 텍스트
        for (int i = 0; i < 20; i++)
        {
            var damage = Instantiate(text, Vector3.zero, Quaternion.identity, textParent).GetComponent<Text>();
            textPool.Enqueue(damage);
            damage.gameObject.SetActive(false);
        }
    }
    // 몬스터 넣기
    public void InsertQueue(string _name, Monster _object)
    {
        monsterDic[_name].Enqueue(_object);
        _object.gameObject.SetActive(false);
    }
    // 몬스터 빼기
    public Monster GetQueue(string _name)
    {
        Monster monsterCopy = monsterDic[_name].Dequeue();
        monsterCopy.sprite.enabled = true;
        monsterCopy.gameObject.SetActive(true);

        return monsterCopy;
    }

    // 이펙트 넣기
    public void InsertQueueSkill(string _name, GameObject _object)
    {
        skillEffectDic[_name].Enqueue(_object);
        _object.SetActive(false);
    }
    // 이펙트 빼기
    public GameObject GetQueueSkill(string _name)
    {
        GameObject effect = skillEffectDic[_name].Dequeue();
        effect.transform.localScale = new Vector3(1, 1);
        effect.SetActive(true);
        return effect;
    }

    // 데미지 넣기
    public void InsertQueueDamageText(Text _object)
    {
        textPool.Enqueue(_object);
        _object.gameObject.SetActive(false);
    }
    // 데미지 빼기
    public Text GetQueueDamageText()
    {
        Text damageText = textPool.Dequeue();
        damageText.gameObject.SetActive(true);

        return damageText;
    }
}