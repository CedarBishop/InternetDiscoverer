using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

    public Image loadImage;

    public float maxInitialWaitTime;
    public float maxIncrementWaitTime;
    public float maxIncrementAmount;

    private void Start()
    {
        HomeButton();
    }

    public void HomeButton ()
    {
        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        StartCoroutine("CoLoadPage");

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
                StartCoroutine("CoLoadPage");
            }
        }
    }

    public void ForwardButton ()
    {
        if (GameManager.instance.GetNextBrowserState(out BrowserHistoryState state))
        {
            watchPage.LoadHistoryState(state);
            StartCoroutine("CoLoadPage");
        }
    }

    public void RefreshButton ()
    {
        StartCoroutine("CoLoadPage");
    }

    public void WatchVideo (VideoData videoData)
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(true);
        StartCoroutine("CoLoadPage");
        watchPage.LoadVideo(videoData);
    }

    IEnumerator CoLoadPage ()
    {
        loadImage.fillAmount = 1.0f;
        float initialWaitTime = Random.Range(0, maxInitialWaitTime);
        yield return new WaitForSeconds(initialWaitTime);

        while (loadImage.fillAmount > 0)
        {
            float incrementAmount = Random.Range(0, maxIncrementAmount);
            loadImage.fillAmount -= maxIncrementAmount;
            float waitTime = Random.Range(0, maxInitialWaitTime);
            yield return new WaitForSeconds(waitTime);
        }

        loadImage.fillAmount = 0.0f;
    }
}
