using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cursor", menuName = "ScriptableObjects/Cursor", order = 1)]
public class Cursors : ScriptableObject
{
    public Texture2D cursor;
    public Texture2D hand;
    public Texture2D loading;
}
