using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] protected ShopData shopData;


    private void Start()
    {
        LoadShopIcon();        
    }

    protected void LoadShopIcon()
    {
        foreach(Transform button in transform)
        {
            if (button.GetChild(0).TryGetComponent<Image>(out Image img))
            {
                GameObject turretPrefab = shopData.GetTurret(button.GetSiblingIndex());
                img.sprite = shopData.GetTurretSprite(turretPrefab);
            }
            
        }
    }

  
   
}
