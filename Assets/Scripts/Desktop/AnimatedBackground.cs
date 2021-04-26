using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedBackground : MonoBehaviour
{
    public Image Imag;
    private byte NextCount;

    public Sprite[] Sprites;
    public float CycleTime = 1;
    private float CurrentTime = 0;
    private float NextTime;
    private float Interval;

    private void Start()
    {
        SetIntevalSpeed();
    }

    void Update()
    {
        CycleSprites();
    }

    private void CycleSprites()
    {
        if (Sprites == null)
            return;

        if (CurrentTime >= NextTime)
        {
            Imag.sprite = Sprites[NextCount];
            NextCount++;
            if (NextCount >= (Sprites.Length))
                NextCount = 0;
            NextTime += Interval;
        }
        if (CurrentTime >= CycleTime)
        {
            CurrentTime = 0;
            NextTime = Interval;
        }
        else
            CurrentTime += Time.deltaTime;
    }

    private void SetIntevalSpeed()
    {
        Interval = (CycleTime / Sprites.Length);
        NextTime = Interval;
    }
}
