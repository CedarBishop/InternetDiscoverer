using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cursor", menuName = "ScriptableObjects/Cursor", order = 1)]
public class Cursors : ScriptableObject
{
    public Sprite cursor;
    //public Sprite hand;
    public Sprite loading;
    public Sprite[] normalShadow;
    public Sprite[] loadingShadows;
}
