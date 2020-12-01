using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gameOverMusic;

    private void Start()
    {
        audioSource.PlayOneShot(gameOverMusic);
    }
}
