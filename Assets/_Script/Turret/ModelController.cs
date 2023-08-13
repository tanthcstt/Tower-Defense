using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public static ModelController Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
      
       
    }
 

    public void SetModelByLevel(GameObject parent, int level)
    {      
        ChangeModel(parent, level); 
        SetLevel(parent, level);    
    }

    private void ChangeModel(GameObject parent, int level)
    {
        foreach (Transform child in parent.transform)
        {

            if (child.GetSiblingIndex() + 1 == level)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void SetLevel(GameObject obj, int level)
    {
        if (obj.TryGetComponent<TurretData>(out TurretData data))
        {
            data.SetTurretLevel(level);
        }
    }



   
}
