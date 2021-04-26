using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Vector2Int randomVideoChance = new Vector2Int(1,4);

    public VideoData[] allVideos;

    private Stack<BrowserHistoryState> previousBrowserHistory = new Stack<BrowserHistoryState>();
    private Stack<BrowserHistoryState> forwardBrowserHistory = new Stack<BrowserHistoryState>();

    private BrowserHistoryState currentState;

    public CursorManager cursorManager = null;

    private int amountOfClicks = 0;

    private VideoData targetVideo;

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
    }

    public List<VideoData> GenerateRecomendedVideos (VideoData newVideo, bool isHomePage)
    {
        previousBrowserHistory.Push(currentState);

        List<VideoData> recomendedVideos = new List<VideoData>();

        if (isHomePage)
        {
            foreach (var item in allVideos)
            {
                if (!item.videoTags.Contains(VideoTags.Deep) || item.title != targetVideo.title)
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
                    int rand = Random.Range(0, randomVideoChance.y);
                    if (rand < randomVideoChance.x)
                    {
                        recomendedVideos.Add(video);
                    }
                }
            }
        }

        recomendedVideos.Shuffle();

        currentState = new BrowserHistoryState();
        currentState.currentVideo = newVideo;
        currentState.isHomePage = isHomePage;
        currentState.videoDatas = recomendedVideos;

        return recomendedVideos;
    }

    bool SharesTag (VideoData videoData1, VideoData videoData2)
    {
        // TODO: optimise this function
        foreach (var video1Tag in videoData1.videoTags)
        {
            if (videoData2.videoTags.Contains(video1Tag))
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

    public int GetClicksAndTargetVideo (out VideoData target)
    {
        target = targetVideo;
        return amountOfClicks;
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
            targetVideo = allVideos[Random.Range(0, allVideos.Length)];
        } while (targetVideo.videoTags.Contains(VideoTags.Deep) && targetVideo.title != previousVideo.title);

        UIManager.instance.internetDiscoverer.HomeButton();
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

