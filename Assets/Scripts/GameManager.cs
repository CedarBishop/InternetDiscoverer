using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static event Action TargetVideoReset;
    public static event Action<int> ConsecutiveDeepVideosUpdated;

    public static event Action CrashRestart;

    public Vector2Int randomVideoChance = new Vector2Int(1,4);

    public VideoData[] allVideos;

    private List<VideoData> allNonDeepVideos = new List<VideoData>();

    private Stack<BrowserHistoryState> previousBrowserHistory = new Stack<BrowserHistoryState>();
    private Stack<BrowserHistoryState> forwardBrowserHistory = new Stack<BrowserHistoryState>();

    private BrowserHistoryState currentState;

    public CursorManager cursorManager = null;

    public int amountOfDeepVideosToCrash;
    public float crashTimeDelay;

    private int amountOfClicks = 0;

    private VideoData targetVideo;

    private string username;

    private int consecutiveDeepVideos;

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

        if (cursorManager == null)
        {
            cursorManager = FindObjectOfType<CursorManager>();
        }

        foreach (VideoData video in allVideos)
        {
            if (!VideoContainsTag(video, VideoTags.Deep))
            {
                allNonDeepVideos.Add(video);
            }
        }
        targetVideo = allVideos[0];
        ResetTargetVideo();
    }

    private void Start()
    {
        GlobalSoundManager.Inst.ChangeMusicTrack(MusicTrackEnum.Background, true);
    }

    public List<VideoData> GenerateRecomendedVideos (VideoData newVideo, bool isHomePage)
    {
        previousBrowserHistory.Push(currentState);

        List<VideoData> recomendedVideos = new List<VideoData>();

        if (isHomePage)
        {
            foreach (var item in allNonDeepVideos)
            {
                if (item.title != targetVideo.title)
                {
                    recomendedVideos.Add(item);
                }                
            }
        }
        else
        {
            // use tags from recomended video to return a list of similar videos

            foreach (VideoData video in allVideos)
            {
                if (video == newVideo)
                {
                    continue;
                }
                if (SharesTag(video, newVideo))
                {
                    recomendedVideos.Add(video);
                }
                else
                {
                    int rand = UnityEngine.Random.Range(0, randomVideoChance.y);
                    if (rand < randomVideoChance.x)
                    {
                        recomendedVideos.Add(video);
                    }
                }
            }
        }

        CheckForDeepVideos(newVideo);

        recomendedVideos.Shuffle();

        currentState = new BrowserHistoryState();
        currentState.currentVideo = newVideo;
        currentState.isHomePage = isHomePage;
        currentState.videoDatas = recomendedVideos;

        return recomendedVideos;
    }

    bool SharesTag (VideoData videoData1, VideoData videoData2)
    {
        foreach (var video1Tag in videoData1.videoTags)
        {
            if (VideoContainsTag(videoData2, video1Tag))
            {
                return true;
            }
        }

        return false;
    }

    bool VideoContainsTag (VideoData videoData, VideoTags videoTag)
    {
        foreach (var tag in videoData.videoTags)
        {
            if (tag == videoTag)
            {
                return true;
            }
        }
        return false;
    }

    public void ClearHistory ()
    {
        previousBrowserHistory.Clear();
        forwardBrowserHistory.Clear();
    }

    public bool GetPreviousBrowserState (out BrowserHistoryState historyState)
    {
        historyState = new BrowserHistoryState();

        if (currentState.isHomePage)
        {
            return false;
        }

        if (previousBrowserHistory.Count > 0)
        {
            forwardBrowserHistory.Push(currentState);
            currentState = previousBrowserHistory.Pop();
            historyState = currentState;
            return true;
        }

        return false;
    }

    public bool GetNextBrowserState (out BrowserHistoryState historyState)
    {
        historyState = new BrowserHistoryState();
        if (forwardBrowserHistory.Count > 0)
        {
            previousBrowserHistory.Push(currentState);
            currentState = forwardBrowserHistory.Pop();
            historyState = currentState;
            return true;
        }

        return false;
    }

    public int GetClicks ()
    {
        return amountOfClicks;
    }

    public VideoData GetTargetVideo ()
    {
        return targetVideo;
    }

    public void AddClicks (int amount)
    {
        amountOfClicks += amount;
    }

    public void ResetTargetVideo ()
    {
        VideoData previousVideo = targetVideo;
        do
        {
            targetVideo = allNonDeepVideos[UnityEngine.Random.Range(0, allNonDeepVideos.Count)];
        } while (targetVideo.title != previousVideo.title);

        ClearClicks();

        if (TargetVideoReset != null)
        {
            TargetVideoReset();
        }
    }

    public void ClearClicks ()
    {
        amountOfClicks = 0;
    }

    public string GetUserName ()
    {
        return username;
    }

    public void SetUserName (string value)
    {
        username = value;
    }

    void CheckForDeepVideos (VideoData newVideo)
    {
        if (newVideo != null)
        {
            if (VideoContainsTag(newVideo, VideoTags.Deep))
            {
                consecutiveDeepVideos++;
                if (consecutiveDeepVideos >= amountOfDeepVideosToCrash)
                {
                    StartCoroutine("CoCrash");
                }
            }
            else
            {
                consecutiveDeepVideos = 0;
            }            
        }
        else
        {
            consecutiveDeepVideos = 0;
        }

        if (ConsecutiveDeepVideosUpdated != null)
        {
            ConsecutiveDeepVideosUpdated(consecutiveDeepVideos);
        }
    }

    IEnumerator CoCrash ()
    {
        GlobalSoundManager.Inst.ChangeMusicTrack(MusicTrackEnum.Crash, false);
        yield return new WaitForSeconds(crashTimeDelay);

        UIManager.instance.BlueScreen.SetActive(true);
        GlobalSoundManager.Inst.SkipMusicToTime(18f);
        PostProcessingManager.Inst.OnCrashRestart();
        yield return new WaitForSeconds(5);
        UIManager.instance.BlueScreen.SetActive(false);

        UIManager.instance.SetMenuItem(MenuItem.Login);
        if (CrashRestart != null)
        {
            CrashRestart();
        }
        consecutiveDeepVideos = 0;
        if (ConsecutiveDeepVideosUpdated != null)
        {
            ConsecutiveDeepVideosUpdated(consecutiveDeepVideos);
        }
        GlobalSoundManager.Inst.ChangeMusicTrack(MusicTrackEnum.Background, true);
    }
}

public struct BrowserHistoryState
{
    public VideoData currentVideo;
    public bool isHomePage;
    public List<VideoData> videoDatas;
}

public static class IListExtensions
{
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

