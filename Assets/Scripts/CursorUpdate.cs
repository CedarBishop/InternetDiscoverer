using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector2 mousePos;

    public Transform[] shadows;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (true)
        {
            shadows[1].position = shadows[0].position;
            shadows[0].position = mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
            Debug.Log("Main cursor: " + transform.position +
                "\nShadow 1: " + shadows[0].position +
                "\nShadow 2: " + shadows[1].position);

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
}
