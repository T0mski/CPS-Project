using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;          // The question text
        public string[] options;            // Array of answer options
        public int correctOptionIndex;     // Index of the correct option
    }

    public TMP_Text questionText;            // UI Text for the question
    public Button[] answerButtons;          // Array of Buttons for the answers
    public TMP_Text feedbackText;          // Feedback text (e.g., "Correct!" or "Try Again")

    public List<Question> questions = new List<Question>();     // List of questions
    private int currentQuestionIndex = 0;                      // Current question index


    [SerializeField]
    private ArraySO QuestionsArray;
    private void Start()
    {
        feedbackText.text = "";             // Clear feedback text initially
        DisplayQuestion();                 // Display the first question
    }

    public void DisplayQuestion()
    {
        // Get the current question
        Question currentQuestion = questions[currentQuestionIndex];

        // Set the question text
        questionText.text = currentQuestion.questionText;

        // Set the answer button texts
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.options[i];

            // Add a listener to each button
            int index = i; // Capture the index for the listener
            answerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    public void CheckAnswer(int selectedOptionIndex)
    {
        // Check if the selected option is correct
        if (selectedOptionIndex == questions[currentQuestionIndex].correctOptionIndex)
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Try Again!";
        }

        // Wait for a moment and load the next question
        Invoke("NextQuestion", 2f); // Wait 2 seconds before moving to the next question
    }

    private void NextQuestion()
    {
        feedbackText.text = ""; // Clear feedback text

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestion(); // Display the next question
        }
        else
        {
            EndQuiz(); // No more questions, end the quiz
        }
    }

    private void EndQuiz()
    {
        questionText.text = "Quiz Complete!";
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false); // Hide the buttons
        }
    }

    
}
