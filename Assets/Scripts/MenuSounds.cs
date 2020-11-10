using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuSelect;
    public AudioClip startGame;

    public void PlaySelect()
    {
        MusicManager.instance.audioSource.PlayOneShot(menuSelect);
    }
    public void PlayStart()
    {
        MusicManager.instance.audioSource.PlayOneShot(startGame);
    }
}
