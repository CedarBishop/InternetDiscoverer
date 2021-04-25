using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Inst;

    public PostProcessVolume PPV;
    private bool IsPostProcessing = true;
    public int IntensityVolume = 20;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        SetSingleton();
        if (PPV == null)
            PPV = gameObject.GetComponent<PostProcessVolume>();
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
        PPV.TryGetComponent<LensDistortion>(out LD);
        if (_Toggle)
            LD.intensity.value = IntensityVolume;
        else
            LD.intensity.value = 0;

        IsPostProcessing = _Toggle;
    }
}
