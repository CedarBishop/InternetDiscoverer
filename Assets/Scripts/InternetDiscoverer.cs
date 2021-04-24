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

        GameManager.instance?.ClearHistory();

        homePage?.LoadRecommendedVideos();
    }

    public void BackButton ()
    {
        if (GameManager.instance.GetPreviousBrowserState(out BrowserHistoryState state))
        {
            if (state.isHomePage)
            {
                HomeButton();
            }
            else
            {

            }
        }
    }

    public void ForwardButton ()
    {

    }

    public void RefreshButton ()
    {

    }

    public void WatchVideo (VideoData videoData)
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(true);

        watchPage.LoadVideo(videoData);
    }
}
