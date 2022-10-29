using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    /**
     * Opens the game scene after the play button is clicked
     */
    public void Play() 
    {   // Check File -> Build Settings and you will find that the MainMenu has index of 0 and the scene after it will have index of 1
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  // similar to just doing SceneManager.LoadScene("put here the scene you want it to load next")
    }

    /**
     * Quits the game session
     */
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
