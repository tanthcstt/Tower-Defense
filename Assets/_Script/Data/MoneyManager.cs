using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
   
    public int CurrentMoney { get; private set; }
    public int CurrentKeys { get; private set; }
    public int CurrentDiamon { get; private set; }

    protected UIMoney moneyUI;
    [SerializeField] protected UIMoney diamonUI;
    [SerializeField] protected UIMoney keysUI;
    [SerializeField] protected UIMoney battleMoneyUI;

    public enum TradingType
    {
        Money,
        Key,
        Diamon,
    }
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {        
        UpdateUI();
    }

    public bool IsEnough(TradingType type, int cost)
    {
        return type switch
        {
            TradingType.Money => cost <= CurrentMoney,
            TradingType.Key => cost <= CurrentKeys,
            TradingType.Diamon => cost <= CurrentDiamon,
            _ => false,
        };
    }
  
    public void SetMoney(TradingType type,int cost)
    {
        switch (type)
        {
            case TradingType.Money:
                CurrentMoney += cost; 
                break;
            case TradingType.Key:   
                CurrentKeys += cost;               
                break;
            case TradingType.Diamon:
                CurrentDiamon += cost; 
                break;
        }

        UpdateUI();

        if (Application.platform != RuntimePlatform.Android) return; // save game on android
        if (type == TradingType.Key || type == TradingType.Diamon)
        {
            GPGSSaveAndLoad.Instance.OpenSavedGame(true);
        }


    }

    public void UpdateUI()
    {
     
        if (moneyUI) moneyUI.UpdateUI(CurrentMoney); 
        if (diamonUI)  diamonUI.UpdateUI(CurrentDiamon);
        if (keysUI) keysUI.UpdateUI(CurrentKeys);  
    }
   
    public void LoadComponent()
    {
        GameObject diamonUIObj = GameObject.Find("UI Money/Diamon Container");
        GameObject keyUIObj = GameObject.Find("UI Money/Key Container");
        GameObject moneyUIObj = GameObject.Find("UI/Money");

        diamonUI =(diamonUIObj != null) ? diamonUIObj.GetComponent<UIMoney>() : null;
        keysUI =(keyUIObj != null) ? keyUIObj.GetComponent<UIMoney>() : null;

        if (moneyUIObj && moneyUIObj.TryGetComponent<UIMoney>(out moneyUI))
        {
            SetBattleMoney();
        }

       
    }
   
    protected void SetBattleMoney()
    {
        int battleMoney = LevelManager.Instance.GetLevelData().initMoney;
        SetMoney(TradingType.Money,battleMoney);
    }
}
