using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Inst;

    public PostProcessVolume PPVNormal;
    public PostProcessVolume PPVDEEP;
    public PostProcessVolume PPVDEEPER;
    private bool IsPostProcessing = true;
    public int IntensityVolume = 20;

    private bool DEEPER = false;
    private float DEEPtarget = 0;
    private float DEEPmultiplier = 0.25f; // Higher makes the transition happen faster

    // Start is called before the first frame update
    void Start()
    {
        GameManager.ConsecutiveDeepVideosUpdated += IncrementDEEP;
        InitialSetup();
    }

    private void Update()
    {
        PPVDEEP.weight = Mathf.Lerp(PPVDEEP.weight, DEEPtarget, 5 * Time.deltaTime);

        if (DEEPER)
        {
            PPVDEEPER.weight += 0.075f * Time.deltaTime;
        }
    }

    private void InitialSetup()
    {
        SetSingleton();
        if (PPVNormal == null)
        {
            PPVNormal = gameObject.GetComponentInChildren<PostProcessVolume>();
        }
    }

    private void SetSingleton()
    {
        if (Inst == null)
            Inst = this;
        else
            Destroy(gameObject);
    }

    public void TogglePostPrcessing(bool _Toggle)
    {
        LensDistortion LD;
        PPVNormal.TryGetComponent<LensDistortion>(out LD);
        if (_Toggle)
            LD.intensity.value = IntensityVolume;
        else
            LD.intensity.value = 0;

        IsPostProcessing = _Toggle;
    }

    public void IncrementDEEP(int i)
    {
        DEEPtarget = i * DEEPmultiplier;
        //PPVDEEP.weight += DEEPtarget;
        Debug.Log("DEEP target: " + DEEPtarget);
        if (PPVDEEP.weight >= 1)
        {
            DEEPER = true;
        }
    }
}
