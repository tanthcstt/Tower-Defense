using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretColection : MonoBehaviour
{
    [SerializeField] protected GameObject turretColectionContent;
    [SerializeField] protected UIUnlockTurret unlockTurret;
   
    private void Start()
    {
        ColectionInit();        
    }

    protected void ColectionInit()
    {
        List<SOTurret> turretData = UnlockTurretManager.Instance.GetTurretDataList();
        for (int i = 0; i < turretData.Count; i++)
        {
            Transform turretSlot = turretColectionContent.transform.GetChild(i);

            LoadImage(turretSlot, turretData[i].turretSprite);
            AddColectionButtonListener(turretSlot, turretData[i]);

        }
    }
    protected void LoadImage(Transform turretSlot, Sprite sprite)
    {
        if (turretSlot.GetChild(0).TryGetComponent<Image>(out Image image))
        {
            image.sprite = sprite;  
        }    
    }

    public void AddColectionButtonListener(Transform turretSlot, SOTurret turretData)
    {
        if (turretSlot.GetChild(0).TryGetComponent<Button>(out Button btn))
        {
            btn.onClick.AddListener(delegate { unlockTurret.ActivateUnlockTurretUI(turretData); });
        }
    }
}
