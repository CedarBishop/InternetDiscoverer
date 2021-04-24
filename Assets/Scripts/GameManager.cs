using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public VideoData[] allVideos;
    public List<BrowserHistoryState> browserHistory;

    private int browserHistoryStateIndex = -1;

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
    }

    public List<VideoData> GenerateRecomendedVideos (VideoData currentVideo, bool isHomePage)
    {
        List<VideoData> recomendedVideos = new List<VideoData>();

        if (isHomePage)
        {
            foreach (var item in allVideos)
            {
                recomendedVideos.Add(item);
            }
        }
        else
        {
            // use tags from recomended video to return a list of similar videos

            foreach (VideoData video in allVideos)
            {
                if (SharesTag(video, currentVideo))
                {
                    recomendedVideos.Add(video);
                }
            }
        }

        recomendedVideos.Shuffle();

        BrowserHistoryState state = new BrowserHistoryState();
        state.isHomePage = isHomePage;
        state.videoDatas = recomendedVideos;

        browserHistory.Add(state);
        browserHistoryStateIndex++;


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
        browserHistory.Clear();
        browserHistoryStateIndex = -1;
    }

    public bool GetPreviousBrowserState (out BrowserHistoryState historyState)
    {
        historyState = new BrowserHistoryState();
        if (browserHistoryStateIndex > 0)
        {
            browserHistoryStateIndex--;
            historyState = browserHistory[browserHistoryStateIndex];
            return true;
        }

        return false;
    }

    public bool GetNextBrowserState (out BrowserHistoryState historyState)
    {
        historyState = new BrowserHistoryState();
        if (browserHistoryStateIndex < browserHistory.Count - 1)
        {
            browserHistoryStateIndex++;
            historyState = browserHistory[browserHistoryStateIndex];
            return true;
        }

        return false;
    }
}

public struct BrowserHistoryState
{
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

