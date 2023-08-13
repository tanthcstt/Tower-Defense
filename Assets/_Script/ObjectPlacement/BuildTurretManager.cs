using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurretManager : MonoBehaviour
{
   public static BuildTurretManager Instance;

    protected GameObject createdObject;
    protected List<GameObject> builtTurret = new List<GameObject>(); 
    public bool IsValidPos { get; private set; }

    private void Awake()
    {
        Instance = this;
        IsValidPos = false;
    }

    public void DespawnAllTurret()
    {
        foreach(GameObject obj in builtTurret)
        {
            ObjectPooling.Instance.Despawn(obj);
        }
        builtTurret.Clear();
    }

    public void SetPosByRuntime(Vector3 pointerPos)
    {
        Vector3 worldPos = GetWorldPos(pointerPos);
        if (pointerPos != Vector3.zero)
        {
            createdObject.transform.position = worldPos;
        } else
        {
            // hide turret
            createdObject.transform.position = new Vector3(0,-10,0);    
        }        
    }

    protected Vector3 GetWorldPos(Vector3 origin)
    {
        Ray ray =Camera.main.ScreenPointToRay(origin);

        if (Physics.Raycast(ray,out RaycastHit hit, 99f,LayerMask.GetMask("Base"))) {

            if (hit.collider.gameObject.CompareTag("Turret Base"))
            {
                IsValidPos = true;
                Vector3 worldPos = hit.collider.transform.position;
                worldPos.y = 0f;
                return worldPos;
            }

            IsValidPos = false;
            return hit.point;   
        }
        IsValidPos = false;
        return Vector3.zero;
    }

    public void BuildFaild()
    {
        builtTurret.Remove(createdObject);
        ShopManager.Instance.Sell(createdObject);
    }

    public GameObject CreateTurret(GameObject prefab)
    {
        //GameObject turret = Instantiate(prefab, new Vector3(0, -10, 0), Quaternion.identity);
        GameObject turret = ObjectPooling.Instance.Spawn(prefab);
        createdObject = turret;
        builtTurret.Add(createdObject);
        return turret;  
    }
}
