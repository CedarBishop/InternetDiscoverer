using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxGroupButton : MonoBehaviour
{
    public CursorType Type;
    private CursorManager cursorManager;

    public Button Butt;
    public Image Img;
    public Sprite Positive;
    public Sprite Negative;
    private bool Checked = false;
    public bool IsDefault = false;


    private void Awake()
    {
        if (cursorManager == null)
        {
            cursorManager = FindObjectOfType<CursorManager>();
        }
    }

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
            Debug.Log("Set Cursor");
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
