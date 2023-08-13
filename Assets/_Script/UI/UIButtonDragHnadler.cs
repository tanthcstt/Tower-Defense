using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonDragHnadler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    protected bool isBuySuccess = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (!isBuySuccess) return;
        // turret folow pointer
        BuildTurretManager.Instance.SetPosByRuntime(eventData.position);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // buy
        isBuySuccess = ShopManager.Instance.Buy(transform.GetSiblingIndex());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isBuySuccess)
        {
            return;
        }

        // end build
        if (!BuildTurretManager.Instance.IsValidPos)
        {
            BuildTurretManager.Instance.BuildFaild();   
        }
        isBuySuccess = false;
    }

   
}
