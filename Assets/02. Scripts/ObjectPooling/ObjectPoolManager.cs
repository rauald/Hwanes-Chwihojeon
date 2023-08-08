using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public Transform[] monsterParent;
    public GameObject[] monster;

    // ���� Ǯ
    public Dictionary<string, Queue<Monster>> monsterDic = new Dictionary<string, Queue<Monster>>();
    public Queue<Monster> monkeyPool = new Queue<Monster>();
    public Queue<Monster> pigPool = new Queue<Monster>();
    
    // ��ų ����Ʈ Ǯ
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
    
    // ������ �ؽ�Ʈ Ǯ
    public Queue<Text> textPool = new Queue<Text>();

    public Transform textParent;
    public GameObject text;

    private void Awake()
    {
        // �̱���
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
        // ���� ���� (�ִ� 4����)
        for (int i = 0; i < 4; i++)
        {
            var monkey = Instantiate(monster[0], Vector3.zero, Quaternion.identity, monsterParent[0]).GetComponent<Monster>();
            monkeyPool.Enqueue(monkey);
            monkey.gameObject.SetActive(false);
        }
        monsterDic.Add("������", monkeyPool);
        for (int i = 0; i < 4; i++)
        {
            var pig = Instantiate(monster[1], Vector3.zero, Quaternion.identity, monsterParent[1]).GetComponent<Monster>();
            pigPool.Enqueue(pig);
            pig.gameObject.SetActive(false);
        }
        monsterDic.Add("�����", pigPool);
        
        // ��ų ����Ʈ ����
        // ����
        var tiger = Instantiate(skillEffect[0], Vector3.zero, Quaternion.identity, skillParent);
        tigerPool.Enqueue(tiger);
        tiger.SetActive(false);
        skillEffectDic.Add("����", tigerPool);

        // ������
        var spin = Instantiate(skillEffect[1], Vector3.zero, Quaternion.identity, skillParent);
        spinPool.Enqueue(spin);
        spin.SetActive(false);
        skillEffectDic.Add("����", spinPool);

        // ����ó ����
        for (int i = 0; i < 50; i++)
        {
            var charge = Instantiate(skillEffect[2], Vector3.zero, Quaternion.identity, skillParent);
            chargePool.Enqueue(charge);
            charge.SetActive(false);
        }
        skillEffectDic.Add("����", chargePool);

        // ������ ��
        var laser = Instantiate(skillEffect[3], Vector3.zero, Quaternion.identity, skillParent);
        laserPool.Enqueue(laser);
        laser.SetActive(false);
        skillEffectDic.Add("��", laserPool);
        
        // ������
        for (int i = 0; i < 4; i++)
        {
            var energy = Instantiate(skillEffect[4], Vector3.zero, Quaternion.identity, skillParent);
            energyPool.Enqueue(energy);
            energy.SetActive(false);
        }
        skillEffectDic.Add("������", energyPool);

        // ����
        for (int i = 0; i < 10; i++)
        {
            var meteor = Instantiate(skillEffect[5], Vector3.zero, Quaternion.identity, skillParent);
            meteorPool.Enqueue(meteor);
            meteor.SetActive(false);
        }
        skillEffectDic.Add("����", meteorPool);

        // ����
        for (int i = 0; i < 30; i++)
        {
            var dust = Instantiate(skillEffect[6], Vector3.zero, Quaternion.identity, skillParent);
            dustPool.Enqueue(dust);
            dust.SetActive(false);
        }
        skillEffectDic.Add("����", dustPool);

        // ��
        for (int i = 0; i < 30; i++)
        {
            var star = Instantiate(skillEffect[7], Vector3.zero, Quaternion.identity, skillParent);
            starPool.Enqueue(star);
            star.SetActive(false);
        }
        skillEffectDic.Add("��", starPool);
        
        // ��
        for (int i = 0; i < 4; i++)
        {
            var fire = Instantiate(skillEffect[8], Vector3.zero, Quaternion.identity, skillParent);
            firePool.Enqueue(fire);
            fire.SetActive(false);
        }
        skillEffectDic.Add("��", firePool);

        // �� ��
        var dragonHead = Instantiate(skillEffect[9], Vector3.zero, Quaternion.identity, skillParent);
        dragonHeadPool.Enqueue(dragonHead);
        dragonHead.SetActive(false);
        skillEffectDic.Add("�巡���", dragonHeadPool);

        // �� ���
        for (int i = 0; i < 100; i++)
        {
            var dragon = Instantiate(skillEffect[10], Vector3.zero, Quaternion.identity, skillParent);
            dragonPool.Enqueue(dragon);
            dragon.SetActive(false);
        }
        skillEffectDic.Add("�巡��", dragonPool);

        // �����
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
        skillEffectDic.Add("�����", shockwavePool);

        // ����
        for (int i = 0; i < 10; i++)
        {
            var lightning = Instantiate(skillEffect[12], Vector3.zero, Quaternion.identity, skillParent);
            lightningPool.Enqueue(lightning);
            lightning.SetActive(false);
        }
        skillEffectDic.Add("����", lightningPool);

        // �˼�
        for (int i = 0; i < 4; i++)
        {
            var swordline = Instantiate(skillEffect[13], Vector3.zero, Quaternion.identity, skillParent);
            swordlinePool.Enqueue(swordline);
            swordline.SetActive(false);
        }
        skillEffectDic.Add("��", swordlinePool);

        // �н�
        for (int i = 0; i < 10; i++)
        {
            var clone = Instantiate(skillEffect[14], Vector3.zero, Quaternion.identity, skillParent);
            clonePool.Enqueue(clone);
            clone.SetActive(false);
        }
        skillEffectDic.Add("�н�", clonePool);

        // ������ �ؽ�Ʈ
        for (int i = 0; i < 20; i++)
        {
            var damage = Instantiate(text, Vector3.zero, Quaternion.identity, textParent).GetComponent<Text>();
            textPool.Enqueue(damage);
            damage.gameObject.SetActive(false);
        }
    }
    // ���� �ֱ�
    public void InsertQueue(string _name, Monster _object)
    {
        monsterDic[_name].Enqueue(_object);
        _object.gameObject.SetActive(false);
    }
    // ���� ����
    public Monster GetQueue(string _name)
    {
        Monster monsterCopy = monsterDic[_name].Dequeue();
        monsterCopy.sprite.enabled = true;
        monsterCopy.gameObject.SetActive(true);

        return monsterCopy;
    }

    // ����Ʈ �ֱ�
    public void InsertQueueSkill(string _name, GameObject _object)
    {
        skillEffectDic[_name].Enqueue(_object);
        _object.SetActive(false);
    }
    // ����Ʈ ����
    public GameObject GetQueueSkill(string _name)
    {
        GameObject effect = skillEffectDic[_name].Dequeue();
        effect.transform.localScale = new Vector3(1, 1);
        effect.SetActive(true);
        return effect;
    }

    // ������ �ֱ�
    public void InsertQueueDamageText(Text _object)
    {
        textPool.Enqueue(_object);
        _object.gameObject.SetActive(false);
    }
    // ������ ����
    public Text GetQueueDamageText()
    {
        Text damageText = textPool.Dequeue();
        damageText.gameObject.SetActive(true);

        return damageText;
    }
}