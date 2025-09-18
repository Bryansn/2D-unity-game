using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppleCollector : MonoBehaviour
{
    public int applesCollected = 0;
    public int totalApples = 10;

    public void Collect(PlayerController player)
    {
        Debug.Log("collected apple");
        Destroy(gameObject);
    }
}
