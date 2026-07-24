using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChainhookTimer : MonoBehaviour
{
    public static ChainhookTimer Instance;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    float countDownTime = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            countDownTime = (float) Math.Round(countDownTime, 2);
            text.text = countDownTime.ToString();
        }
    }

    public void ChainhookCooldown(float time)
    {
        countDownTime = time;
        text.enabled = true;
        
        var color = image.color;
        color.a = .25f;
        image.color = color;
    }

    public void ChainhookActive()
    {
        countDownTime = 0;
        text.enabled = false;

        var color = image.color;
        color.a = 1f;
        image.color = color;
    }
}
