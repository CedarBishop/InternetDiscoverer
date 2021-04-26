using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

   // public GameObject ButtonParent;
   // public GameObject BrowserOutline;

    public Image loadImage;
    public Image CloseImage;

    public float maxInitialWaitTime;
    public float maxIncrementWaitTime;
    public float maxIncrementAmount;

    private void Start()
    {
        SubToEvents();
    }


    public void HomeButton ()
    {
        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        StartCoroutine("CoLoadPage");

        GameManager.instance?.ClearHistory();

        homePage?.LoadRecommendedVideos();

        //GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
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
        //GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void ForwardButton ()
    {
        if (GameManager.instance.GetNextBrowserState(out BrowserHistoryState state))
        {
            watchPage.LoadHistoryState(state);
            StartCoroutine("CoLoadPage");
        }
        //GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void RefreshButton ()
    {
        StartCoroutine("CoLoadPage");
        //GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void WatchVideo (VideoData videoData)
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(true);
        StartCoroutine("CoLoadPage");
        watchPage.LoadVideo(videoData);
        //GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    IEnumerator CoLoadPage ()
    {
        // Set Cursor to loading
        GameManager.instance.cursorManager.mouseEventLoading.Invoke();

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
        
        // Set Cursor to normal
        GameManager.instance.cursorManager.mouseEventNormal.Invoke();
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
        StartCoroutine("CoLoadPage");

        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        GameManager.instance?.ClearHistory();
        homePage?.LoadRecommendedVideos();
    }

    public void CloseExplorer()
    {
        StartCoroutine("CoClosePage");
    }

    IEnumerator CoClosePage()
    {
        GameManager.instance.cursorManager.mouseEventLoading.Invoke();

        CloseImage.fillAmount = 1.0f;
        float initialWaitTime = Random.Range(0, 0.1f);
        yield return new WaitForSeconds(initialWaitTime);

        while (CloseImage.fillAmount > 0)
        {
            float incrementAmount = Random.Range(0, maxIncrementAmount);
            CloseImage.fillAmount -= maxIncrementAmount;
            float waitTime = Random.Range(0, 0.1f);
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
