using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    public Text ClockTimeText;
    public IconButton StartButton;
    public IconButton ExplorerButton;
    public IconButton SettingsButtton;

    public SettingsMenu settingsMenu;

    void Start()
    {
        SubToEvents();
    }

    void Update()
    {

    }


    public void OpenExplorerButton ()
    {
        UIManager.instance.SetMenuItem(MenuItem.Explorer);
    }

    public void OpenSettingsButton()
    {
        settingsMenu.gameObject.SetActive(true);
    }

    public void OpenMessenger ()
    {
        UIManager.instance.messenger.Open();
    }

    private void OnActivation(MenuItem _Item)
    {
        if (_Item == MenuItem.Desktop)
        {
            // Add any custom logic other than activation when this   
        }
    }
    public void SubToEvents()
    {
        UIManager.SubToActivationEvent(OnActivation);
    }

    public void PlayOpenAssetSFX()
    {
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ButtonOPEN);
    }
}