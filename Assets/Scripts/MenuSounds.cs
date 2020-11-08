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
        // audioSource.pitch = Random.Range(0.7f, 1.1f);
        audioSource.PlayOneShot(menuSelect);
        //float after playoneshot to scale volume from 0 to 1

    }
    public void PlayStart()
    {
        audioSource.PlayOneShot(startGame);
    }
}
