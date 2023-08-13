using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurretInfo : MonoBehaviour
{
    [SerializeField] protected Image img;
    [SerializeField] protected TextMeshProUGUI upgradePriceTxt;
    [SerializeField] protected Button upgradeButton;
    [SerializeField] protected Image upgradeBtnImage;

    [SerializeField] protected Slider damageSlider;
    protected float maxDamage = 100f;

    [SerializeField] protected Slider reloadTimeSlider; 
    protected float minReloadTime = 1f; 
    protected float maxReloadTime = 10f;
    protected GameObject currentTurret;
  
    private void Awake()
    {
        damageSlider.maxValue = maxDamage;
        reloadTimeSlider.maxValue = maxReloadTime;
        AddUpgradeListener();
    }  
    protected void AddUpgradeListener()
    {
        upgradeButton.onClick.AddListener(UpGradeTurret);
    }

    public void LoadInfo(GameObject turret)
    {
        if (turret.TryGetComponent<TurretData>(out TurretData data))
        {
            damageSlider.value = data.general.damage[data.CurrentTurretLevel-1];
            reloadTimeSlider.value = data.general.reloadTime; 
            currentTurret = turret;

            int upgradePrice = data.general.upgradePrice[data.CurrentTurretLevel - 1];
            upgradePriceTxt.text = "(" + upgradePrice + ")";

            if (UnlockTurretManager.Instance.IsUnlocked(data.general, data.CurrentTurretLevel + 1))
            {
                upgradeBtnImage.color = Color.green;
            } else
            {
                upgradeBtnImage.color = Color.gray;    
            }

        }
    }

    public void UpGradeTurret()
    {
        if (currentTurret && currentTurret.TryGetComponent<TurretData>(out TurretData data))
        {
            if (data.CurrentTurretLevel < data.general.maxLevel)
            {
                // play can only upgrade when turret unlocked

                if (!UnlockTurretManager.Instance.IsUnlocked(data.general, data.CurrentTurretLevel + 1)) return; 
                
                data.SetTurretLevel(data.CurrentTurretLevel + 1);               
                ModelController.Instance.SetModelByLevel(currentTurret, data.CurrentTurretLevel);
                // reload component
                currentTurret.GetComponent<TurretTarget>().ReloadComponent();
                currentTurret.GetComponent<ACTurret>().ReloadComponent();               

                LoadInfo(currentTurret);
                // decrease money
                int upgradePrice = data.general.upgradePrice[data.CurrentTurretLevel - 1];
                MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Money, -upgradePrice);

            }


        }

    }
   
}
