using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public void RestartGame() {
        SceneManager.LoadScene("testscene 1");
    }
    public void QuitGame() {
        Debug.Log("quit pressed");
        Application.Quit();
    }
}
