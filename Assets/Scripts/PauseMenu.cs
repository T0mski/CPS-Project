using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool Paused;
    public void Resume()
    {
        gameObject.SetActive(false);
        Paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
