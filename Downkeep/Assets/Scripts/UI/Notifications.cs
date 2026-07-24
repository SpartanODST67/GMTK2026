using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;

    public void WriteNotifications(LinkedList<string> msgs)
    {
        StringBuilder sb = new();

        foreach(string msg in msgs)
        {
            sb.AppendLine($"> {msg}");
            sb.AppendLine("");
        }

        textBox.text = sb.ToString();
    }
}
