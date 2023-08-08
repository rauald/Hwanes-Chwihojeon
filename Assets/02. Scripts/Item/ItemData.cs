using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    // �̱���
    static public ItemData instance;   // �� �̵��� ��ä ���� ������ ���� ����

    public Equip[] equip;
    public Consum[] consum;

    private void Awake()
    {
        // �̱���
        // �ڽ��� ���ٸ� �ڽ��� ����
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        // �ڽ��� �ִٸ� �����ϰ� �ٸ��� ����
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Equip EquipFindAdd(string _name, int _idx, string _type)
    {
        int name = 0;
        int type = 0;

        if (_name == "��Ÿȣ") name = 0;
        else if (_name == "����") name = 1;
        else if (_name == "������") name = 2;

        if (_type == "����") type = 0;
        else if (_type == "��") type = 1;

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