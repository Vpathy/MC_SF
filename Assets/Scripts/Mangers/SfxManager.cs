using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : GenericSingleton<SfxManager>
{

    public AudioClip BeginState_BG_sfxClip;
    public AudioClip PlayState_BG_sfxClip;
    public AudioClip Click_sfxClip;
    public AudioClip Correct_sfxClip;
    public AudioClip Wrong_sfxClip;


    public AudioSource ClipsfxSource;
    public AudioSource BGSource;


    public void Init()
    {
        EventManager.AddListener(CName.click, ClickSfx);
        EventManager.AddListener(CName.correct, CorrectSfx);
        EventManager.AddListener(CName.wrong, WrongSfx);
        EventManager.AddListener(CName.countdownEnd, GameplayMusic);


    }
    
    public void ClickSfx()
    {
        ClipsfxSource.PlayOneShot(Click_sfxClip);
    }

    public void CorrectSfx()
    {
        ClipsfxSource.PlayOneShot(Correct_sfxClip);
    }
    public void WrongSfx()
    {
        ClipsfxSource.PlayOneShot(Wrong_sfxClip);
    }

    public void GameplayMusic()
    {
        BGSource.clip = PlayState_BG_sfxClip;
        BGSource.Play();
    }

}
