using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using TMPro;

public class GPGSSaveAndLoad : MonoBehaviour
{
    public static GPGSSaveAndLoad Instance { get; private set; }
    protected bool isQuitting;

    protected bool isSaving;// saving or loading

    private void Awake()
    {
        if (Instance)
        {
          
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadDataWhenStart());
        } else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StartCoroutine(WindowsTestData());
        }

    }
    protected IEnumerator WindowsTestData()
    {
        // load after auth and load scene done
        while (!LoadSceneManager.Instance.IsLoadSceneDone)
        {
            yield return null;
        }
        GameData empty = new GameData();
        GameDataManager.Instance.LoadToWorld(empty);
    }
    protected IEnumerator LoadDataWhenStart()
    {
        // load after auth and load scene done
        while(!AuthenticationManager.Instance.IsAuthenticated ||
            !LoadSceneManager.Instance.IsLoadSceneDone)
        {
            yield return null;  
        }
        OpenSavedGame(false);
    }


  
    public void OpenSavedGame(bool isSaving)
    {      
        this.isSaving = isSaving;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("NewGameData", DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }
    // open save callback
    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
         
            if (isSaving) // saving
            {
                SaveGame(game);

            }
            else // loading
            {
                LoadGame(game);
            }
        }
        else
        {
            // handle error
            print("on save game open fail");
        }
    }


    void SaveGame(ISavedGameMetadata game)
    {
        GameData data = GameDataManager.Instance.GetGameData();
        string gameDataString = GameDataManager.Instance.ConvertGameDataToString(data);
        byte[] savestring = System.Text.ASCIIEncoding.ASCII.GetBytes(gameDataString);

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
           // .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savestring, OnSavedGameWritten);
      
       
       
    }

    // save game callback
    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            print("Saved");
          
        }
        else
        {
            // handle error
            print("Fail to commit");
        }
    }

    public void LoadGame(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    // load game callback
    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            string loadedString = System.Text.ASCIIEncoding.ASCII.GetString(data);
            print(loadedString);
        
            GameData loadingGameData = GameDataManager.Instance.ConvertStringToGameData(loadedString);
            GameDataManager.Instance.LoadToWorld(loadingGameData);
            print("Read successfully");
        }
        else
        {
            // handle error
            print("Read fail");

        }
    }
  
}
