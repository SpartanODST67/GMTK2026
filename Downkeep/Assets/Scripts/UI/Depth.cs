using System;
using TMPro;
using UnityEngine;

public class Depth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DepthTracker depth;

    void Update()
    {
        text.text = depth.GetDepth().ToString("F2");
    }
}
