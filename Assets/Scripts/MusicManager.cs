using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Tracks
    {
        None,
        Menu,
        Game,
        Boss,
        GameOver,
        Victory
    }

    public static MusicManager instance;
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip bossMusic;
    public AudioClip gameOverMusic;
    public AudioClip victoryMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayTrack(Tracks track)
    {
        switch(track)
        {
            case Tracks.None:
                audioSource.Stop();
                break;
            case Tracks.Menu:
                if (audioSource.clip == menuMusic)
                    return;
                audioSource.clip = menuMusic;
                audioSource.Play();
                break;
            case Tracks.Game:
                if (audioSource.clip == gameMusic)
                    return;
                audioSource.clip = gameMusic;
                audioSource.Play();
                break;
            case Tracks.Boss:
                if (audioSource.clip == bossMusic)
                    return;
                audioSource.clip = menuMusic;
                audioSource.Play();
                break;
            case Tracks.GameOver:
                if (audioSource.clip == gameOverMusic)
                    return;
                audioSource.clip = gameOverMusic;
                audioSource.Play();
                break;
            case Tracks.Victory:
                if (audioSource.clip == victoryMusic)
                    return;
                audioSource.clip = victoryMusic;
                audioSource.Play();
                break;
        }
    }
}
