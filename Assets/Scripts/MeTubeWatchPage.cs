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


    public void LoadVideo(VideoData videoData)
    {
        recomendedVideos?.FillRecomendedVideos(videoData, false);
        videoTitleText.text = videoData.title;
        viewsText.text = "views: " + videoData.views;
        ratingsFillImage.fillAmount = videoData.ratings / 5.0f;

        GenerateComments(videoData);
    }

    public void LoadHistoryState (BrowserHistoryState historyState)
    {
        recomendedVideos?.LoadHistory(historyState);

        videoTitleText.text = historyState.currentVideo.title;
        viewsText.text = "views: " + historyState.currentVideo.views;
        ratingsFillImage.fillAmount = historyState.currentVideo.ratings / 5.0f;

        GenerateComments(historyState.currentVideo);
    }

    void GenerateComments(VideoData videoData)
    {

    }
}
