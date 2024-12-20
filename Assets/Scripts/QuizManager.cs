using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static OptionsScript;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderData;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Controls;



public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;          // The question text
        public string[] options;            // Array of answer options
        public int correctOptionIndex;     // Index of the correct option

        public Question(string text, string[] ops, int correctIndex)
        {
            questionText = text;
            options = ops;
            correctOptionIndex = correctIndex;

        }
    }

    public TMP_Text outQuestionText;          // UI Text for the question
    public Button[] answerButtons;          // Array of Buttons for the answers
    public TMP_Text[] ButtonTxt;
    public TMP_Text feedbackText;          // Feedback text (e.g., "Correct!" or "Try Again")

    public TMP_Text score;

    public Question questions;     // List of questions
    public OptionsScript optionsScript;

    private List<string> Topics;
    List<string> posQs = new List<string>();
    List<string> posOs = new List<string>();
    private int randomIndex;
    private int correctIndex;

    private bool DoOnce;

    [SerializeField]
    private IntSO scoreSO;
    
    private void Start()
    {
        
        feedbackText.text = "";             // Clear feedback text initially
        Topics = optionsScript.selectedTopics;
        DisplayQuestion();                 // Display the first question
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainGame");
        }

    }
    public void DisplayQuestion()
    {
        DoOnce = false;
        outQuestionText.text = GetRandomQuestionFromDict(Topics, topicQuestions);
        SetButtonTextToOptions();

    }

    public string GetRandomQuestionFromDict(List<string> values, Dictionary<string, List<(string, string[])>> QuestionDict)
    {
        
        
        
        foreach (string value in values)
        {
            if (QuestionDict.TryGetValue(value, out List<(string, string[])> questionsArray))
            {
                foreach ( var question in questionsArray)
                { 
                    posQs.Add(question.Item1);
                    foreach(var option in question.Item2)
                    {
                        
                        posOs.Add(option);
                        
                    }
                    
                }
            }
        }
        randomIndex = Random.Range(0, posQs.Count);
        if (posQs.Count > 0)
        {
            return posQs[randomIndex];
        }
        return "No Matching question found.";
    }

    public void SetButtonTextToOptions()
    {
        int randButton = Random.Range(0, 4);
        randomIndex *= 4;
        switch (randButton){
            case 0:
                ButtonTxt[0].text = posOs[randomIndex    ].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 0;
                break;
            case 1:
                ButtonTxt[1].text = posOs[randomIndex].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 1;
                break;
            case 2:
                ButtonTxt[2].text = posOs[randomIndex].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 2;
                break;
            case 3:
                ButtonTxt[3].text = posOs[randomIndex].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 3].ToString();
                correctIndex = 3;
                break;
            default:
               break;

        }
    }


   
    public void CheckAnswer(int selectedOptionIndex)
    {
        // Check if the selected option is correct
        if (selectedOptionIndex == correctIndex)
        {
            feedbackText.text = "Correct!";
            if(scoreSO.Value > 0)
            {
                DecrementScore();
                gameObject.SetActive(false);
            }
            else
            {
                score.text = "Last Try";
            }
            
        }
        else
        {
            feedbackText.text = "Incorrect!";
            if (scoreSO.Value == 0)
            {
                Lost();
            }
            else
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }
    public void Lost()
    {
        SceneManager.LoadScene("Fail");
    }

    private void DecrementScore()
    {
        if (!DoOnce)
        {
            scoreSO.Value += 1;
            DoOnce = true;
        }
    }
    


    Dictionary<string, List<(string Question, string[] Options)>> topicQuestions = new Dictionary<string, List<(string, string[])>>()
{
    { "Structure of CPU", new List<(string, string[])>
        {
            ("What is the function of the control unit (CU) in the CPU?",
                new string[] { "Controls the execution of instructions", "Performs arithmetic operations", "Manages memory allocation", "Executes input/output operations" }),
            ("Explain the role of the Arithmetic Logic Unit (ALU) in the CPU.",
                new string[] { "Performs arithmetic and logic operations", "Manages instruction sequencing", "Controls data flow", "Stores program instructions" }),
            ("What is the purpose of the registers in a CPU?",
                new string[] { "Stores temporary data during processing", "Holds the operating system", "Manages the program counter", "Handles hardware interrupts" }),
            ("Describe how the fetch-decode-execute cycle works.",
                new string[] { "Fetches an instruction, decodes it, then executes it", "Executes the program from memory directly", "Converts machine code into binary data", "Processes only arithmetic instructions" })
        }
    },
    { "Types of Processor", new List<(string, string[])>
        {
            ("What are the key differences between RISC and CISC architectures?",
                new string[] { "RISC uses simpler instructions, CISC uses complex instructions", "RISC is for desktop use, CISC for servers", "RISC uses GPUs, CISC does not", "CISC is faster than RISC" }),
            ("Explain the advantages of using multi-core processors.",
                new string[] { "They can process tasks in parallel", "They are more energy-efficient", "They increase single-thread performance", "They eliminate the need for RAM" }),
            ("Define parallel processing and give an example of its application.",
                new string[] { "Executing multiple tasks simultaneously; e.g., graphics rendering", "Performing arithmetic operations quickly", "Storing data on multiple servers", "Encrypting data for security" }),
            ("What is the purpose of a graphics processing unit (GPU)?",
                new string[] { "To handle graphics-related calculations", "To store video memory", "To manage display resolutions", "To connect the CPU to a monitor" })
        }
    },
    { "Web Technologies", new List<(string, string[])>
        {
            ("What is the difference between HTML and CSS?",
                new string[] { "HTML structures the content, CSS styles it", "HTML is a programming language, CSS is not", "CSS stores data, HTML retrieves it", "HTML is client-side, CSS is server-side" }),
            ("Define the purpose of JavaScript in web development.",
                new string[] { "To add interactivity to web pages", "To store user data", "To define web page layout", "To connect databases to websites" }),
            ("What is the role of HTTP in web technologies?",
                new string[] { "It is a protocol for transferring web resources", "It defines how websites store data", "It encrypts sensitive user data", "It handles DNS requests" }),
            ("Explain how cookies are used in web applications.",
                new string[] { "They store small pieces of user data on the client-side", "They encrypt user data for secure transmission", "They act as a firewall between servers", "They prevent unauthorized access to websites" })
        }
    },
    { "Data Types and Structs", new List<(string, string[])>
        {
            ("What is the difference between primitive and composite data types?",
                new string[] { "Primitive types are basic, composites are made of primitives", "Primitive types use less memory", "Composite types are immutable", "Primitive types are not used in modern languages" }),
            ("Define the term 'struct' in programming and give an example.",
                new string[] { "A user-defined data type; e.g., a point with x and y values", "A built-in integer type", "A library for managing data", "A method to optimize memory" }),
            ("Explain why type casting is used in programming.",
                new string[] { "To convert one data type into another", "To remove unnecessary data", "To optimize performance", "To store data permanently" }),
            ("What is the difference between static and dynamic typing?",
                new string[] { "Static typing checks types at compile-time, dynamic at runtime", "Static typing requires pointers", "Dynamic typing always uses strings", "Static typing is only used in assembly language" })
        }
    },
    { "Boolean Algebra", new List<(string, string[])>
        {
            ("Simplify the Boolean expression A + (A·B).",
                new string[] { "A", "A·B", "A+B", "0" }),
            ("What is De Morgan’s theorem in Boolean algebra?",
                new string[] { "¬(A + B) = ¬A · ¬B", "¬(A · B) = A + B", "¬(A + B) = A · B", "A + A = A" }),
            ("Explain the difference between AND, OR, and NOT gates in Boolean logic.",
                new string[] { "AND requires both inputs to be true, OR requires one, NOT inverts the input", "AND requires one input, OR requires both, NOT outputs both", "OR outputs 0 for true inputs", "NOT has three inputs" }),
            ("How do you construct a truth table for the expression A·B + ¬C?",
                new string[] { "List all input combinations for A, B, and C and evaluate the expression", "Evaluate only true inputs", "Use binary addition to compute outputs", "Invert all inputs and compute their sum" })
        }
    }
};
}
