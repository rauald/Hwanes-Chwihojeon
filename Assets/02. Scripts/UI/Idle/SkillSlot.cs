using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public ExplanationInfo explan;

    private CharacterSkill skill;

    public int number;

    public Image img;
    private Image skillImg;
    private Text skillName;

    private void Awake()
    {
        skillImg = this.transform.GetChild(0).GetComponent<Image>();
        skillName = this.transform.GetChild(1).GetComponent<Text>();
    }

    public void SkillClear()
    {
        img.color = new Color(255, 255, 255, 0f);
        skill = null;
        skillImg.sprite = null;
        skillName.text = null;

        this.gameObject.SetActive(false);
    }

    public void SkillOpen(CharacterSkill _skill)
    {
        skill = _skill;

        skillImg.sprite = skill.icon;
        skillName.text = skill.skillName;
    }

    public void SkillExplan()
    {
        img.color = new Color(255, 255, 255, 0.3f);
        explan.ExplanationShow(skill.description, skill.rank);
    }

    public void SkillChoiceCancle()
    {
        img.color = new Color(255, 255, 255, 0f);
    }
}