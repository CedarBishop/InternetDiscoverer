using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CursorType { Normal, Blue, Orange}
public class SettingsMenu : MonoBehaviour
{
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;
    public Text MusicVolText;
    public Text MusicVolText2;
    public Text SFXVolText;
    public Text SFXVolText2;
    private float MusicVol = 50;
    private float SFXVol = 50;

    private bool IsPostProcessingOn = true;

    public CursorType CurrentCursor = CursorType.Normal;

    public void Start()
    {
        InititalSetup();
        Close();
    }

    public void TogglePP(bool _Toggle)
    {
        PostProcessingManager.Inst.TogglePostPrcessing(_Toggle);
        IsPostProcessingOn = _Toggle;
    }

    public void UpdateSFXSlider(float _Adjustment)
    {
        SFXVol = _Adjustment;
        //SFXVol = Mathf.RoundToInt(SFXVol);
        SFXVolText.text = SFXVol.ToString("0");
        SFXVolText2.text = SFXVol.ToString("0");
        GlobalSoundManager.Inst.MaxSFXVolume = SFXVol/100f;
    }

    public void UpdateMusicSlider(float _Adjustment)
    {
        MusicVol = _Adjustment;
        MusicVol = Mathf.RoundToInt(MusicVol);
        MusicVolText.text = MusicVol.ToString();
        MusicVolText2.text = MusicVol.ToString();
        GlobalSoundManager.Inst.MaxMusicVolume = MusicVol/100f;
    }

    private void InititalSetup()
    {
        MusicVolText.text = MusicVol.ToString();
        SFXVolText.text = SFXVol.ToString();
        MusicVolSlider.maxValue = 100;
        MusicVolSlider.minValue = 0;
        MusicVolSlider.value = MusicVol;
        SFXVolSlider.maxValue = 100;
        SFXVolSlider.minValue = 0;
        SFXVolSlider.value = SFXVol;
        SubToEvents();
    }

    private void AcvivateEntity(MenuItem _Item)
    {
        if(_Item == MenuItem.SettingsScreen)
            gameObject.SetActive(true);
    }

    public void SubToEvents()
    {
        UIManager.SubToActivationEvent(AcvivateEntity);
    }

    public void Close ()
    {
        gameObject.SetActive(false);
    }
}


