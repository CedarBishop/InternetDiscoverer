using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeTubeWatchPage : MonoBehaviour
{
    public RecommendedVideos recomendedVideos;
    public Text videoTitleText;
    public Image ratingsFillImage;
    public Text viewsText;
    public Text youtuberText;
    public Text dateAddedText;
    public VideoViewport videoViewport;


    public void LoadVideo(VideoData videoData)
    {
        recomendedVideos?.FillRecomendedVideos(videoData, false);
        UpdateCurrentVideoUI(videoData);
        GenerateComments(videoData);
    }

    public void LoadHistoryState (BrowserHistoryState historyState)
    {
        recomendedVideos?.LoadHistory(historyState);
        UpdateCurrentVideoUI(historyState.currentVideo);
        GenerateComments(historyState.currentVideo);
    }

    void UpdateCurrentVideoUI (VideoData videoData)
    {
        videoTitleText.text = videoData.title;
        viewsText.text = "views: " + videoData.views;
        ratingsFillImage.fillAmount = videoData.ratings / 5.0f;
        youtuberText.text = "By " + videoData.youtuber;
        dateAddedText.text = "Date Added: " + videoData.dateAdded;

        videoViewport.Setup(videoData);
    }

    void GenerateComments(VideoData videoData)
    {

    }
}
