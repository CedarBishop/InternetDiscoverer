using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Desktop desktop;
    public InternetDiscoverer internetDiscoverer;

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
}
