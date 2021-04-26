using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorManager : MonoBehaviour
{
    public Cursors cursorNormal;
    public Cursors cursorBlue;
    public Cursors cursorOrange;

    public Cursors currentCursor;
    public Cursors defaultCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public bool hideClientMouse = true;

    public UnityEvent mouseEventLoading;
    public UnityEvent mouseEventNormal;
    private SpriteRenderer[] mouseShadowSprites;

    // Start is called before the first frame update
    void Start()
    {
        mouseShadowSprites = transform.GetComponentsInChildren<SpriteRenderer>();
        
        if (hideClientMouse)
        {
            Cursor.visible = !hideClientMouse;
        }

        currentCursor = defaultCursor;
        UpdateCursor(currentCursor);
        

        mouseEventLoading.AddListener(MouseLoading);
        mouseEventNormal.AddListener(MouseNormal);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ClickDOWN);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            GlobalSoundManager.Inst?.PlayOneShot(SoundEffectEnum.ClickUP);
        }
    }

    public void UpdateCursor(Cursors c)
    {
        currentCursor = c;

        /// NOTE:: mouseShadowSprites[0].sprite is the MAIN cursor. It's the first child in the gameobject and I ceebs distinguishing it lmao - Max.
        mouseShadowSprites[0].sprite = currentCursor.cursor;
    }

    public void MouseLoading()
    {
        //Debug.Log("Changing Cursor to: LOADING");
        Debug.Log("Changing Cursor to: " + currentCursor.loading.name);
        mouseShadowSprites[0].sprite = currentCursor.loading;
        for (int i = 1; i < mouseShadowSprites.Length; i++)
        {
            mouseShadowSprites[i].sprite = currentCursor.loadingShadows[i - 1];
        }
    }

    public void MouseNormal()
    {
        //Debug.Log("Changing Cursor to: NORMAL");
        mouseShadowSprites[0].sprite = currentCursor.cursor;
        for (int i = 1; i < mouseShadowSprites.Length; i++)
        {
            mouseShadowSprites[i].sprite = currentCursor.normalShadow[i - 1];
        }
    }

    public void changeCursorTex(Texture2D tex)
    {
        Cursor.SetCursor(tex, hotSpot, cursorMode);
    }

    private void SubToCursorEvent()
    {
        UIManager.SubToCursorEvent(ReactToCursorEvent);
    }

    private void ReactToCursorEvent(CursorType _Type)
    {
        switch (_Type)
        {
            case CursorType.Normal:
                UpdateCursor(cursorNormal);
                break;
            case CursorType.Blue:
                UpdateCursor(cursorBlue);
                break;
            case CursorType.Orange:
                UpdateCursor(cursorOrange);
                break;
        }
    }
}
