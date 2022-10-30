using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   

    [SerializeField]
    Button startButton;

    /**
     * Quits the game session
     */
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

    public void Play()
    {
        SceneManager.LoadScene("EarthScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
