using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollector : MonoBehaviour
{
    public static int applesCollected = 0; 
    public GameObject winScreen;        
    public int totalApples = 10;

    public void Collect(PlayerController player)
    {
        Debug.Log("Collected apple");
        applesCollected++;
        Destroy(gameObject);

        if (applesCollected >= totalApples)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        GameState.endMessage = "You Win!"; // ? Set your message

        // Optional: Reset time scale in the next scene
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("end screen");
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Collect(player);
            }
        }
    }

}
