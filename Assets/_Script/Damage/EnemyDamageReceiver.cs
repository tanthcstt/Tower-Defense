using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        base.OnDead();
        SpawnManager.Instance.RemoveSpawned(gameObject);       
        
    }
}
