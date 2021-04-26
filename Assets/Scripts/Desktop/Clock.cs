using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock: MonoBehaviour
{
    private int Hours = 1;
    private int Minutes = 00;
    private float Min;
    private float Multiplier = 10f;

    public Text Text1;
    public Text Text2;
    public Text Text3;
    private string ClockText;

    public void Update()
    {
        UpdateClock(Time.deltaTime);
        UpdateText();
    }

    private void UpdateClock(float _DeltaTime)
    {
        Min += (_DeltaTime * Multiplier);
        if (Min >= 60)
        {
            Min = 0;
            Hours++;
        }

        Minutes = Mathf.RoundToInt(Min);
    }

    private void UpdateText()
    {
        if(Hours > 999)
        {
            ClockText = "Here, Why?";
            Text1.text = ClockText;
            Text2.text = ClockText;
            Text3.text = ClockText;
            return;
        }

        string HoursText = SetHoursText();

        string MinutesText;
        if (Minutes <= 9)
            MinutesText = "0" + Minutes.ToString();
        else
            MinutesText = Minutes.ToString();

        string TimeOfDay = "PM";

        ClockText = HoursText + ":" + MinutesText + TimeOfDay;
        Text1.text = ClockText;
        Text2.text = ClockText;
        Text3.text = ClockText;
    }

    private string SetHoursText()
    {
        string HoursText;

        if (Hours <= 9)
        {
            HoursText = "  " + Hours.ToString();
            return HoursText;
        }
        else if (Hours <= 99)
        {
            HoursText = " " + Hours.ToString();
            return HoursText;
        }
        else
        {
            HoursText = Hours.ToString();
            return HoursText;
        }
    }
}
