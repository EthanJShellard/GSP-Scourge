using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Using slider for this implementation
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(int max) 
    {
        slider.maxValue = max;
    }

    public void SetValue(int value) 
    {
        slider.value = value;
    }
}
