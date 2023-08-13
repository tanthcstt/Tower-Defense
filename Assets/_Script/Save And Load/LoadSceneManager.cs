using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance { get; private set; }
    public bool IsLoadSceneDone { get; private set; }   

    private void Awake()
    {
        Instance = this;
        IsLoadSceneDone = false;
        DontDestroyOnLoad(gameObject);    
    }

   

    public IEnumerator LoadSence(string sceneName, Action callback = null)
    {
        // -use load scene async here - 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        IsLoadSceneDone = false;
        while(!operation.isDone)
        {
            yield return null;  
        }
        IsLoadSceneDone = true;

        if (callback!= null)
        {
            callback.Invoke();

        }
    }


  


}
