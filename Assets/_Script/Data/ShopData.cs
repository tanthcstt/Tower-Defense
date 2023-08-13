using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData :MonoBehaviour
{
    [SerializeField] protected List<GameObject> turretPrefabs = new List<GameObject>(); 

    public GameObject GetTurret(int indexInList)
    {
        return turretPrefabs[indexInList];
    }

    public Sprite GetTurretSprite(GameObject turret)
    {
        if (turret.TryGetComponent<TurretData>(out TurretData turretData))
        {
            return turretData.general.turretSprite;
        }
        return null;
    }

}
