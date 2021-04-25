using MiscUtil.Collections.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoViewport : MonoBehaviour
{
    public Image videoProgressBar;
    public Text timeText;
    public Image playPauseButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    public bool isPlaying;

    private float timeIntoVideo;
    private float videoLengthTime;

    private void Start()
    {
        playPauseButton.sprite = (isPlaying) ? pauseSprite : playSprite;
    }

    public void Setup (VideoData videoData)
    {
        videoLengthTime = videoData.lengthTimeInSeconds;
        ResetVideo();
        isPlaying = true;
    }

    public void TogglePlayPause ()
    {
        isPlaying = !isPlaying;

        playPauseButton.sprite = (isPlaying) ? pauseSprite : playSprite;
    }

    private void Update()
    {
        if (isPlaying)
        {
            timeIntoVideo += Time.deltaTime;
            videoProgressBar.fillAmount = timeIntoVideo / videoLengthTime;
            timeText.text = Helpers.SecondsToMinutesText(timeIntoVideo) + "/" + Helpers.SecondsToMinutesText(videoLengthTime);

            if (timeIntoVideo > videoLengthTime)
            {
                ResetVideo();
            }
        }
    }

    private void ResetVideo ()
    {
        timeIntoVideo = 0.0f;
    }
}

public static class Helpers
{
    public static string SecondsToMinutesText(float seconds)
    {        
        int minutes = (int)seconds / 60;
        seconds -= (minutes * 60);
        int secondsInt = (int)seconds;
        string timeText = minutes + ":" + ((secondsInt < 10)? "0" : "") + secondsInt;
        return timeText;
    }
}

