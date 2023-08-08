using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "setting", menuName = "Setting/setting")]
public class Setting : ScriptableObject
{
    // �̹���
    public Sprite sprite;
    // �̸�
    public string setName;
    // ����
    public string description;
}