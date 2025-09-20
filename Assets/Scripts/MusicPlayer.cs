
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        // If there's already a MusicPlayer, destroy this one
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
