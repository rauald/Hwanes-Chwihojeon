using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    // 싱글톤
    static public ItemData instance;   // 씬 이동시 객채 생성 방지를 위한 변수

    public Equip[] equip;
    public Consum[] consum;

    private void Awake()
    {
        // 싱글톤
        // 자신이 없다면 자신을 생성
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        // 자신이 있다면 유지하고 다른걸 삭제
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Equip EquipFindAdd(string _name, int _idx, string _type)
    {
        int name = 0;
        int type = 0;

        if (_name == "아타호") name = 0;
        else if (_name == "린샹") name = 1;
        else if (_name == "스마슈") name = 2;

        if (_type == "무기") type = 0;
        else if (_type == "방어구") type = 1;

        for (int i = 0; i < equip.Length; i++)
        {
            if((int)equip[i].character == name)
            {
                if((int)equip[i].type == type)
                {
                    if(equip[i].idx == _idx)
                    {
                        return equip[i];
                    }
                }
            }
        }
        return null;
    }

    public Consum ConsumFindAdd(int _idx)
    {
        for(int i=0; i<consum.Length; i++)
        {
            if(consum[i].idx == _idx)
            {
                return consum[i];
            }
        }

        return null;
    }
}