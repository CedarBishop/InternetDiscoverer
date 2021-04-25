using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeTubeHomePage : MonoBehaviour
{
    public RecommendedVideos recomendedVideos;
    public Scrollbar[] scrollbars;

    public void LoadRecommendedVideos ()
    {
        recomendedVideos?.FillRecomendedVideos(null, true);
        foreach (Scrollbar scrollbar in scrollbars)
        {
            scrollbar.value = 1;
        }
    }
}
