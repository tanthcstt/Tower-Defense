using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTurretManager : MonoBehaviour
{
    public static UnlockTurretManager Instance { get; private set; }
    [SerializeField] protected List<SOTurret> turretList = new();

   
    public int[] UnlockLevelList { get; private set; }    

    public List<SOTurret> GetTurretDataList()
    {
        return turretList;  
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

        UnlockLevelList = new int[] { 1,1,1,1};
    }
 
    public void LoadList(int[] levelList)
    {
        if (levelList == null) return;
        UnlockLevelList = levelList;
    }

    public void Unlock(int turretIndex)
    {
        UnlockLevelList[turretIndex]++;


        // save 
        if (Application.platform == RuntimePlatform.Android)
        {
            GPGSSaveAndLoad.Instance.OpenSavedGame(true); // save data on value change
        }
    }

    public bool IsUnlocked(SOTurret turret, int level)
    {
        int turretIndex = turretList.IndexOf(turret);
        return UnlockLevelList[turretIndex] >= level;
    }


  

}
