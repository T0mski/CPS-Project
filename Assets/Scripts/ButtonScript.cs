using JetBrains.Annotations;
using System;
using Unity;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //Input string that is input in the inspector panel, this allows me to have 1 piece of code that does the inputs for all of the buttons that load scenes in my game.
    public string SceneToLoad;

    // Is Listening for any input form buttons on any menu then the variable that is input is called using the load scene button.
    public void EnableScene()
    {
        SceneManager.LoadScene(SceneToLoad);
        
    }
    // Allows the player to quit the game.
    public void QuitGame()
    {
        Application.Quit();
    }

}
