using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MainGameScript : MonoBehaviour
{
    public GameObject Tower;
    private float[] nextMult = new float[7] { 1f, 1.5f, 0.3f, 0.5f, 1.3f, 1.8f, 0.7f };
    public GameObject Character;

    private GameObject CurrentBuilding;
    public GameObject CurrentCollider;

    private bool DoneOnce = false;
    private bool DoOnce = false;

    public GameObject QuizManager;

    public GameObject PauseMenu;

    
    public CharacterScript characterScript;
    [SerializeField]
    private IntSO scoreSO;
    [SerializeField]
    private BoolSO PausedSO;

    void Update()
    {
        if (PausedSO.Value == false)
        {
            // once the player has made it to the next building it will "Reload the game" (starts again)
            // also adds score to the score card.
            if (Character.transform.position.x == Tower.transform.position.x && !DoneOnce)
            {

                DoneOnce = true;
                SceneManager.LoadScene("MainGame");
                AddScore();
            }
            // Sets different atributes of the object below the player .
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentBuilding = GameObject.Find("What is below.").GetComponent<whatisBelow>().other;
                CurrentBuilding.name = "PreviousBuilding";
                CurrentCollider.tag = "Building";
            }
            // Check if the player has fallen down to show the QuizManager so the player
            // can answer the question.
            if (scoreSO.Value >= 0 && characterScript.HasFallenDown)
            {
                QuizManager.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                int RandomNum = Random.Range(0, 7);
                float scaleFactor = nextMult[RandomNum];
                Vector3 origionalScale = Tower.transform.localScale;
                if (origionalScale.x == 0)
                {
                    origionalScale.x = 1f;
                }
                Tower.transform.localScale = new Vector3(origionalScale.x * scaleFactor, origionalScale.y, origionalScale.z);
            }

           
        }
    }

    private void AddScore()
    {
        if (!DoOnce)
        {
            scoreSO.Value += 1;
            DoOnce = true;
        }
    }


    private void Start()
    {
        QuizManager.SetActive(false);
     }
}

