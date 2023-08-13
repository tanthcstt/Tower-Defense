using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMoney : MonoBehaviour
{
    protected TextMeshProUGUI moneyTxt;
  
    private void Start()
    {
        MoneyManager.Instance.LoadComponent();
    }

    private void Awake()
    {
        moneyTxt = GetComponentInChildren<TextMeshProUGUI>();
       
    }

    public void UpdateUI(int value)
    {
        moneyTxt.text = value.ToString();  
       
    }
}
