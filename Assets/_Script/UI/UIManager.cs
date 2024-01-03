using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] protected PlayerTarget playerTarget;
    [SerializeField] protected GameObject turretInfoUI;
    public UIGameResult resultUI;
    private void Awake()
    {
        Instance = this;
    }
   
    private void Start()
    {
        turretInfoUI.SetActive(false);  
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { ToggleTurretInfo(); }
       
    }
    public void ToggleUI(GameObject UI)
    {
        UI.SetActive(!UI.activeSelf);
    }

    protected void ToggleTurretInfo()
    {
        GameObject targetTurret = playerTarget.GetPlayerTarget("Turret");

        if (targetTurret)
        {
            turretInfoUI.SetActive(true);
            // load turret data
            UITurretInfo turretInfo = turretInfoUI.GetComponent<UITurretInfo>();
            turretInfo.LoadInfo(targetTurret);
            return; 
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                turretInfoUI.SetActive(false);
            }
        }



    }




}
