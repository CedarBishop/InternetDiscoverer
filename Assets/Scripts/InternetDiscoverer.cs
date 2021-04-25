using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

    private void Start()
    {
        HomeButton();
    }

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
                watchPage.LoadHistoryState(state);
            }
        }
    }

    public void ForwardButton ()
    {
        if (GameManager.instance.GetNextBrowserState(out BrowserHistoryState state))
        {
            watchPage.LoadHistoryState(state);
            print(state.currentVideo.title);
        }
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
