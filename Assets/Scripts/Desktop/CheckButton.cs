using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    public Button Butt;
    public Image Img;
    public Sprite Positive;
    public Sprite Negative;
    public bool Checked = true;


    // Start is called before the first frame update
    void Start()
    {
        if(Checked)
            Img.sprite = Positive;
        else
            Img.sprite = Negative;
    }

    public void CheckButtonPress()
    {
        if(Checked)
        {
            Img.sprite = Negative;
            Checked = false;
        }
        else
        {
            Img.sprite = Positive;
            Checked = true;
        }
    }
}
