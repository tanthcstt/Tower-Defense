using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnlockTurret : MonoBehaviour
{
    [SerializeField] protected GameObject UnlockButtonPrefab;
    [SerializeField] protected Transform buttonGroupTransfrom;

    protected SOTurret currentTurret;
    protected int turretIndex;
    protected int turretLevel;
    protected int turretUnlockPrice;
    public void ActivateUnlockTurretUI(SOTurret turretData)
    {
        SetTurretInfo(turretData);  

        InitButton();

        for (int i = 0; i < buttonGroupTransfrom.childCount; i++)
        {

            int turretIndex = UnlockTurretManager.Instance.GetTurretDataList().IndexOf(turretData);
            int level = i + 1;
           
            // load info(price - level - image)
            LoadInfo(buttonGroupTransfrom.GetChild(i), level);

            // add listener
            if (level == UnlockTurretManager.Instance.UnlockLevelList[turretIndex] +1)
            {
                AddUnlockListener(buttonGroupTransfrom.GetChild(i));
            }
        }

    }

    protected void SetTurretInfo(SOTurret turretData)
    {
        turretIndex = UnlockTurretManager.Instance.GetTurretDataList().IndexOf(turretData);
        turretLevel = UnlockTurretManager.Instance.UnlockLevelList[turretIndex];
        currentTurret = turretData;
        if (turretLevel >= turretData.maxLevel) return;
        turretUnlockPrice = turretData.unlockMoney[turretLevel];
    }


protected void InitButton() 
{ 

    
        ClearContent();

        for (int i = 0; i < currentTurret.maxLevel; i++)
        {
            Instantiate(UnlockButtonPrefab,buttonGroupTransfrom);
        }
}


    protected void ClearContent()
    {
        for(int i = buttonGroupTransfrom.childCount - 1; i >= 0; i--)
        {
            GameObject deleteObj = buttonGroupTransfrom.GetChild(i).gameObject;

            deleteObj.transform.SetParent(null);
            Destroy(deleteObj);
        }
    }

    protected void LoadInfo(Transform buttonGroup, int level)
    {
        // load image
        Image turretImg = transform.Find("TurretImage").GetComponent<Image>();
        turretImg.sprite = currentTurret.turretSprite;
        // load level
        TextMeshProUGUI levelText = buttonGroup.Find("LevelText").GetComponent<TextMeshProUGUI>();
        levelText.text = "Level "+ level.ToString();

        // load price
        TextMeshProUGUI priceText = buttonGroup.Find("Button/PriceText").GetComponent<TextMeshProUGUI>();
        if (level <= UnlockTurretManager.Instance.UnlockLevelList[turretIndex])
        {
            priceText.text = "Unlocked";
            priceText.fontSize = 14f;

            // button color turn gray when unlocked
            Image unlockImage = buttonGroup.Find("Button").GetComponent<Image>();
            unlockImage.color = Color.gray;

        } else
        {
            priceText.text = currentTurret.unlockMoney[level - 1].ToString();
         
        }
    }

    protected void AddUnlockListener(Transform buttonGroup)
    {
        Button unlockBtn = buttonGroup.GetComponentInChildren<Button>();
        unlockBtn.onClick.AddListener(OnUnlockBtnClick);
    }

    protected void OnUnlockBtnClick()
    {
        // decrease money     
        if (!MoneyManager.Instance.IsEnough(MoneyManager.TradingType.Key,turretUnlockPrice)) return;

        MoneyManager.Instance.SetMoney(MoneyManager.TradingType.Key, -turretUnlockPrice);
        UnlockTurretManager.Instance.Unlock(turretIndex);
        ActivateUnlockTurretUI(currentTurret);
    }

}
