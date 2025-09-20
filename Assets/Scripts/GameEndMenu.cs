using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Needed for TextMeshProUGUI

public class GameEndMenu : MonoBehaviour
{
    public static string endmessage = "";
    public TextMeshProUGUI endMessageText; // Reference to the message text

    // Call this from another script to set the message
    //public void SetEndMessage(string message)
    //{
    //    if (endMessageText != null)
    //    {
    //        endMessageText.text = message;
    //    }
    //}

    void Start()
    {
        if (endmessage != null)
        {
            endMessageText.text = endmessage;
        }
    }

    public void RestartGame()
    {
        AppleCollector.applesCollected = 0;
        SceneManager.LoadScene("testscene 1");
    }

    public void QuitGame()
    {
        Debug.Log("quit pressed");
        Application.Quit();
    }
}

