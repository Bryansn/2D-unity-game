using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppleCollector : MonoBehaviour
{
    public int applesCollected = 0;
    public int totalApples = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            applesCollected++;
            Destroy(collision.gameObject);

            Debug.Log("Apples: " + applesCollected + "/" + totalApples);

            if (applesCollected >= totalApples)
            {
                Debug.Log("yay");

            }
        }
    }
}
