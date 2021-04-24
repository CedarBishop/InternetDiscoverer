using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VideoData : ScriptableObject
{
    public Sprite thumbnail;
    public string title;
    public string views;
    public string lengthTime;
    public string dateAdded;
    public string youtuber;
    public List<VideoData> videoTags;
}
