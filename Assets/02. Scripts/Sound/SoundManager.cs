using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    #region 싱글톤
    private void Awake()
    {
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
    #endregion
    public AudioSource BGM;             // 배경음
    public AudioSource SFX;             // 기본 및 타격 효과음
    public AudioSource SkillSFX;        // 스킬 효과음

    public Sound[] BGMList;             // 배경음 리스트
    public Sound[] SFXList;             // 기본 및 타격 효과음 리스트
    public Sound[] skillSFXList;        // 스킬 효과음 리스트

    public bool isBGM = true;           // 배경음 킬지 끌지
    public bool isSFX = true;           // 효과음 킬지 끌지

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < BGMList.Length; i++)
        {
            if (BGMList[i].name == _name)
            {
                BGM.clip = BGMList[i].clip;
                if (!isBGM) return;

                BGM.Play();
            }
        }
    }
    public void BGMOn()
    {
        isBGM = true;
        BGM.Play();
    }
    public void BGMOff()
    {
        isBGM = false;
        BGM.Stop();
    }

    public void PlaySFX(string _name)
    {
        for (int i = 0; i < SFXList.Length; i++)
        {
            if (SFXList[i].name == _name)
            {
                SFX.clip = SFXList[i].clip;
                if (!isSFX) return;
                SFX.Play();
            }
        }
    }
    public void PlaySkillSFX(string _name)
    {
        for (int i = 0; i < skillSFXList.Length; i++)
        {
            if(skillSFXList[i].name == _name)
            {
                SkillSFX.clip = skillSFXList[i].clip;
                if (!isSFX) return;
                SkillSFX.Play();
            }
        }
    }
    public void StopSkillSFX()
    {
        SkillSFX.Stop();
    }
    public void SFXOn()
    {
        isSFX = true;
    }
    public void SFXOff()
    {
        isSFX = false;
    }
}