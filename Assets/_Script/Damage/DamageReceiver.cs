using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
  
    public float HP{ get; private set; }
    protected int reward; // money
    
    public void SetInfo(int HP, int reward = 0)
    {
        this.HP = HP;   
        this.reward = reward;   
    }

    public virtual void Receive(float damage)
    {
        HP -= damage;      
        if (IsDead())
        {
            OnDead();   
        }
    }

    private bool IsDead()
    {
        return HP <= 0;
    }

    protected virtual void OnDead()
    {
        MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Key,+reward);
        gameObject.SetActive(false);
    }
}
