using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACTurret : MonoBehaviour
{
    public enum TurretState
    {
        Idle,
        Fire,
        Reload,      
        Remove,       
        Install,
    }
    public TurretState CurrentState { get; private set; }
    protected TurretState prevState = TurretState.Idle; 
    protected GameObject currentProjectile;
    protected Animator animator;
    protected Transform bulletSpawnerTransform;
    protected TurretData turretData;
    protected TurretTarget turretTarget;
    protected bool isEndState = true;


    protected float reloadTime = 2f;
    protected float fireTime=1.5f;
    protected float installTime = 1f;

    public void SetState(TurretState state)
    {
        prevState = CurrentState;
        CurrentState = state;
    }
    private void OnEnable()
    {       
        SetBulletSpawnerTransform();      
        SetState(TurretState.Install);
      
    }
    private void Awake()
    {
        ReloadComponent();
    }

    public void ReloadComponent()
    {
        animator = GetComponentInChildren<Animator>();
        turretData = GetComponent<TurretData>();
        turretTarget = GetComponent<TurretTarget>();
    }

    protected virtual void Update()
    {
        //PerformAction();
        if (isEndState)
        {
            StartCoroutine(AnimationTransition());
        }

        if (CurrentState != prevState)
        {
            AnimationAction();
            prevState = CurrentState;   
        }

        
    }

    protected IEnumerator AnimationTransition()
    {
        switch(CurrentState)
        {
            case TurretState.Install:
                isEndState = false;
                yield return new WaitForSeconds(installTime);
                isEndState = true;
                SetState(TurretState.Reload);
                break;

            case TurretState.Idle:               
                break;

            case TurretState.Reload:
                isEndState = false;
                yield return new WaitForSeconds(reloadTime);
                isEndState = true;
                SetState(TurretState.Idle);
                break;

            case TurretState.Fire:
                isEndState = false;
                yield return new WaitForSeconds(fireTime);
                isEndState = true;
                SetState(TurretState.Reload);
                break;

            case TurretState.Remove:
                break;
        }
    }

    protected void AnimationAction()
    {
        switch (CurrentState)
        {
            case TurretState.Install:
                animator.SetTrigger("Install");
                break;

            case TurretState.Idle:
                break;

            case TurretState.Reload:
                currentProjectile = CreateBullet();
                SetPhysics(false);
                ToggleCollider(false);
                SetProjectileDamage();
                animator.SetTrigger("Reload");
                break;

            case TurretState.Fire:
                OnFire();
                animator.SetTrigger("Fire");
                break;

            case TurretState.Remove:
                animator.SetTrigger("remove");
                break;
        }
    }

    
    public void SetBulletSpawnerTransform()
    {
        bulletSpawnerTransform = FindDeepChildExtention.FindDeepChild(transform, "ShootPoint");
    }
       
   
    protected virtual void OnFire()
    {
        if (currentProjectile != null)
        {
            currentProjectile.transform.SetParent(null);

            SetPhysics(true);
            LaunchProjectile();
            ToggleCollider(true);
            currentProjectile = null;
        }
    }
    protected GameObject CreateBullet()
    {
        if (currentProjectile != null) return null;

        GameObject projectilePrefab = turretData.general.bulletPrefabs;
        if (projectilePrefab == null) return null;


        return ObjectPooling.Instance.Spawn(turretData.general.bulletPrefabs, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation,bulletSpawnerTransform);
    }

    // speed = 1 => animationLength
    // speed = ? => time between 2 shoot
    // 0.35 is shotting animation length
    // time between 2 times shoot = reload time + shoot time (because reload call imediately after shoot)
    protected float GetAnimationSpeed()
    {
        float reloadTime = turretData.general.reloadTime;     
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        if (reloadTime >= animationLength + 0.35f) return 1;
        return (reloadTime + 0.35f) / animationLength;
    }
    protected abstract void LaunchProjectile();

    protected void SetProjectileDamage()
    {
        if (currentProjectile && currentProjectile.TryGetComponent<DamageSender>(out DamageSender damageSender))
        {
            damageSender.SetDamage(turretData.general.damage[turretData.CurrentTurretLevel-1]);      
        }
    }
    protected void SetPhysics(bool state)
    {
        if (currentProjectile != null &&
            currentProjectile.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = !state;
        }
    }

    protected void ToggleCollider(bool state)
    {
        if (currentProjectile == null) return;  
        Collider col = currentProjectile.GetComponent<Collider>();
        col.enabled = state;    
    }

  
}

