using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Fly,
    Walk,
}
[System.Serializable]
public class Enemy
{
    public EnemyType enemyType;
    public int HP;
    public int dropMoney;
    public GameObject prefab;
    public float rate;
}


[CreateAssetMenu(fileName ="Level Data", menuName ="Level Data/New Level Data")]
[System.Serializable]
public class SOLevelData : ScriptableObject
{

    public int level;
    public int maxEnemy;  
    public List<Enemy> enemies;

    public int rewardKey;
    public int rewardDiamon;
    public int initMoney;
   

}
