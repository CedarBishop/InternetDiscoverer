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
    public Text SFXVolText;
    private float MusicVol = 70;
    private float SFXVol = 90;

    public Toggle PostProcesingToggle;
    private bool IsPostProcessingOn = true;

    public CursorType CurrentCursor = CursorType.Normal;
    public Button CursorBlue;
    public Button CursorOrange;
    public Button CursorNormal;

    public Color32 CursorBGColour = Color.white;
    public Color32 CursorSectionColour = Color.cyan;

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
        SetCursor(CursorType.Normal);
    }

    public void ToggleBlue()
    {
        SetCursor(CursorType.Blue);
    }
    public void ToggleNormal()
    {
        SetCursor(CursorType.Normal);
    }
    public void ToggleOrange()
    {
        SetCursor(CursorType.Orange);
    }

    private void SetCursor(CursorType _Type)
    {
        CurrentCursor = _Type;
        ChangeButtonBackgroundColour(_Type);
        UIManager.InvokeCursorEvent(_Type);
    }

    private void ChangeButtonBackgroundColour(CursorType _Type)
    {
        switch (_Type)
        {
            case CursorType.Normal:
                CursorBlue.image.color = CursorBGColour;
                CursorNormal.image.color = CursorSectionColour;
                CursorOrange.image.color = CursorBGColour;
                break;
            case CursorType.Blue:
                CursorBlue.image.color = CursorSectionColour;
                CursorNormal.image.color = CursorBGColour;
                CursorOrange.image.color = CursorBGColour;
                break;
            case CursorType.Orange:
                CursorOrange.image.color = CursorSectionColour;
                CursorBlue.image.color = CursorBGColour;
                CursorNormal.image.color = CursorBGColour;
                break;
        }
    }
}


