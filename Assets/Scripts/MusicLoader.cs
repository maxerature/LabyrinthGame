using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    public MusicManager.Tracks track;
    public float delay;
    private void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(delay);
        MusicManager.instance.PlayTrack(track);
        Destroy(this.gameObject);
    }
}
