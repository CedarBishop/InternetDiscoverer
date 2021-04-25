using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    public Text ClockTimeText;
    private Clock PersistentClock;
    public IconButton StartButton;
    public IconButton ExplorerButton;
    public IconButton SettingsButtton;

    void Start()
    {
        PersistentClock = new Clock();
    }

    void Update()
    {
        PersistentClock.UpdateClock(Time.deltaTime);
        ClockTimeText.text = PersistentClock.Hours +":"+ PersistentClock.Minutes;
    }




    private void AcvivateEntity(MenuItem _Item)
    {
        if (_Item == MenuItem.Desktop)
            gameObject.SetActive(true);
        else if(_Item == MenuItem.Explorer)
            gameObject.SetActive(false);       

    }
    public void SubToEvents()
    {
        UIManager.SubToActivationEvent(AcvivateEntity);
    }
}

public class Clock
{
    public int Hours;
    public int Minutes;
    private float Min;
    private float Multiplier = 60f;

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