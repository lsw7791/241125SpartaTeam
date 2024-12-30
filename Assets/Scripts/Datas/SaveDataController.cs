using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataController : MonoSingleton<SaveDataController>
{
    //public string gameDataFileName = "CharacterList.json";

    //public GameData _gameData;
    //public GameData gameData
    //{
    //    get
    //    {
    //        if(_gameData == null)
    //        {
    //            LoadGameData();
    //            SaveGameData();
    //        }
    //        return _gameData;
    //    }
    //}

    //public void LoadGameData()
    //{
    //    string filePath = Application.persistentDataPath + gameDataFileName;

    //    if(File.Exists(filePath))
    //    {
    //        Debug.Log("불러오기");
    //        string FromJsonData = File.ReadAllText(filePath);
    //        _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
    //    }
    //    else
    //    {

    //    }
    //}
}
