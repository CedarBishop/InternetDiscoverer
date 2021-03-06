using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public static event Action<VideoData> NewPageLoaded;

    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

    public Image loadImage;
    public Image CloseImage;

    public InputField webURL;
    public InputField meTubeBigSearch;
    public InputField meTubeSmallSearch;

    public float maxInitialWaitTime;
    public float maxIncrementWaitTime;
    public float maxIncrementAmount;

    private VideoData currentPageVideoData;

    private void Start()
    {
        SubToEvents();
        LoadHomePage(true);

        GameManager.TargetVideoReset += OnTargetVideoReset;
        GameManager.CrashRestart += OnCrashRestart;
    }

    void OnTargetVideoReset ()
    {
        LoadHomePage();
    }

    void OnCrashRestart ()
    {
        LoadHomePage(true);
    }

    public void HomeButton ()
    {
        LoadHomePage();
    }

    void LoadHomePage (bool withoutLoadingScreen = false)
    {
        // Reset URL and other search bars to their defaults
        if (meTubeSmallSearch.IsActive())
        {
            meTubeSmallSearch.text = "";
        }
        homePage.gameObject.SetActive(true);
        webURL.text = "https://www.metube.com";
        meTubeBigSearch.text = "";


        watchPage.gameObject.SetActive(false);

        GameManager.instance?.ClearHistory();
        currentPageVideoData = null;

        homePage?.LoadRecommendedVideos();

        GameManager.instance.ClearClicks();

        if (!withoutLoadingScreen)
        {
            StartCoroutine("CoLoadPage");
        }
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
                GameManager.instance.AddClicks(2);
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
        currentPageVideoData = videoData;
        StartCoroutine("CoLoadPage");
        watchPage.LoadVideo(videoData);
        GameManager.instance.AddClicks(1);
    }

    IEnumerator CoLoadPage ()
    {
        // Set Cursor to loading
        GameManager.instance.cursorManager.mouseEventLoading.Invoke();

        loadImage.fillAmount = 1.0f;
        float initialWaitTime = UnityEngine.Random.Range(0, maxInitialWaitTime);
        yield return new WaitForSeconds(initialWaitTime);

        while (loadImage.fillAmount > 0)
        {
            float incrementAmount = UnityEngine.Random.Range(0, maxIncrementAmount);
            loadImage.fillAmount -= maxIncrementAmount;
            float waitTime = UnityEngine.Random.Range(0, maxInitialWaitTime);
            yield return new WaitForSeconds(waitTime);
        }

        loadImage.fillAmount = 0.0f;
        
        // Set Cursor to normal
        GameManager.instance.cursorManager.mouseEventNormal.Invoke();

        if (NewPageLoaded != null)
        {
            NewPageLoaded(currentPageVideoData);
        }
    }

    public void MinimiseButton ()
    {
        UIManager.instance.SetMenuItem(MenuItem.Desktop);
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ButtonCLOSE);
    }

    public void MaxmiseButton ()
    {
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ButtonOPEN);
    }

    public void CloseButton ()
    {
        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        GameManager.instance?.ClearHistory();
        homePage?.LoadRecommendedVideos();

        UIManager.instance.SetMenuItem(MenuItem.Desktop);
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ButtonCLOSE);
    }

    public void OpenExplorer()
    {
        LoadHomePage();
    }

    public void CloseExplorer()
    {
        StartCoroutine("CoClosePage");
    }

    IEnumerator CoClosePage()
    {
        GameManager.instance.cursorManager.mouseEventLoading.Invoke();

        CloseImage.fillAmount = 1.0f;
        float initialWaitTime = UnityEngine.Random.Range(0, 0.1f);
        yield return new WaitForSeconds(initialWaitTime);

        while (CloseImage.fillAmount > 0)
        {
            float incrementAmount = UnityEngine.Random.Range(0, maxIncrementAmount);
            CloseImage.fillAmount -= maxIncrementAmount;
            float waitTime = UnityEngine.Random.Range(0, 0.1f);
            yield return new WaitForSeconds(waitTime);
        }

        CloseImage.fillAmount = 0.0f;
        GameManager.instance.cursorManager.mouseEventNormal.Invoke();
    }

    private void SubToEvents()
    {
        UIManager.SubToActivationEvent(OnActivation);
    }

    private void OnActivation(MenuItem _Item)
    {
        if (_Item == MenuItem.Explorer)
        {
            // Add any custom logic other than activation when this   
            StartCoroutine("CoLoadPage");
        }
    }
}
