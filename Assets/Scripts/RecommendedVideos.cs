using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecommendedVideos : MonoBehaviour
{
    public VideoOption[] videoOptions;

    public void FillRecomendedVideos(VideoData videoData)
    {
        List<VideoData> recommendedVideos = GameManager.instance?.GenerateRecomendedVideos(videoData);

        recommendedVideos.Shuffle();
        int index = 0;
        foreach (VideoOption video in videoOptions)
        {
            if (index < recommendedVideos.Count)
            {
                video.gameObject.SetActive(true);
                video.videoData = recommendedVideos[index];
                video.Setup();
                ++index;
            }
            else
            {
                video.gameObject.SetActive(false);
            }
        }
    }
}
