using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance { get; private set; }
    [SerializeField] protected GameObject loadingBackground;
    protected Slider loadingSlider;
    public bool IsLoadSceneDone { get; private set; }   

    private void Awake()
    {
        Instance = this;
        IsLoadSceneDone = false;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadingBackground.transform.root.gameObject);
        loadingSlider = loadingBackground.GetComponentInChildren<Slider>();       
        

    }



    public IEnumerator LoadSence(string sceneName, Action callback = null)
    {
        Debug.Log(loadingSlider);
        loadingBackground.SetActive(true);

        // -use load scene async here - 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        IsLoadSceneDone = false;
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.99f);
            loadingSlider.value = progress;
            yield return null;  
        }
        IsLoadSceneDone = true;


        if (callback!= null)
        {
            callback.Invoke();

        }

        loadingBackground.SetActive(false);      
    }


  


}
