using UnityEngine;


public class Volumebar : MonoBehaviour {
    
    private AudioSource audioSrc;
    private float musicVolume = 1f;

	
	void Start (){
        audioSrc = GetComponent<AudioSource>();
	}
	
	void Update (){
        audioSrc.volume = musicVolume;
	}

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}