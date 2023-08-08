using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    public Image[] bgmImg;
    public Image[] sfxImg;

    public Button[] bgmBtn;
    public Button[] sfxBtn;

    private void OnEnable()
    {
        if(SoundManager.instance.isBGM)
        {
            bgmImg[0].color = new Color(0, 0, 0, 0.5f);
            bgmImg[1].color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            bgmImg[0].color = new Color(255, 255, 255, 0.5f);
            bgmImg[1].color = new Color(0, 0, 0, 0.5f);
        }

        if(SoundManager.instance.isSFX)
        {
            sfxImg[0].color = new Color(0, 0, 0, 0.5f);
            sfxImg[1].color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            sfxImg[0].color = new Color(255, 255, 255, 0.5f);
            sfxImg[1].color = new Color(0, 0, 0, 0.5f);
        }
    }

    public void BGMOn()
    {
        SoundManager.instance.BGMOn();
        bgmImg[0].color = new Color(0, 0, 0, 0.5f);
        bgmImg[1].color = new Color(255, 255, 255, 0.5f);
    }
    public void BGMOff()
    {
        SoundManager.instance.BGMOff();
        bgmImg[0].color = new Color(255, 255, 255, 0.5f);
        bgmImg[1].color = new Color(0, 0, 0, 0.5f);
    }
    public void SFXOn()
    {
        SoundManager.instance.SFXOn();
        sfxImg[0].color = new Color(0, 0, 0, 0.5f);
        sfxImg[1].color = new Color(255, 255, 255, 0.5f);
    }
    public void SFXOff()
    {
        SoundManager.instance.SFXOff();
        sfxImg[0].color = new Color(255, 255, 255, 0.5f);
        sfxImg[1].color = new Color(0, 0, 0, 0.5f);
    }
}