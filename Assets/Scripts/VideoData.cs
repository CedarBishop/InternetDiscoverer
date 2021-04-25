using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VideoTags { Funny, Animals, Drama, Clickbait, Cooking, Music, Movies, Sport, Education, Beauty, Deep, Gaming, News, Celebrity }

[CreateAssetMenu]
public class VideoData : ScriptableObject
{
    public Sprite smallThumbnail;
    public Sprite largeThumbnail;
    public string title;
    public int views;
    public string lengthTime;
    public string dateAdded;
    public string youtuber;
    public float ratings;
    public List<VideoTags> videoTags;
}
