using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOLMessage : MonoBehaviour
{
    public Text messageText;

    public Color yourColor;
    public Color friendColor;

    public void Setup(string messageContent, bool isYou)
    {
        messageText.text = messageContent;
        messageText.alignment = (isYou) ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        messageText.color = (isYou) ? yourColor : friendColor;
    }
}
