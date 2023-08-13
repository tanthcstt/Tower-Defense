using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameResult : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI titleTxt;
    [SerializeField] protected TextMeshProUGUI moneyAmount;
    [SerializeField] protected TextMeshProUGUI diamonAmount;
    [SerializeField] protected Button homeBtn;
    [SerializeField] protected Button replayButton;

    public enum GameResult
    {
        Win,
        Lose,
    }

  
    public void LoadInfo(GameResult result, int rewardKey = 0, int rewardDiamon = 0)
    {
        if (result == GameResult.Win)
        {
            titleTxt.text = "Victory";
            AddLisnter(rewardKey, rewardDiamon);

        }
        else if (result == GameResult.Lose)
        {
            titleTxt.text = "Defeat";
            AddLisnter(0, 0);

        }

        moneyAmount.text = rewardKey.ToString();
        diamonAmount.text = rewardDiamon.ToString();

      
    }

    public void AddLisnter(int rewardKey, int rewardDiamon)
    {
        homeBtn.onClick.AddListener(delegate {

            MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Key, rewardKey);
            MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Diamon, rewardDiamon);
        
            GameNavigation.Instance.ReturnToHome();

        });
        replayButton.onClick.AddListener(LevelManager.Instance.ReloadLevel);
    }

}
