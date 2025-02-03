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
using UnityEditor.Experimental.GraphView;



public class QuizManager : MonoBehaviour
{
    public TMP_Text outQuestionText;            // Reference to UI Text for the question.
    public Button[] answerButtons;              // Reference to Array of Buttons for the answers.
    public TMP_Text[] ButtonTxt;                // Reference to Array of the Text of the Buttons.
    public TMP_Text feedbackText;               // Reference to Feedback text.

    public TMP_Text score;
  
    public OptionsScript optionsScript;

    private List<string> Topics;                // This will hold the selected topics from the OptionsScript.
    List<string> posQs = new List<string>();    // This is the possible questions that the player could be asked
    List<string> posOs = new List<string>();    // This is the possible options that the player could be hve to chose from.
    private int randomIndex;                    // Private variable for a random number
    private int correctIndex;                   // Private variable for the correct option index number e.g. 32 out of 55

    private bool DoOnce;                        // Self explanatory.

    [SerializeField]                           
    private IntSO scoreSO;                      // Scriptable object that allows the pass through of variables through different reloads of the game.
    
    private void Start()
    {
        
        feedbackText.text = "";                 // Clear feedback text initially
        Topics = optionsScript.selectedTopics;  // sets topic list equal to the 
        DisplayQuestion();                      // Display the first question
    }
    public void DisplayQuestion()
    {
        DoOnce = false;
        outQuestionText.text = GetRandomQuestionFromDict(Topics, topicQuestions);
        SetButtonTextToOptions();

    }
    public string GetRandomQuestionFromDict(List<string> values, Dictionary<string, List<(string, string[])>> QuestionDict)
    {
        // loops through the Topics in the values list.
        foreach (string value in values)
        {
            // checks if the value is in the dictionary.
            if (QuestionDict.TryGetValue(value, out List<(string, string[])> questionsArray))
            {
                // loops through the questions in the questions array.
                foreach ( var question in questionsArray)
                { 
                    // adds the first item in the questions tuple to the possible questions list.
                    posQs.Add(question.Item1);
                    // loops through the options in the second item of the questions tuple.
                    foreach(var option in question.Item2)
                    {
                        // adds the option to the possible options list.
                        posOs.Add(option);
                        
                    }
                    
                }
            }
        }
        // gets random number between 0 and the number of items in the posQs list.
        randomIndex = Random.Range(0, posQs.Count);
        //if the posqs list is longer than 0 it returns the item at the random index in the posQs list
        //else it returns "no Matching Question" which shouldnt be possible.
        if (posQs.Count > 0)
        {
            return posQs[randomIndex];
        }
        return "No Matching question found.";
    }
    public void SetButtonTextToOptions()
    {
        // gets a random number between 0 and 3
        int randButton = Random.Range(0, 4);
        randomIndex *= 4;       // multiplies itself by 4
        switch (randButton){
            case 0: //sets option A to be the correct answer
                // seach sets itself the the random index multiplyed by 4 to get the correct 4 options then increments the index by 1
                // depending on what place the button is in the list. does this for each case.
                ButtonTxt[0].text = posOs[randomIndex    ].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 0;
                break;
            case 1://sets option B to be the correct answer
                ButtonTxt[1].text = posOs[randomIndex].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 1;
                break;
            case 2://sets option C to be the correct answer
                ButtonTxt[2].text = posOs[randomIndex].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[3].text = posOs[randomIndex + 3].ToString();
                correctIndex = 2;
                break;
            case 3://sets option D to be the correct answer
                ButtonTxt[3].text = posOs[randomIndex].ToString();
                ButtonTxt[1].text = posOs[randomIndex + 1].ToString();
                ButtonTxt[2].text = posOs[randomIndex + 2].ToString();
                ButtonTxt[0].text = posOs[randomIndex + 3].ToString();
                correctIndex = 3;
                break;
            //default:// default value just skips the case.
            //    break;

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
                won();
            }
            else
            {
                score.text = "Last Try";
                SceneManager.LoadScene("MainGame");
            }
            
        }
        else
        {
            feedbackText.text = "Incorrect!";
            if (scoreSO.Value == 0)
            {
                Lost();
            }
            else if (scoreSO.Value == 1) 
            {
                DecrementScore();
                score.text = "Last Try";
                SceneManager.LoadScene("MainGame");
            }

            else
            {
                DecrementScore();
                DecrementScore();
                SceneManager.LoadScene("MainGame");
            }
            
        }
    }
    private void won()
    {
        
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainGame");
        
    }
    public void Lost()
    {
        SceneManager.LoadScene("Fail");
        
    }
    private void DecrementScore()
    {
        if (!DoOnce)
        {
            scoreSO.Value -= 1;
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
    {
                "Types Of Storage", new List<(string, string[])>
                {
                    ("What is the purpose of primary storage?", new string[] {"To store data and instructions currently in use", "To archive data", "To backup data", "To transmit data"}),
                    ("Which type of storage is non-volatile and stores the operating system?", new string[] {"ROM", "RAM", "Cache", "Virtual Memory"}),
                    ("Which storage medium uses magnetic disks to store data?", new string[] {"Hard Disk Drive", "Solid State Drive", "Optical Disc", "Flash Memory"}),
                    ("What is an advantage of solid-state storage over magnetic storage?", new string[] {"Faster data access", "Lower cost per GB", "Larger capacity", "Longer lifespan"})
                }
            },
            {
                "Types of Languages", new List<(string, string[])>
                {
                    ("What is a characteristic of a high-level language?", new string[] {"It is machine-independent", "It is hardware-specific", "It uses binary instructions", "It is written in assembly"}),
                    ("Which language type is used to write low-level hardware instructions?", new string[] {"Assembly Language", "Python", "JavaScript", "SQL"}),
                    ("What is the main purpose of an assembler?", new string[] {"To convert assembly code into machine code", "To interpret high-level code", "To compile Java programs", "To debug errors"}),
                    ("Which language is best suited for web development?", new string[] {"HTML and CSS", "Assembly", "C", "SQL"})
                }
            },
            {
                "Compression", new List<(string, string[])>
                {
                    ("What is the purpose of lossy compression?", new string[] {"To reduce file size by removing non-essential data", "To encrypt the file", "To backup data", "To improve file integrity"}),
                    ("Which is an example of a lossless compression format?", new string[] {"ZIP", "JPEG", "MP3", "MP4"}),
                    ("What is a trade-off when using lossy compression?", new string[] {"Loss of quality", "Increased file size", "Reduced compatibility", "Slower processing"}),
                    ("Which algorithm is commonly used in lossless compression?", new string[] {"Huffman Coding", "JPEG Compression", "MP3 Encoding", "AES Encryption"})
                }
            },
            {
                "Encryption/Hashing", new List<(string, string[])>
                {
                    ("What is the purpose of hashing in computer systems?", new string[] {"To create a fixed-size unique representation of data", "To encrypt data for transmission", "To compress data for storage", "To backup sensitive information"}),
                    ("Which encryption method uses a pair of public and private keys?", new string[] {"Asymmetric Encryption", "Symmetric Encryption", "Hashing", "Steganography"}),
                    ("What is a characteristic of a good hashing algorithm?", new string[] {"It produces unique and fixed-size hashes", "It is reversible", "It requires a secret key", "It is hardware-dependent"}),
                    ("Which encryption algorithm is widely used for securing online communications?", new string[] {"AES", "SHA-256", "MD5", "Huffman Encoding"})
                }
            },
            {
                "Databases", new List<(string, string[])>
                {
                    ("What is a primary key in a relational database?", new string[] {"A unique identifier for a record", "A field used for sorting", "An encryption key", "A backup key"}),
                    ("Which command is used to retrieve data from a database?", new string[] {"SELECT", "INSERT", "DELETE", "UPDATE"}),
                    ("What is the purpose of normalization in database design?", new string[] {"To reduce redundancy and improve data integrity", "To increase data size", "To create unstructured data", "To encrypt sensitive information"}),
                    ("Which type of database stores data in key-value pairs?", new string[] {"NoSQL", "Relational", "Hierarchical", "Graph"})
                }
            },
            {
                "Networks", new List<(string, string[])>
                {
                    ("What is the purpose of a network protocol?", new string[] {"To define rules for data communication", "To encrypt network data", "To compress transmitted files", "To store user credentials"}),
                    ("Which network topology connects all devices to a central hub?", new string[] {"Star Topology", "Bus Topology", "Mesh Topology", "Ring Topology"}),
                    ("What does the IP in IP address stand for?", new string[] {"Internet Protocol", "Internal Process", "Interconnected Path", "Information Packet"}),
                    ("Which layer of the OSI model handles data encryption?", new string[] {"Presentation Layer", "Transport Layer", "Network Layer", "Application Layer"})
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
