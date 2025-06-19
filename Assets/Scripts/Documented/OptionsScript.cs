using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    // List of toggles for each topic
    [System.Serializable]
    public class Topic
    {
        public string topicName;  // Display name of the topic
        public Toggle toggle;     // Reference to the toggle UI element
    }

    public List<Topic> topics;  // List of topics and toggles

    public Button confirmButton;  // Reference to the confirm button

    public List<string> selectedTopics = new List<string>() // Holds the selected topics
    {   "Structure of CPU",
        "Types Of Processor", 
        "Types Of Storage", 
        "Types of Language", 
        "Compression", 
        "Encryption/Hashing", 
        "Databases", 
        "Networks", 
        "Web Technologies",
        "Data Types and Structs",
        "Boolean Algebra"
    }; 

 
    // Called on the click of the Continue button on the options page.
    public void OnConfirmSelection()
    {
        selectedTopics.Clear();
        // Check each toggle and add the topic to the selected list if it's enabled
        foreach (var topic in topics)
        {
            if (topic.toggle.isOn)
            {
                selectedTopics.Add(topic.topicName);
            }
        }
        SceneManager.LoadScene("MainMenu");
    }
}