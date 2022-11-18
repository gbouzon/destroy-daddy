using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject playerView;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject finalToExit;

    private static bool hasSaved;

    void Start()
    {
        hasSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf || finalToExit.activeSelf)
            {
                Resume();
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                playerView.SetActive(false);
            }
        }
    }

    public void Save() {
        hasSaved = true;
    }

    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playerView.SetActive(true);
        finalToExit.SetActive(false);
        hasSaved = false;
    }

    public void ReturnToMainMenu() {
        if (hasSaved) {
            BypassReturn();
        }
        else {
            pauseMenu.SetActive(false);
            finalToExit.SetActive(true);
        }
    }

    public void BypassReturn() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playerView.SetActive(true);
        finalToExit.SetActive(false);
        hasSaved = false;
        LevelLoader.sceneTransition = true;
        LevelLoader.nextScene = "MainMenu";
    }

    public void SkipReturn() {
        pauseMenu.SetActive(true);
        finalToExit.SetActive(false);
    }
}
