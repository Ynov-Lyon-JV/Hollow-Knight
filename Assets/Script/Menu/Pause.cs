using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject spellBook;

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
        spellBook = GameObject.Find("UI_Spells");
        spellBook.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown(Dico.Get("BUTTON_CANCEL")))
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (gameIsPaused)
            {
                LoadMainMenu();
            }
        }
        if (Input.GetButtonDown(Dico.Get("BUTTON_SPELLMENU")))
        {
            if (spellBook.activeSelf)
            {
                Player.instance.Resume();
                spellBook.SetActive(false);
            }
            else
            {
                spellBook.SetActive(true);
                Player.instance.Pause();
            }
        }
    }

    public void Paused()
    {
        Player.instance.Pause();
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Player.instance.Resume();
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