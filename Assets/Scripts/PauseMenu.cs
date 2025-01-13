using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private BoolSO PausedSO;
    public void Resume()
    {
        gameObject.SetActive(false);
        PausedSO.Value = false;
        Time.timeScale = 1;
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
    private void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        PausedSO.Value = true;
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
