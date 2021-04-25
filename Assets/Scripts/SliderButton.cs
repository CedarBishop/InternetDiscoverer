using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour
{
    public Scrollbar scrollBar;
    public float incrementAmount;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        scrollBar.value += incrementAmount;
    }
}
