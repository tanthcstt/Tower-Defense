using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetStraight : TurretTarget
{
    protected override GameObject Target()
    {
        Ray ray = new Ray(bulletSpawnTransform.position + new Vector3(0f,heightOffsetY,0f), transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, turretData.general.maxRange, LayerMask.GetMask("Enemy")))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
