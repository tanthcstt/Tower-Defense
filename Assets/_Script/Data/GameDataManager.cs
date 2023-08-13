using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;    
        DontDestroyOnLoad(gameObject);
    }

    public string ConvertGameDataToString(GameData gameData)
    {
        string dataString = "";

        dataString += gameData.keys;
        dataString += "|";

        dataString += gameData.diamons;
        dataString += "|";

        dataString +=gameData.currentLevel;
        dataString += "|";

        for (int i = 0; i < gameData.turretUnlockedLevel.Length; i++)
        {
            dataString += gameData.turretUnlockedLevel[i];
            dataString += "|";
        }

        return dataString;
    }
    
    public GameData ConvertStringToGameData(string dataString)
    {
        print("Data string" + dataString);
        string [] dataArray = dataString.Split('|');
        GameData gameData = new GameData();
        if (dataArray.Length < 6)
        {
            print("missing field in data string");
            return gameData;
        }

        // key, diamon
        ConvertToInt(dataArray[0], ref gameData.keys);
        ConvertToInt(dataArray[1], ref gameData.diamons);

        // level
        ConvertToInt(dataArray[2], ref gameData.currentLevel);


        // unlocked turret level
        ConvertToInt(dataArray[3], ref gameData.turretUnlockedLevel[0]);
        ConvertToInt(dataArray[4], ref gameData.turretUnlockedLevel[1]);
        ConvertToInt(dataArray[5], ref gameData.turretUnlockedLevel[2]);
        ConvertToInt(dataArray[6], ref gameData.turretUnlockedLevel[3]);

        

        return gameData;
    }

    protected void ConvertToInt(string source, ref int des)
    {
        bool isSuccess = int.TryParse(source, out int val);
        if (!isSuccess)
        {
            Debug.Log("Money convert failed");
            des = 0;
        } else
        {
            des = val;      
        }


    }

    public void LoadToWorld(GameData gameData)
    {
        // set keys - diamon
        MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Key,gameData.keys);       
        MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Diamon,gameData.diamons);

        // set level
       // Debug.Log(gameData.currentLevel);

        // unlocked level
        UnlockTurretManager.Instance.LoadList(gameData.turretUnlockedLevel);

    }
    public GameData GetGameData()
    {
        GameData data = new GameData();

        data.keys = MoneyManager.Instance.CurrentKeys;
        data.diamons = MoneyManager.Instance.CurrentDiamon;
        data.currentLevel = LevelManager.Instance.Level;
        data.turretUnlockedLevel = UnlockTurretManager.Instance.UnlockLevelList;

        return  data;   
    }
}
