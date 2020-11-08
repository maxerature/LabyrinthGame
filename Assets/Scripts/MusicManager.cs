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
        Game
    }

    public static MusicManager instance;
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

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
                //unsure how long it takes to load, adding artifical delay
                audioSource.Play();
                break;
        }
    }
}
