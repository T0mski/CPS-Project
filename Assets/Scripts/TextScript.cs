using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public TMP_Text score; // Reference to the Text component on the canvas panel.
    [SerializeField]
    private IntSO scoreSO; // privare inscance of an int scriptable object (SO) 

   private void Update()
    {
        // sets the text to the value of the SO.
        score.text = scoreSO.Value.ToString();

    }
}
