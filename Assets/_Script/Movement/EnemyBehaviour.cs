using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected float walkingSpeed = 5f;
    [SerializeField] protected GameObject bulletPrefab;
    protected CharacterController characterController;
    protected Transform bulletSpawnTrasform;

    [SerializeField]public float stopDistanceWall = 25f;
    public readonly float stopDistanceEnemy = 3f;
    public readonly float launchAngle = 10f;
    public readonly float shootingDelay = 2f;
    protected Transform targetTransform;
    protected bool isEndState = true;
    public enum EnemyState
    {
        Move,
        Attack,
    }
    public EnemyState CurrentState { get; protected set; }    
    

    private void Awake()
    {
        LoadComponent();
        CurrentState = EnemyState.Move;
    }
    protected void LoadComponent()
    {
        characterController = GetComponent<CharacterController>();
        bulletSpawnTrasform = transform.Find("BulletSpawner");
        targetTransform = GameObject.Find("Enviroment/EnemyTarget").transform;
      
    }

    private void Update()
    {
        SetState();
        if (isEndState)
        {
            StartCoroutine(PerfornState());
        }
    }
    protected virtual void SetState()
    {
        if (Physics.Raycast(bulletSpawnTrasform.position, transform.forward,stopDistanceEnemy)) // enemy behind another enemy
        {
            CurrentState = EnemyState.Attack;
        } else if (transform.position.z - targetTransform.position.z > stopDistanceWall)
        {
            CurrentState=EnemyState.Move;
        } else
        {
            CurrentState = EnemyState.Attack;
        }
       

    }
    protected IEnumerator PerfornState()
    {
        switch (CurrentState)
        {
            case EnemyState.Attack:
                isEndState = false;
                OnEnemyAttack();    
                yield return new WaitForSeconds(shootingDelay);
                isEndState = true;
                break;

            case EnemyState.Move:
                Move();
                break;
        }
    }
    protected void Move()
    {
        characterController.Move(Time.deltaTime * walkingSpeed * transform.forward);
    }
    protected virtual void OnEnemyAttack()
    {

        GameObject bullet = ObjectPooling.Instance.Spawn(bulletPrefab, bulletSpawnTrasform.position, bulletSpawnTrasform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        bullet.GetComponent<DamageSender>().SetDamage(10);


        LaunchBullet(rb);

    }

    protected virtual void LaunchBullet(Rigidbody rb)
    {
       

        rb.velocity = 50f * GetLaunchDirection();
    }

    protected virtual Vector3 GetLaunchDirection()
    {
        // Calculate the desired rotation based on the slope angle
        Quaternion slopeRotation = Quaternion.Euler(12f, 0.0f, 0.0f);

        // Apply the rotation to the vector
        Vector3 rotatedVector = slopeRotation * transform.forward;

        return rotatedVector;
    }
}
