using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{

    public SOTurret general;

    public int CurrentTurretLevel { get; private set; }
    private void OnEnable()
    {
        SetTurretLevel(1);
    }

    public void SetTurretLevel(int level)
    {
        this.CurrentTurretLevel = level;    
    }

}
