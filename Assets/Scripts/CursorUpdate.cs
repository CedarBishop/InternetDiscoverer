using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector2 mousePos;

    public Transform[] shadows;

    //These numbers are based off Lens distortion intensity 20
    private float YMin = 8f;
    private float YMax = 767f;
    private float XMin = 5f;
    private float XMax = 1016f;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (true)
        {
            shadows[1].position = shadows[0].position;
            shadows[0].position = mousePos;
            Vector2 ClampPos = ClampPosition(Input.mousePosition);
            mousePos = Camera.main.ScreenToWorldPoint(ClampPos);
            transform.position = mousePos;

            //Debug.Log("Main cursor: " + transform.position +
            //    "\nShadow 1: " + shadows[0].position +
            //    "\nShadow 2: " + shadows[1].position);

            yield return new WaitForSeconds(speed);

            /// Weird trail effect
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = mousePos;

            //yield return new WaitForSeconds(speed / 3);
            //shadows[0].position = mousePos;


            //yield return new WaitForSeconds(speed / 3);
            //shadows[1].position = shadows[0].position;

            //yield return new WaitForSeconds(speed / 3);
            //Debug.Log("Main cursor: " + transform.position +
            //    "\nShadow 1: " + shadows[0].position +
            //    "\nShadow 2: " + shadows[1].position);
        }
    }

    private Vector2 ClampPosition(Vector2 _Pos)
    {
        Vector2 ClampVec = _Pos;

        if (_Pos.x > XMax)
            ClampVec.x = XMax;
        if (_Pos.x < XMin)
            ClampVec.x = XMin;
        if (_Pos.y > YMax)
            ClampVec.y = YMax;
        if (_Pos.y < YMin)
            ClampVec.y = YMin;
        return ClampVec;
    }
}
//These numbers are based off Lens distortion intesiy 10
//private float YMin = 6f;
//private float YMax = 762f;
//private float XMin = 2f;
//private float XMax = 10176f;
