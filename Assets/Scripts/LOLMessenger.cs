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
    private bool isNotFirstRace;

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
                if (Random.Range(0, 3) < 1)
                {
                    UpdatePopup(currentVideo);
                }                
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
        string targetVideo = GameManager.instance.GetTargetVideo().title;
        string username = GameManager.instance.GetUserName();
        string messageContent = "";

        if (isNotFirstRace)
        {
            messageContent = "Hey " + username + " let's race again.";
            CreateMessage(messageContent, false);
            messageContent = "The new target video is " + targetVideo;
            CreateMessage(messageContent, false);
        }
        else
        {
            messageContent = "Hey " + username + " let's do a MeTube race";
            CreateMessage(messageContent, false);
            messageContent = "The person with the least amount of clicks wins";
            CreateMessage(messageContent, false);
            messageContent = "The target video is " + targetVideo;
            CreateMessage(messageContent, false);
        }   


        hasGivenObjective = true;
        isNotFirstRace = true;

    }

    private void UpdatePopup(VideoData currentVideo)
    {
        popup.SetActive(true);
        int clicks = GameManager.instance.GetClicks();
        string username = GameManager.instance.GetUserName();
        string targetVideo = GameManager.instance.GetTargetVideo().title;

        string messageContent = username + " you are already on " + clicks + " clicks.";
        CreateMessage(messageContent, false);
        messageContent = username + "You better find it soon.";
        CreateMessage(messageContent, false);
    }

    private void ObjectiveComplete (VideoData currentVideo)
    {
        popup.SetActive(true);
        int clicks = GameManager.instance.GetClicks();
        string targetVideo = GameManager.instance.GetTargetVideo().title;
        string username = GameManager.instance.GetUserName();
        string messageContent = "";

        if (clicks < 4)
        {
            messageContent += "Damn you found that video in only "+ clicks  +" clicks!\nYou even beat me."; 
        }
        else if (clicks < 7)
        {
            messageContent += "Okay job, not as fast as me but you managed to find " + targetVideo + " in " + clicks + " clicks." ;
        }
        else
        {
            messageContent += "Wow you are so slow, it took you " + clicks + "clicks to find " + targetVideo + ".";
        }

        CreateMessage(messageContent, false);

        GameManager.instance.ResetTargetVideo();
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
