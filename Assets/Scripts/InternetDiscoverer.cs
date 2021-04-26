using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetDiscoverer : MonoBehaviour
{
    public MeTubeHomePage homePage;
    public MeTubeWatchPage watchPage;

    public GameObject ButtonParent;
    public GameObject BrowserOutline;

    public Image loadImage;
    public Image CloseImage;

    public float maxInitialWaitTime;
    public float maxIncrementWaitTime;
    public float maxIncrementAmount;

    private GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void Start()
    {
        SubToEvents();
    }

    public void Update()
    {

    }

    public void HomeButton ()
    {
        homePage.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        StartCoroutine("CoLoadPage");

        GameManager.instance?.ClearHistory();

        homePage?.LoadRecommendedVideos();

        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
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
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void ForwardButton ()
    {
        if (GameManager.instance.GetNextBrowserState(out BrowserHistoryState state))
        {
            watchPage.LoadHistoryState(state);
            StartCoroutine("CoLoadPage");
        }
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void RefreshButton ()
    {
        StartCoroutine("CoLoadPage");
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void WatchVideo (VideoData videoData)
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(true);
        StartCoroutine("CoLoadPage");
        watchPage.LoadVideo(videoData);
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    IEnumerator CoLoadPage ()
    {
        // Set Cursor to loading
        gameManager.cursorManager.mouseEventLoading.Invoke();

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
        gameManager.cursorManager.mouseEventNormal.Invoke();
    }

    public void MinimiseButton ()
    {

        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void MaxmiseButton ()
    {

        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void CloseButton ()
    {
        CloseExplorer();
        GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.Test1);
    }

    public void OpenExplorer()
    {
        gameObject.SetActive(true);
        homePage.gameObject.SetActive(true);
        ButtonParent.gameObject.SetActive(true);
        BrowserOutline.gameObject.SetActive(true);
        watchPage.gameObject.SetActive(false);
        StartCoroutine("CoLoadPage");

        GameManager.instance?.ClearHistory();
        homePage?.LoadRecommendedVideos();
    }

    public void CloseExplorer()
    {
        homePage.gameObject.SetActive(false);
        watchPage.gameObject.SetActive(false);
        ButtonParent.gameObject.SetActive(false);
        BrowserOutline.gameObject.SetActive(false);
        StartCoroutine("CoClosePage");
    }

    IEnumerator CoClosePage()
    {
        gameManager.cursorManager.mouseEventLoading.Invoke();

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
        gameManager.cursorManager.mouseEventNormal.Invoke();
    }

    private void SubToEvents()
    {
        UIManager.SubToActivationEvent(ReactToActivationEvent);
    }

    private void ReactToActivationEvent(MenuItem _MenuItem)
    {
        if (_MenuItem == MenuItem.Explorer)
            OpenExplorer();
    }
}
