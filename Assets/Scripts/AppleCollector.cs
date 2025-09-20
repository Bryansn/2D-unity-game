using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleCollector : MonoBehaviour
{
    public static int applesCollected = 0; 
    public GameObject winScreen;        
    public int totalApple = 10;

    public void Collect(PlayerController player)
    {
        Debug.Log("Collected apple");
        applesCollected++;
        Destroy(gameObject);

        if (applesCollected >= totalApple)
        {
            Debug.Log("all apples collected" + totalApple);
            GameEndMenu.endmessage = "YOU WIN!";
            SceneManager.LoadScene("end screen");
        }
    }

}
