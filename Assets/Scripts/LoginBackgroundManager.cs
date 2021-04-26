using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBackgroundManager : MonoBehaviour
{
    public GameObject logInWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableLoginWindow()
    {
        logInWindow.SetActive(true);
    }
}
