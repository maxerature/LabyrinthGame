using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip youWinMusic;
    void Start()
    {
        audioSource.PlayOneShot(youWinMusic);
    }
}
