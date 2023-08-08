using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public Text newText;

    private void Start()
    {
        DataController.instance.LoadGameData();
        SoundManager.instance.PlayBGM("��Ʈ��");
        if (!DataController.instance.gameData.save) newText.text = "�����ϱ�";
        else newText.text = "�̾��ϱ�";
    }

    public void GameStart()
    {
        SoundManager.instance.PlaySFX("����");
        if (!DataController.instance.gameData.save) LoadingSceneController.LoadScene(1);
        else LoadingSceneController.LoadScene(DataController.instance.gameData.sceneIdx);
        //SceneManager.LoadScene(1);
    }
}