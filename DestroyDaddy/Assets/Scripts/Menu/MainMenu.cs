using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   

    public static PlayerData pd;

    /**
     * Quits the game session
     */

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerExperience.startCount = 0;
        ShipController.startCount = 0;
    }

    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }

    public void Play()
    {
        LevelLoader.sceneTransition = true;
        LevelLoader.nextScene = "Earth";
    }

    public void Load()
    {   
        pd = SaveSystem.Load();
        if (pd != null) {
            pd.assignValues();
            // doest seem to load
            LevelLoader.sceneTransition = true;
            LevelLoader.nextScene = pd.sceneName;
        }
        else {
            LevelLoader.sceneTransition = true;
            // issues with loading level
            LevelLoader.nextScene = "Earth";
        }
        
    }
}
