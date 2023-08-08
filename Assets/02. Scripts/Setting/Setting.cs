using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "setting", menuName = "Setting/setting")]
public class Setting : ScriptableObject
{
    // 이미지
    public Sprite sprite;
    // 이름
    public string setName;
    // 설명
    public string description;
}