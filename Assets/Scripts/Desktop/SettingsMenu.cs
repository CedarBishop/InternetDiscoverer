using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;
    public Text MusicVolText;
    public Text SFXVolText;
    private float MusicVol = 70;
    private float SFXVol = 90;

    public Toggle PostProcesingToggle;
    private bool IsPostProcessingOn = true;

    public Toggle Cursor1;
    public Toggle Cursor2;
    public Toggle Cursor3;

    public void Start()
    {
        InititalSetup();
    }

    public void UpdateSFXSlider(float _Adjustment)
    {
        SFXVol = _Adjustment;
        SFXVol = Mathf.RoundToInt(SFXVol);
        SFXVolText.text = SFXVol.ToString();
        GlobalSoundManager.Inst.MaxSFXVolume = SFXVol;
    }

    public void UpdateMusicSlider(float _Adjustment)
    {
        MusicVol = _Adjustment;
        MusicVol = Mathf.RoundToInt(MusicVol);
        MusicVolText.text = MusicVol.ToString();
        GlobalSoundManager.Inst.MaxMusicVolume = MusicVol;
    }

    private void InititalSetup()
    {
        MusicVolText.text = MusicVol.ToString();
        SFXVolText.text = SFXVol.ToString();
        MusicVolSlider.maxValue = 1;
        MusicVolSlider.minValue = 0;
        MusicVolSlider.value = MusicVol;
        SFXVolSlider.maxValue = 100;
        SFXVolSlider.minValue = 0;
        SFXVolSlider.value = SFXVol;
    }
}
