using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeTubeHomePage : MonoBehaviour
{
    public RecommendedVideos recomendedVideos;

    public void LoadRecommendedVideos ()
    {
        recomendedVideos?.FillRecomendedVideos(null);
    }
}
