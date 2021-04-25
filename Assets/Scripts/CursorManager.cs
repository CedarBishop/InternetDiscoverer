using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Cursors cursorNormal;
    public Cursors cursorBlue;
    public Cursors cursorOrange;

    private Cursors currentCursor;
    public Cursors defaultCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public bool hideClientMouse = true;

    // Start is called before the first frame update
    void Start()
    {
        if (hideClientMouse)
            Cursor.visible = !hideClientMouse;
        else
        {
            currentCursor = defaultCursor;
            UpdateCursor(currentCursor);
        }

    }

    public void UpdateCursor(Cursors c)
    {
        currentCursor = c;
        Cursor.SetCursor(currentCursor.cursor, hotSpot, cursorMode);
    }
}
