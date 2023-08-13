using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyBehaviour : EnemyBehaviour
{

    protected bool IsAttacked;

    protected override Vector3 GetLaunchDirection()
    {
        Vector3 dir = base.GetLaunchDirection();
        dir = new Vector3(dir.x, -dir.y, dir.z); 
        return dir; 
    }
    
    protected override void SetState()
    {
        if (transform.position.z - targetTransform.position.z < stopDistanceWall && !IsAttacked)
        {
            OnEnemyAttack();         
          
        } 
        CurrentState = EnemyState.Move;
         
    }
    protected override void OnEnemyAttack()
    {
        base.OnEnemyAttack();
        StartCoroutine(DespawnAfterAttack());
        IsAttacked = true;
    }
    protected IEnumerator DespawnAfterAttack()
    {
        yield return new WaitForSeconds(5); // wait for enemy fly out of battle range
        SpawnManager.Instance.RemoveSpawned(gameObject);
        ObjectPooling.Instance.Despawn(gameObject);
    }

}
