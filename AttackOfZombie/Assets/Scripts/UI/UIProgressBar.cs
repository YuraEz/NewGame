using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UIProgressBar : MonoBehaviour
{

    private Slider slider;
    [SerializeField] private float healthAmount;
    public static UIProgressBar Instance;
    private void Awake()
    {
        Instance = this;
        slider = GetComponent<Slider>();
        healthAmount = CarController.Instance.Healths;
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
