using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    protected ShopData shopData;

    private void Awake()
    {
        Instance = this;
        shopData = GetComponentInChildren<ShopData>();
    }


    public bool Buy(int turretIndex)
    {
       
        GameObject prefab = shopData.GetTurret(turretIndex);
        int price = prefab.GetComponent<TurretData>().general.price;

        if (MoneyManager.Instance.IsEnough(MoneyManager.TradingType.Money, price))
        {
            BuildTurretManager.Instance.CreateTurret(prefab);
            MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Money ,- price);
            return true;
        }

        return false;
       
       
    }

    public void Sell(GameObject turret)
    {
        turret.SetActive(false);    
        int price = turret.GetComponent<TurretData>().general.price;
        MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Money, + price);

    }


}
