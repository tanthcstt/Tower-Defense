using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIGameResult;

public class BattleResultManager : MonoBehaviour
{
    public static BattleResultManager Instance { get; private set; }
    [SerializeField] protected GameObject gameResult;
    private void Awake()
    {
        Instance = this;
    }

    public void OnBattleWin()
    {
        UIManager.Instance.resultUI.gameObject.SetActive(true);
        SOLevelData levelData = LevelManager.Instance.GetLevelData();

        int rewardKey = levelData.rewardKey;
        int rewardDiamon = levelData.rewardDiamon;
        UIManager.Instance.resultUI.LoadInfo(UIGameResult.GameResult.Win, rewardKey, rewardDiamon);
    }

    public void OnBattleLose()
    {
        gameResult.SetActive(true);
        SpawnManager.Instance.SetActive(false);
        // despawn all enemy
        BuildTurretManager.Instance.DespawnAllTurret();
        SpawnManager.Instance.DespawnAll();
        gameResult.GetComponent<UIGameResult>().LoadInfo(UIGameResult.GameResult.Lose);
    }
}
