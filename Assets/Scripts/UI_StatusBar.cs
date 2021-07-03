using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusBar : MonoBehaviour
{
    private Image statusImage;

    private float currentPercentage;
    private float maximumPercentage;

    public float statusBarChangeSpeed;

    private void Start() {
        statusImage = GetComponent<Image>();
        maximumPercentage = 1;
        currentPercentage = 1;
    }

    public void ChangeValue(float percentage) {
        maximumPercentage = percentage;
    }

    private void Update() {
        CalculateCurrentPercentage();
        SetImageValue();
    }

    private void CalculateCurrentPercentage() {
        currentPercentage += (maximumPercentage - currentPercentage) * Time.deltaTime * statusBarChangeSpeed;
        if (Mathf.Abs(currentPercentage - maximumPercentage) < .01f)
            currentPercentage = maximumPercentage;
    }

    private void SetImageValue() {
        statusImage.fillAmount = currentPercentage;
    }
}
