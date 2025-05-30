using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log(nameof(Play));

        SceneManager.LoadScene(1);
        // SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log(nameof(Quit));

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
