using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretTarget : MonoBehaviour
{
   
    protected TurretData turretData;
    protected Transform bulletSpawnTransform;
    protected ACTurret turretAnimation;
    public GameObject TargetingObject { get;private set; }   
    protected bool isReloading = true;

    [SerializeField] protected float heightOffsetY = 0f;

    private void OnEnable()
    {
        ReloadComponent();  
    }

    public void ReloadComponent()
    {
        SetBulletSpawnerTransform();
        turretData = GetComponent<TurretData>();
        turretAnimation = GetComponent<ACTurret>();
        StartCoroutine(ActivateTurret());
    }
    private void Update()
    {
        TargetingObject = Target();
        StartCoroutine(OnEnemyFound());
      
        Debug.DrawRay(bulletSpawnTransform.position +new Vector3(0f, heightOffsetY, 0f), transform.forward * turretData.general.maxRange, Color.red);
    }

    // fire(Animation) when found enemy
    private IEnumerator OnEnemyFound()
    {
        while(true)
        {
            if (TargetingObject != null && !isReloading && turretAnimation.CurrentState == ACTurret.TurretState.Idle)
            {
                turretAnimation.SetState(ACTurret.TurretState.Fire);
                isReloading = true;
            } else
            {
                break;
            }
            yield return new WaitForSeconds(turretData.general.reloadTime);
            isReloading = false;
        }

      
    }
    protected IEnumerator ActivateTurret()
    {
        yield return new WaitForSeconds(2);
        isReloading = false;  
    }
    public void SetBulletSpawnerTransform()
    {
        bulletSpawnTransform = FindDeepChildExtention.FindDeepChild(transform, "ShootPoint");
    }

    protected abstract GameObject Target();
   

  
}
