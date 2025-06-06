using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool isLoading = false;
    bool isQuitting = false;

    public void Play()
    {
        if(isLoading) return;
        isLoading = true;

        Debug.Log(nameof(Play));

        SceneManager.LoadScene(1);
        // SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        if(isQuitting) return;
        isQuitting = true;

        Debug.Log(nameof(Quit));

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
