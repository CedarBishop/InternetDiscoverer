using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeTubeWatchPage : MonoBehaviour
{
    public Text videoTitleText;
    public RecommendedVideos recomendedVideos;


    public void LoadVideo(VideoData videoData)
    {
        recomendedVideos?.FillRecomendedVideos(videoData);
        GenerateComments(videoData);
    }



    void GenerateComments(VideoData videoData)
    {

    }
}
