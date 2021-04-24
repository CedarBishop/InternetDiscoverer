using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

    public void HomeButton ()
    {
        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
    }

    public void WatchVideo (VideoData videoData)
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(true);

        watchPage.LoadVideo(videoData);
    }
}
