using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTurretNotLaunching : ACTurret
{
    // send dame by raycast not launch projectile
    protected override void LaunchProjectile() {  }



    // sendDamage by runtime
    protected override void OnFire()
    {
        DamageSender sender = GetComponent<DamageSender>();
        sender.SetDamage(turretData.general.damage[0]);

        DamageReceiver receiver = turretTarget.TargetingObject.GetComponent<DamageReceiver>();

        StartCoroutine(sender.SendDamageInTime(receiver, fireTime));

    }


}
