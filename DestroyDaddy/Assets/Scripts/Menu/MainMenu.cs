using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button loadButton;

    [SerializeField]
    GameObject levelLoader;

    public static PlayerData pd;

    /**
     * Quits the game session
     */

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
            LevelLoader.sceneTransition = true;
            LevelLoader.nextScene = pd.sceneName;
        }
        else {
            LevelLoader.sceneTransition = true;
            LevelLoader.nextScene = "Earth";
        }
        
    }
}
