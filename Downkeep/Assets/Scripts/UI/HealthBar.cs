using System;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        healthText.text = Math.Max(0, health.curHealth).ToString();
    }
}
