using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private float progressSpeed;
    private float currentValue;
    private float fillAmount;

    public float CurrentValue
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            fillAmount = currentValue / MaxValue;
        }
    }
    public float MaxValue { get; set; }

    private void Update()
    {
        TrackingChanging();
    }

    private void TrackingChanging()
    {
        if (fillAmount != GetComponent<Image>().fillAmount)
        {
            GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, fillAmount, Time.deltaTime * progressSpeed);
        }
    }
}
