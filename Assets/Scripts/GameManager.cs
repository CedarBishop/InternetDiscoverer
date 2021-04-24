using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public VideoData[] allVideos;

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

    public List<VideoData> GenerateRecomendedVideos (VideoData currentVideo)
    {
        List<VideoData> recomendedVideos = new List<VideoData>();

        // use tags from recomended video to return a list of similar videos

        foreach (VideoData video in allVideos)
        {
            if (SharesTag(video, currentVideo))
            {
                recomendedVideos.Add(video);
            }
        }

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
}
