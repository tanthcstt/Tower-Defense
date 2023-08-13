using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNavigation : MonoBehaviour
{
    public static GameNavigation Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);  
    }

    public void ReturnToHome()
    {
        StartCoroutine(LoadSceneManager.Instance.LoadSence("Home", () =>
        {
            MoneyManager.Instance.LoadComponent();
            MoneyManager.Instance.UpdateUI();
        }));

    }

    public void StartGame()
    {
        if (AuthenticationManager.Instance.IsAuthenticated || Application.platform == RuntimePlatform.WindowsEditor)
        {
            StartCoroutine(LoadSceneManager.Instance.LoadSence("Home"));
        }
    }

  
      
}
