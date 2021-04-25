using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MenuItem { Desktop, StartMenu, SettingsScreen, Explorer }
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Desktop desktop;
    public InternetDiscoverer internetDiscoverer;

    public static Action<MenuItem> ActivationEvent;
    public static Action<CursorType> CursorToggleEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Desktop GetDesktop()
    {
        return desktop;
    }

    public InternetDiscoverer GetInternetDiscoverer ()
    {
        return internetDiscoverer;
    }

    public static void SubToActivationEvent(Action<MenuItem> _Function)
    {
        ActivationEvent += _Function;
    }
    public static void InvokeActivationEvent(MenuItem _MenuItem)
    {
        ActivationEvent?.Invoke(_MenuItem);
    }
    public static void SubToCursorEvent(Action<CursorType> _Function)
    {
        CursorToggleEvent += _Function;
    }
    public static void InvokeCursorEvent(CursorType _Cursor)
    {
        CursorToggleEvent?.Invoke(_Cursor);
    }
}
