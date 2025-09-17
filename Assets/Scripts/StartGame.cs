using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("testscene");  // Change to your actual game scene name
    }
}
