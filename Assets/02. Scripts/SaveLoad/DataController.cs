using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    static GameObject container;

    static DataController _instance;
    public static DataController instance
    {
        get
        {
            if (!_instance)
            {
                container = new GameObject();
                container.name = "DataController";
                _instance = container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "SaveSlot.json";

    public GameData gameData = new GameData();
    string filePath;
    // 불러오기
    public void LoadGameData()
    {
        filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            gameData = new GameData();
        }
    }
    // 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);
    }
}