using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VideoData : ScriptableObject
{
    public Sprite smallThumbnail;
    public Sprite largeThumbnail;
    public string title;
    public string views;
    public string lengthTime;
    public string dateAdded;
    public string youtuber;
    public List<VideoTags> videoTags;
}
