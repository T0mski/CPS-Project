using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private int Score;
    public TMP_Text score;
    [SerializeField]
    private IntSO scoreSO;

   private void Update()
    {

        score.text = scoreSO.Value.ToString();
        
        
        

    }
}
