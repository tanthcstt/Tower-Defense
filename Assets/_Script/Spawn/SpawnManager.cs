using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }   
    [SerializeField] protected float spawnDelay;
    protected SOLevelData enemyData;
    [SerializeField] protected Transform[] spawnTransformWalk;
    [SerializeField] protected Transform[] spawnTransformFly;
    protected List<GameObject> spawnedList = new List<GameObject>();    
    protected int spawnedEnemy = 0;
    protected bool isForceStop;
    protected int prevPosition = 0;
    private void OnEnable()
    {
        isForceStop = false;    
    }

    private void Awake()
    {
        Instance = this;
        SetActive(true);
    }

    private void Start()
    {
        enemyData = LevelManager.Instance.GetLevelData();
        StartCoroutine(Spawn());

    }

    public void SetActive(bool state)
    {
        isForceStop = !state;
    }

   

    protected Enemy GetEnemyByRate()
    {
        // Calculate cumulative probabilities
        float totalRate = 0f;
        foreach (Enemy enemy in enemyData.enemies)
        {
            totalRate += enemy.rate;
        }

        // Generate a random number between 0 and the total rate
        float randomValue = Random.Range(0f, totalRate);

        foreach (Enemy enemy in enemyData.enemies)
        {

            if (randomValue <= enemy.rate)
            {
                return enemy;
            }
            randomValue -= enemy.rate;
        }
        return null;    
    }

  
    public IEnumerator Spawn()
    {
        while (spawnedEnemy < enemyData.maxEnemy)
        {
            if (isForceStop) yield break;   

            Enemy enemy = GetEnemyByRate();         
            Transform radomTransform = GetRandomTranform(enemy.enemyType);
            GameObject enemyObj = ObjectPooling.Instance.Spawn(enemy.prefab, radomTransform.position, radomTransform.rotation);
           
            //GameObject enemyObj = Instantiate(enemy.prefab, radomTransform.position, radomTransform.rotation);
            DamageReceiver receiver = enemyObj.GetComponent<DamageReceiver>();  
            receiver.SetInfo(enemy.HP,enemy.dropMoney);   

            spawnedEnemy++;
            spawnedList.Add(enemyObj);
            yield return new WaitForSeconds(spawnDelay);
        }
      

    }

    private Transform GetRandomTranform(EnemyType enemyType)
    {
        if (spawnTransformWalk.Length == 0)
        {
            Debug.Log("spawn point is missing");
            return null;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnTransformWalk.Length - 1);
        }
        while (randomIndex == prevPosition);
        prevPosition = randomIndex; 


        if (enemyType == EnemyType.Fly)
        {
            return spawnTransformFly[randomIndex];
        }

        return spawnTransformWalk[randomIndex];
    }

    public void DespawnAll()
    {
        foreach(GameObject obj in spawnedList)
        {
            ObjectPooling.Instance.Despawn(obj);
        }
        spawnedList.Clear();    
    }
    public bool IsEmptySpawnedEnemyList()
    {
        return spawnedList.Count == 0;
    }
  
    public void RemoveSpawned(GameObject obj)
    {
        spawnedList.Remove(obj);       

        if (IsEmptySpawnedEnemyList())
        {
            BattleResultManager.Instance.OnBattleWin();
        }
    }
  
}
