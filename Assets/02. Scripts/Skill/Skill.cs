using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/SkillBasic")]
public class Skill : ScriptableObject
{
    public enum Type
    {
        BASIC,
        TARGET,
        ALL,
        ETC
    }
    public Type type;
    public string skillName;
    public string aniName;
    public int skillDamage;
    public int atkCount;

    public bool isPoison;
    public bool isFaint;

    public bool isSFX;
}