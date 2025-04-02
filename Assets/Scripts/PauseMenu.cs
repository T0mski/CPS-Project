using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    [SerializeField]
    private BoolSO PausedSO;
    private void Start()
    {
        PausedSO.Value = false;
    }
    // Used to reume the game when called.
    public void Resume()
    {
        pauseMenu.SetActive(false); // Deactivates the pause menu.
        Time.timeScale = 1; // Restarts the game time to default settings.
        PausedSO.Value = false;// Sets the program wide Paused variable to true
                               // stopping specific functions of the game

    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    // used to pause the game when called.
    private void Pause()
    {
        pauseMenu.SetActive(true); // activates the pause menu.
        Time.timeScale = 0; // stops the game time.
        PausedSO.Value = true; // sets the program wide Paused variable to true
                               // stopping specific functions of the game
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausedSO.Value)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
}
