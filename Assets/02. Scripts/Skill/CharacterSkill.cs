using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkill", menuName = "Skill/CharacterSkill")]
public class CharacterSkill : Skill
{
    public Sprite icon;
    public int number;
    public int removeMp;

    public string description;
    public string rank;
    public int level;
    public int maxExp;
    public int curExp;

    public int heal;

    public bool ExpUp()
    {
        if (level < 4)
        {
            int rand = Random.Range(21, 41);
            curExp += rand;
            if (curExp >= maxExp)
            {
                curExp = 0;
                RankUp();
                return true;
            }
        }
        return false;
    }
    public void RankUp()
    {
        level++;
        if(level == 2)
        {
            rank = "장기";
        }
        else if (level == 3)
        {
            rank = "달인기";
        }
        else if (level == 4)
        {
            rank = "신기";
        }
    }
}