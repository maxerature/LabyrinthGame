using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadMenu()
    {
        MusicManager.instance.PlayTrack(MusicManager.Tracks.Menu);
        SceneManager.LoadScene(0);
    }
    public void LoadControls()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadStartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
