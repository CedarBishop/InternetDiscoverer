using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuItem { Desktop, StartMenu, SettingsScreen, Explorer}
public class Desktop : MonoBehaviour
{
    public Text ClockTimeText;
    private Clock PersistentClock;
    public static Action<MenuItem> ItemToggleEvent;

    void Start()
    {
        PersistentClock = new Clock();
    }

    void Update()
    {
        PersistentClock.UpdateClock(Time.deltaTime);
        
        
    }

    public static void SubToItemToggleEvennt(Action<MenuItem> _SubFunction)
    {
        ItemToggleEvent += _SubFunction;
    }
    public static void InvokeItemToggleEvent(MenuItem _Item)
    {
        ItemToggleEvent?.Invoke(_Item);
    }
}







public class Clock
{
    public int Hours;
    public int Minutes;
    private float Min;
    private float Multiplier = 4f;

    public void UpdateClock(float _DeltaTime)
    {
        Min += (_DeltaTime*Multiplier);
        if(Min >= 60)
        {
            Min = 0;
            Hours++;
        }

        Minutes = Mathf.RoundToInt(Min);
    }
}