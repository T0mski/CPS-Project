using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private int Score;
    public TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetInt("Score");
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Score > 0)
        {
            score.text = Score.ToString();
            Debug.Log(Score);
        }

    }
}
