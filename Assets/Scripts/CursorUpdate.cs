using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    public float speed = 0.05f;
    public Vector2 offset;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (true)
        { 
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            yield return new WaitForSeconds(speed);
        }
    }
}
