using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOLMessenger : MonoBehaviour
{
    public GameObject popup;
    public Scrollbar messageBoxScrollBar;
    public VerticalLayoutGroup messageVerticalBox;

    public LOLMessage messagePrefab;
    private bool hasGivenObjective;

    private void Start()
    {
        Close();
        GameManager.TargetVideoReset += () => hasGivenObjective = false;
        InternetDiscoverer.NewPageLoaded += OnNewPageLoaded;
    }

    private void OnDestroy()
    {
        GameManager.TargetVideoReset -= () => hasGivenObjective = false;
        InternetDiscoverer.NewPageLoaded -= OnNewPageLoaded;
    }



    private void OnNewPageLoaded (VideoData currentVideo)
    {
        if (hasGivenObjective)
        {
            if (currentVideo == GameManager.instance.GetTargetVideo())
            {
                ObjectiveComplete(currentVideo);
            }
            else
            {
                UpdatePopup(currentVideo);
            }
        }
        else
        {
            GiveObjective(currentVideo);
        }
    }

    private void GiveObjective(VideoData currentVideo)
    {
        popup.SetActive(true);
        GameManager.instance.GetTargetVideo();
    }

    private void UpdatePopup(VideoData currentVideo)
    {
        popup.SetActive(true);
        int clicks = GameManager.instance.GetClicks();
    }

    private void ObjectiveComplete (VideoData currentVideo)
    {
        popup.SetActive(true);
        int clicks = GameManager.instance.GetClicks();
    }

    public void Close ()
    {
        popup.SetActive(false);
    }

    private void CreateMessage (string messageContent, bool isYou)
    {
        LOLMessage message = Instantiate(messagePrefab, messageVerticalBox.transform);
        message.Setup(messageContent, isYou);
        messageBoxScrollBar.value = 0;
    }
}
