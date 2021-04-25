using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    private float DoubleClickTime = 0.25f;
    private float Duration = 0;
    private bool Clicked = false;
    public Button Icon;
    public MenuItem ItemToOpen;

    public void Update()
    {
        if (!Clicked)
            return;

        Duration += Time.deltaTime;
        if (Duration >= DoubleClickTime)
        {
            Clicked = false;
            Duration = 0;
        }
    }

    public void ButtonOnClick()
    {
        if (!Clicked)
            Clicked = true;
        else
        {
            Duration = 0;
            Clicked = false;
            Desktop.InvokeItemToggleEvent(ItemToOpen);
        }

    }
}
