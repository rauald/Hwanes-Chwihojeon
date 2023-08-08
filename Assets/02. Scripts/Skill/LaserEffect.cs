using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        ani = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill != null)
        {
            ani.SetFloat("Level", PlayerManager.instance.characterList[PlayerManager.instance.fight.curCharacter].curSkill.level);
        }
    }
}
