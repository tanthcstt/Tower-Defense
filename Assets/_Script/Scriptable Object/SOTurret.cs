using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret/Create Turret")]


public class SOTurret : ScriptableObject
{

    // max level = 4
    public string turretName;
    public Sprite turretSprite;
    public int maxLevel;
    public float[] damage;
    public float reloadTime;
    public GameObject bulletPrefabs;
    public float maxRange;
    public int price;
    public int[] unlockMoney;
    public int[] upgradePrice;
   
}
