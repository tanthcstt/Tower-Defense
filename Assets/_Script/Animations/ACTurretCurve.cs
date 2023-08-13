using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ACTurretCurve : ACTurret
{
    protected GameObject target;
    [SerializeField] protected float timeToTarget; // Time it takes to reach the target
    public AnimationCurve curve;
    protected float gravity = Physics.gravity.y;
    protected override void Update()
    {
        base.Update();
        target = turretTarget.TargetingObject;
    }

    protected override void LaunchProjectile()
    {
        if (target == null)
        {
            return;
        }

        // Calculate the required launch velocity to hit the target
        Vector3 targetDirection = target.transform.position - bulletSpawnerTransform.position;
        float targetDistance = targetDirection.magnitude;
       
        float timeToTargetXZ = timeToTarget;

        // Calculate the launch velocity in the XZ plane (horizontal plane)
        Vector3 launchVelocityXZ = targetDirection.normalized * targetDistance / timeToTargetXZ;

        // Calculate the launch velocity in the Y direction (vertical direction)
        float launchVelocityY = (targetDirection.y + 0.5f * Mathf.Abs(gravity) * timeToTargetXZ * timeToTargetXZ) / timeToTargetXZ;

        // Combine the XZ and Y velocities to get the final launch velocity
        Vector3 launchVelocity = new Vector3(launchVelocityXZ.x, launchVelocityY, launchVelocityXZ.z);

        // Adjust the velocity based on the curve
        launchVelocity *= curve.Evaluate(1f);

        currentProjectile.GetComponent<Rigidbody>().velocity = launchVelocity;
    }
}
