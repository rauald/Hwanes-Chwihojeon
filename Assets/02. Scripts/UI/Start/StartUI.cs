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
        SoundManager.instance.PlayBGM("인트로");
        if (!DataController.instance.gameData.save) newText.text = "시작하기";
        else newText.text = "이어하기";
    }

    public void GameStart()
    {
        SoundManager.instance.PlaySFX("선택");
        if (!DataController.instance.gameData.save) LoadingSceneController.LoadScene(1);
        else LoadingSceneController.LoadScene(DataController.instance.gameData.sceneIdx);
        //SceneManager.LoadScene(1);
    }
}