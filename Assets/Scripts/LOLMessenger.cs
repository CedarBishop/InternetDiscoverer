using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOLMessenger : MonoBehaviour
{
    public GameObject popup;
    public Scrollbar messageBoxScrollBar;
    public VerticalLayoutGroup messageVerticalBox;
    public Button[] responseButtons;
    public Text responseText1;
    public Text responseText2;


    public LOLMessage messagePrefab;
    private bool hasGivenObjective;
    private bool isNotFirstRace;
    private bool raceFinished;

    private bool canRespond;
    private float bottomOfScrollbarValue;

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
        Open();
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

        SetResponses("Ok", "Bite me!");

        hasGivenObjective = true;
        isNotFirstRace = true;

    }

    private void UpdatePopup(VideoData currentVideo)
    {
        Open();
        int clicks = GameManager.instance.GetClicks();
        string username = GameManager.instance.GetUserName();
        string targetVideo = GameManager.instance.GetTargetVideo().title;

        string messageContent = username + " you are already on " + clicks + " clicks.";
        CreateMessage(messageContent, false);
        messageContent = username + "You better find it soon.";
        CreateMessage(messageContent, false);

        SetResponses("Ok", "Bite me!");
    }

    private void ObjectiveComplete (VideoData currentVideo)
    {
        Open();
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
        SetResponses("Ok", "Bite me!");

        raceFinished = true;        
    }

    public void Close ()
    {
        popup.SetActive(false);
        canRespond = false;

        foreach (var button in responseButtons)
        {
            button.gameObject.SetActive(false);
        }


        if (raceFinished)
        {
            raceFinished = false;
            GameManager.instance.ResetTargetVideo();
        }
    }

    private void CreateMessage (string messageContent, bool isYou)
    {
        LOLMessage message = Instantiate(messagePrefab, messageVerticalBox.transform);
        message.Setup(messageContent, isYou);
        messageBoxScrollBar.value = -0.1f;
    }

    private void SetResponses (string response1, string response2)
    {
        foreach (var button in responseButtons)
        {
            button.gameObject.SetActive(true);
        }
        responseText1.text = response1;
        responseText2.text = response2;
        canRespond = true;
    }

    public void Respond (int buttonResponseNumber)
    {
        if (buttonResponseNumber == 1)
        {
            CreateMessage(responseText1.text, true);
            canRespond = false;
        }
        else if (buttonResponseNumber == 2)
        {
            CreateMessage(responseText2.text, true);
            canRespond = false;
        }

        foreach (var button in responseButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void Open ()
    {
        popup.SetActive(true);
        messageBoxScrollBar.value = 0;
    }
}
