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

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    GameObject saveText;

    Animation savingAnimation;
    private static bool hasSaved;
    public static bool isSaving;
    public static bool GameOver = false;
    public static PlayerData pd;

    void Start()
    {
        hasSaved = false;
        isSaving = false;
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver) {
            Time.timeScale = 0;
            gameOver.SetActive(true);
            playerView.SetActive(false);
            GameOver = false;
        }
        else {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if ((pauseMenu.activeSelf || finalToExit.activeSelf) && isSaving == false)
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
    }

    public void Save() {
        hasSaved = true;
        isSaving = true;
        StartCoroutine(saveAnimation());
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

    IEnumerator saveAnimation() {
        savingAnimation = saveText.GetComponent<Animation>();
        saveText.SetActive(true);
        savingAnimation.Play();
        yield return new WaitForSecondsRealtime(2.8f);
        saveProcess();
        saveText.SetActive(false);
        isSaving = false;
        yield break;
    }

    public void saveProcess() {
        float[] playerPosition = {player.transform.position.x, player.transform.position.y, player.transform.position.z};
        float[] playerRotation = {player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w};
        PlayerData pd = new PlayerData(SceneManager.GetActiveScene().name, playerPosition, playerRotation, 
        PlayerExperience.health, 0, 0, 0, new string[3], "", ShipController.lastPlanet,  ShipController.fuel);
        SaveSystem.Save(pd);
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
