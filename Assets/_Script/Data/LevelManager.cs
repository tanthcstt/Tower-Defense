using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] protected List<SOLevelData> levelData = new List<SOLevelData>();
    
    public int Level { get; private set; }  

    public int GetLevelAmount() { return levelData.Count; }
  

    public SOLevelData GetLevelData()
    {
        return levelData[Level - 1];
    }
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
        SetLevel(levelData.Count); 
    }

    public void SetLevel(int level)
    {
        Level = level;
    }
     
    public void LoadLevel(int level)
    {
        StartCoroutine(LoadSceneManager.Instance.LoadSence("Main"));

        SetLevel(level);    
        
    }

    public void ReloadLevel()
    {
        LoadLevel(this.Level);     

    }
}
