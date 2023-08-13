using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    [SerializeField] protected DamageReceiver receiver;

    protected Slider HPSlider;
    private void Awake()
    {
        receiver.SetInfo(100);
        HPSlider = GetComponent<Slider>();
        HPSlider.maxValue = receiver.HP;  
        HPSlider.value = receiver.HP;
    }

   


    public void UpdateHPBar()
    {
        HPSlider.value = receiver.HP;
    }

   

}
