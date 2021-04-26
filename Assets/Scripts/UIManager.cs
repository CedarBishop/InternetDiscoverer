using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public enum MenuItem { Desktop, StartMenu, SettingsScreen, Explorer }
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Desktop desktop;
    public InternetDiscoverer internetDiscoverer;

    public static Action<MenuItem> ActivationEvent;
    public static Action<CursorType> CursorToggleEvent;

    public MenuItem startingMenuItem;
    private MenuItem currentMenuItem;

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

    private void Start()
    {
        SetMenuItem(startingMenuItem);
    }

    public Desktop GetDesktop()
    {
        return desktop;
    }

    public InternetDiscoverer GetInternetDiscoverer ()
    {
        return internetDiscoverer;
    }

    public void SetMenuItem (MenuItem menuItem)
    {
        desktop.gameObject.SetActive(false);
        internetDiscoverer.gameObject.SetActive(false);

        currentMenuItem = menuItem;

        switch (menuItem)
        {
            case MenuItem.Desktop:
                desktop.gameObject.SetActive(true);
                break;
            case MenuItem.StartMenu:
                break;
            case MenuItem.SettingsScreen:
                break;
            case MenuItem.Explorer:
                internetDiscoverer.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        InvokeActivationEvent(menuItem);
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
