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
    // �ҷ�����
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
    // �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);
    }
}