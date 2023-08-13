using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTurretStraight : ACTurret
{
    // bullets of this turret go straight  
    protected override void LaunchProjectile()
    {
        if (currentProjectile != null)
        {
           
            //launch
            currentProjectile.GetComponent<Rigidbody>().velocity = transform.forward * 50f;
        }

       
        
    }
}
