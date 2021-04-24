using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum VideoTags {Funny, Animals, Cute, Drama, Conspiracy, Cooking, Music, Movies, Fails, Sport, Fashion, Science, Beauty, Dark, Gaming, News, Celebrity}

public class VideoOption : MonoBehaviour
{
    public VideoData videoData;

    public Image thumbnail;
    public Text videoTitle;
    public Text viewsText;
    public Text lengthText;
    public Text youtuberText;
    public Text uploadDateText;

    void Start()
    {
        Setup();
    }

    private void OnValidate()
    {
        Setup();
    }

    void Setup ()
    {
        if (videoData == null)
        {
            return;
        }

        thumbnail.sprite = videoData.thumbnail;
        videoTitle.text = videoData.title;
        viewsText.text = videoData.views + " views";
        lengthText.text = videoData.lengthTime;
        uploadDateText.text = "Added: " + videoData.dateAdded;
        youtuberText.text = "by " + videoData.youtuber;
    }

    public void OnClick ()
    {
        print("On Video Clicked");
    }
}
