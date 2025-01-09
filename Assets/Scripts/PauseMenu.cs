using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private BoolSO PausedSO;
    public void Resume()
    {
        gameObject.SetActive(false);
        PausedSO.Value = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
