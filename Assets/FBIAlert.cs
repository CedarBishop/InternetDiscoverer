using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBIAlert : MonoBehaviour
{
    public GameObject popup;

    private void Start()
    {
        GameManager.ConsecutiveDeepVideosUpdated += CheckIfShouldOpen;
        GameManager.CrashRestart += Close;
        Close();
    }

    void CheckIfShouldOpen (int consecutiveDeepVideos)
    {
        if (consecutiveDeepVideos == GameManager.instance.amountOfDeepVideosToCrash - 1)
        {
            Open();
        }
    }

    public void Open ()
    {
        popup.SetActive(true);
    }

    public void Close ()
    {
        popup.SetActive(false);
    }
}
