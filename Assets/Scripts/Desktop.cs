using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    public Text ClockTimeText;
    public IconButton StartButton;
    public IconButton ExplorerButton;
    public IconButton SettingsButtton;

    void Start()
    {
    }

    void Update()
    {

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