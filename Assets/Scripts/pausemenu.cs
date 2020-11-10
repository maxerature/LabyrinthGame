using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pausemenu : MonoBehaviour
{
    // Start is called before the first frame update
    private bool GameIsPaused = false;
    public GameObject pausemenuUI;

    //Start is called once at the start of the game
    void Start()
    {
        pausemenuUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

            if(GameIsPaused){
                
                Resume();

            }
            else{
                
                Pause();

            }
        }
    }

    public void Resume(){
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){

        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame(){
        Application.Quit();
    }
    
}
