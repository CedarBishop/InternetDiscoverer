using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectEnum { ClickDOWN, ClickUP, Scroll, ButtonOPEN, ButtonCLOSE, Login }
public enum MusicTrackEnum { Test1, Test2, LoopTest }
public enum TrackFadeType { FadeIn, FadeOut }

public class GlobalSoundManager : MonoBehaviour
{
    public static GlobalSoundManager Inst;

    private AudioSource MusicSourceCurrent;
    private AudioSource MusicSourceNext;

    private List<AudioSource> AvaliableSoundEffectSources;
    private List<AudioSource> InUseSoundEffectSources;
    private int MaxSFXSources = 5;

    private float FadeInDuration = 1f;
    private float CurrentFadeDuration = 0;

    private bool IsTrackEnding = false;
    public bool IsTrackOnLoop = false;

    public float MaxSFXVolume = 1;
    public float MaxMusicVolume = 1;

    [SerializeField]
    public List<SoundEffect> AvaliableSoundEffects = new List<SoundEffect>();
    [SerializeField]
    public List<MusicTrack> AvaliableMusicTracks = new List<MusicTrack>();


    public void Awake()
    {
        CreateSingleton();
        InitialSetup();
    }

    void Update()
    {
        CheckIfTrackEnding();
        FadeTracksIn();
        ReturnSFXSourcesToPool();
    }

    public void PlayOneShot(SoundEffectEnum _Effect)
    {
        AudioSource AS = GetAvaliableSFXSource();
        if (AS == null) //This could add to pending sfx list but CBA
            return;

        AudioClip Clip = GetSFXClip(_Effect);
        AS.PlayOneShot(Clip);
    }

    public void ChangeMusicTrack(MusicTrackEnum _Track)
    {
        if (IsTrackEnding)
            return;

        AudioClip MC = GetMusicClip(_Track);
        if (MC == null)
            return;
        MusicSourceNext.clip = MC;
        MusicSourceNext.Play();
        IsTrackEnding = true;
        IsTrackOnLoop = false;
    }

    #region SFX Functionality

    private AudioClip GetSFXClip(SoundEffectEnum _Effect)
    {
        for (int Index = 0; Index < AvaliableSoundEffects.Count; Index++)
        {
            if (AvaliableSoundEffects[Index].SFXEnum == _Effect)
                return AvaliableSoundEffects[Index].Clip;
        }

        Debug.Log("Sound Clip Does not Exist");
        return null;
    }

    private AudioSource GetAvaliableSFXSource()
    {
        AudioSource AS = null;
        if (AvaliableSoundEffectSources.Count != 0)
        {
            AS = AvaliableSoundEffectSources[0];
            AvaliableSoundEffectSources.RemoveAt(0);
            InUseSoundEffectSources.Add(AS);
            return AS;
        }
        else
        {
            return null; //If there is no audio sources avaliable, don't play...
            //AS = InUseSoundEffectSources[0]; 
            //InUseSoundEffectSources.RemoveAt(0);
            //AS.Stop();
            //AS.clip = null;
            //return AS;
        }
    }

    private void ReturnSFXSourcesToPool()
    {
        if (InUseSoundEffectSources.Count == 0)
            return;

        AudioSource AS = null;
        AudioSource[] ASArray = InUseSoundEffectSources.ToArray();

        for (int RemoveIndex = 0; RemoveIndex < ASArray.Length; RemoveIndex++)
        {
            AS = ASArray[RemoveIndex];
            if (!AS.isPlaying)
            {
                AS.clip = null;
                AvaliableSoundEffectSources.Add(AS);
                InUseSoundEffectSources.Remove(AS);
            }
        }
    }

    #endregion

    #region Music Functionality

    private AudioClip GetMusicClip(MusicTrackEnum _Track)
    {
        for (int Index = 0; Index < AvaliableMusicTracks.Count; Index++)
        {
            if (AvaliableMusicTracks[Index].MTEnum == _Track)
                return AvaliableMusicTracks[Index].Clip;
        }

        Debug.Log("Music Does not Exist");
        return null;
    }

    private void FadeTracksIn()
    {
        if (!IsTrackEnding)
            return;

        CurrentFadeDuration += Time.deltaTime;
        float FadePercent = CurrentFadeDuration / FadeInDuration;

        if (FadePercent > 1)
            FadePercent = 1;

        MusicSourceCurrent.volume = (1 - FadePercent) * MaxMusicVolume;
        MusicSourceNext.volume = (FadePercent) * MaxMusicVolume;

        if (FadePercent == 1)
            SetForNextTrack();
    }

    private void SetForNextTrack()
    {
        AudioSource Temp = null;
        Temp = MusicSourceCurrent;
        MusicSourceCurrent = MusicSourceNext;
        MusicSourceNext = Temp;

        if (IsTrackOnLoop)
            MusicSourceNext.clip = MusicSourceCurrent.clip;
        else
        {
            //This is here for playing a playlist
        }

        MusicSourceCurrent.volume = 1;
        MusicSourceNext.volume = 0;
        CurrentFadeDuration = 0;
        IsTrackEnding = false;
    }

    private void CheckIfTrackEnding()
    {
        if (MusicSourceCurrent.clip == null || IsTrackEnding)
            return;

        if (MusicSourceCurrent.time >= (MusicSourceCurrent.clip.length - FadeInDuration))
        {
            MusicSourceNext.Play();
            IsTrackEnding = true;
        }
    }

    #endregion

    #region Class Utility

    private void InitialSetup()
    {
        AvaliableSoundEffectSources = new List<AudioSource>();
        InUseSoundEffectSources = new List<AudioSource>();

        AudioSource AS;
        for (int Index = 0; Index < MaxSFXSources; Index++)
        {
            AS = gameObject.AddComponent<AudioSource>();
            AvaliableSoundEffectSources.Add(AS);
        }

        MusicSourceCurrent = gameObject.AddComponent<AudioSource>();
        MusicSourceNext = gameObject.AddComponent<AudioSource>();
    }

    private void CreateSingleton()
    {
        if (Inst == null)
            Inst = this;
        else
            Destroy(this);
    }

    #endregion
}

[System.Serializable]
public class MusicTrack
{
    public MusicTrackEnum MTEnum;
    public AudioClip Clip;
}

[System.Serializable]
public class SoundEffect
{
    public SoundEffectEnum SFXEnum;
    public AudioClip Clip;
}