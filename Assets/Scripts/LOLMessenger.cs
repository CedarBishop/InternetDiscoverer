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
    private int rivalScore;

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
        foreach (var message in messageVerticalBox.GetComponentsInChildren<LOLMessage>())
        {
            Destroy(message.gameObject);
        }

        rivalScore = Random.Range(1,8);

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

        SetResponses("Got it.", "You're on!");

        hasGivenObjective = true;
        isNotFirstRace = true;

    }

    private void UpdatePopup(VideoData currentVideo)
    {
        Open();
        int clicks = GameManager.instance.GetClicks();
        string username = GameManager.instance.GetUserName();
        string targetVideo = GameManager.instance.GetTargetVideo().title;

        if (clicks < rivalScore + 1)
        {
            string messageContent = username + " you are doing well";
            CreateMessage(messageContent, false);
            messageContent = "You are on " + clicks + " clicks.";
            CreateMessage(messageContent, false);

            SetResponses("Ok", "Yeah I know.");
        }
        else
        {
            string messageContent = username + " you are already on " + clicks + " clicks.";
            CreateMessage(messageContent, false);
            messageContent = "You better find it soon.";
            CreateMessage(messageContent, false);

            SetResponses("Ok", "Bite me!");
        }
        
    }

    private void ObjectiveComplete (VideoData currentVideo)
    {
        Open();
        int clicks = GameManager.instance.GetClicks();
        string targetVideo = GameManager.instance.GetTargetVideo().title;
        string username = GameManager.instance.GetUserName();
        string messageContent = "";

        if (clicks < rivalScore)
        {
            messageContent = "Damn you found that video in only " + clicks + " clicks!";
            CreateMessage(messageContent, false);
            messageContent = "You beat my score of " + clicks + " clicks!";
            CreateMessage(messageContent, false);
            SetResponses("Good game.", "Eat my dust.");
        }
        else if (clicks == rivalScore)
        {
            messageContent = "We both tied for " + clicks + " clicks.";
            CreateMessage(messageContent, false);
            SetResponses("Good game.", "Ok.");
        }
        else
        {
            messageContent = "Wow you are so slow, it took you " + clicks + " clicks to find " + targetVideo + ".";
            CreateMessage(messageContent, false);
            messageContent = "I found " + targetVideo + " in only " + rivalScore + " clicks.";
            CreateMessage(messageContent, false);
            SetResponses("Well done", "Screw you.");
        }        

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
        messageBoxScrollBar.value = 0f;
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
