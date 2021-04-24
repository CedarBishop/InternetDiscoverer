using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Cursors cursorNormal;
    public Cursors cursorBlue;
    public Cursors cursorOrange;

    private Cursors currectCursor;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        currectCursor = cursorNormal;
        UpdateCursor(cursorNormal);
    }

    public void UpdateCursor(Cursors c)
    {
        currectCursor = c;
        Cursor.SetCursor(currectCursor.cursor, hotSpot, cursorMode);
    }
}
