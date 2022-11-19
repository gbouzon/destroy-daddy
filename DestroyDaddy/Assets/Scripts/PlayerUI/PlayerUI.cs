using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject playerView;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject finalToExit;

    [SerializeField]
    GameObject player;

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
        float[] playerPosition = {player.transform.position.x, player.transform.position.y, player.transform.position.z};
        float[] playerRotation = {player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w};
        PlayerData pd = new PlayerData(SceneManager.GetActiveScene().name, playerPosition, playerRotation, 
        PlayerExperience.health, 0, 0, 0, new string[3], "", ShipController.fuel);
        SaveSystem.Save(pd);
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
