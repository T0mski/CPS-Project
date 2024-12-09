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

    public string SceneToLoad;
   


    // Is Listening for any input form buttons on any menu then the variable that is input is called using the load scene button.
    public void EnableScene()
    {
        SceneManager.LoadScene(SceneToLoad);
        PlayerPrefs.SetInt("Score", 0);
    }

}
