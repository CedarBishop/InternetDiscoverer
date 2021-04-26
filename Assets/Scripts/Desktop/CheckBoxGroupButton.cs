using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxGroupButton : MonoBehaviour
{
    public CursorType Type;

    public Button Butt;
    public Image Img;
    public Sprite Positive;
    public Sprite Negative;
    private bool Checked = false;
    public bool IsDefault = false;


    // Start is called before the first frame update
    void Start()
    {
        if (Checked)
            Img.sprite = Positive;
        else
            Img.sprite = Negative;

        SubToEvents();
        if(IsDefault)
        {
            Img.sprite = Positive;
            Checked = true;
            UIManager.InvokeCursorEvent(Type);
        }
    }

    public void CheckButtonPress()
    {
        if (Checked)
        {

        }
        else
        {
            Img.sprite = Positive;
            Checked = true;
            UIManager.InvokeCursorEvent(Type);
        }
    }

    private void ReactTocursorEvent(CursorType _Type)
    {
        if (_Type == Type)
            return;

        Img.sprite = Negative;
        Checked = false;
    }

    private void SubToEvents()
    {
        UIManager.SubToCursorEvent(ReactTocursorEvent);
    }
}
