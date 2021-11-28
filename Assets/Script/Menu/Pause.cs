using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    public static Pause instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance de l'interface de la vie est déjà existante!");
            return;
        }
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        PlayerMove.instance.enabled = false;
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        PlayerMove.instance.enabled = true;
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}