using System;
using TMPro;
using UnityEngine;

public class JumpBoostCount : MonoBehaviour
{
    [SerializeField] PlayerPuppet vampire;
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        text.text = Math.Max(vampire.maxInAirJumps - vampire.curInAirJumps, 0).ToString();
    }
}
