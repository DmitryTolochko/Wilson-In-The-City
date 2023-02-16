using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
    Game,
    Menu
}

public enum UISoundType
{
    ButtonClick,
    Buy,
    MakeChoice,
    GameOver,
    Money
}

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;

    private AudioSource musicPlayer;
    private AudioSource stepsPlayer;
    private AudioSource UIPlayer;
    private AudioSource ambiancePlayer;

    public List<AudioClip> MusicList;
    public List<AudioClip> StepsList;
    public List<AudioClip> UIList;

    private void Awake() 
    {
        musicPlayer = GetComponent<AudioSource>();
        UIPlayer = transform.Find("UISoundPlayer").GetComponent<AudioSource>();
        stepsPlayer = transform.Find("StepsSoundPlayer").GetComponent<AudioSource>();
        ambiancePlayer = transform.Find("AmbiancePlayer").GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayMusic(MusicType type)
    {
        var music = type == MusicType.Game ? MusicList[0] : MusicList[1];
        if (musicPlayer.clip == music && musicPlayer.isPlaying)
            return;
        
        musicPlayer.Stop();

        musicPlayer.clip = music;
        musicPlayer.Play();
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    public void PauseMusic()
    {
        musicPlayer.Pause();
    }

    public void UnPauseMusic()
    {
        musicPlayer.UnPause();
    }

    public void PlayUISound(UISoundType type)
    {
        switch (type)
        {
            case UISoundType.ButtonClick:
                UIPlayer.clip = UIList[0];
                break;
            case UISoundType.Buy:
                UIPlayer.clip = UIList[1];
                break;
            case UISoundType.MakeChoice:
                UIPlayer.clip = UIList[2];
                break;
            case UISoundType.GameOver:
                UIPlayer.clip = UIList[3];
                break;
            case UISoundType.Money:
                UIPlayer.clip = UIList[4];
                break;
        }
        
        UIPlayer.Play();
    }

    public void PlayStepSound()
    {
        stepsPlayer.clip = StepsList[Random.Range(0, StepsList.Count)];
        stepsPlayer.Play();
    }

    public void PlayAmbiance()
    {
        ambiancePlayer.Play();
    }

    public void StopAmbiance()
    {
        ambiancePlayer.Stop();
    }
}
