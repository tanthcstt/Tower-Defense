using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    protected float damage = 0;
    public readonly float delay = 0.1f;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent<DamageReceiver>(out DamageReceiver receiver))
        {
            Send(receiver); 
        }
        ObjectPooling.Instance.Despawn(gameObject);
       
    }

    private void Send(DamageReceiver receiver)
    {
        receiver.Receive(damage);
    }

   
    public IEnumerator SendDamageInTime(DamageReceiver receiver, float timeLength)
    {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < timeLength && receiver)
        {
            
            Send(receiver);
            yield return new WaitForSeconds(delay);
        }
    }
}
