using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDamageReceiver : DamageReceiver
{
    [SerializeField] protected UIHPBar HPBar;
   

    protected override void OnDead() // game over
    {
        BattleResultManager.Instance.OnBattleLose();
    }

    public override void Receive(float damage)
    {
        base.Receive(damage);

        HPBar.UpdateHPBar();
    }
}
