using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float progressSpeed;
    private float currentHealth;
    private float fillAmount;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            fillAmount = CurrentHealth / MaxHealth;
        }
    }
    public float MaxHealth { get; set; }

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
