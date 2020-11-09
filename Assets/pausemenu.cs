using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pausemenuUI;


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
        Debug.Log("Loading menu");
    }

    public void QuitGame(){
        Debug.Log("quitting the game");
    }
    
}
