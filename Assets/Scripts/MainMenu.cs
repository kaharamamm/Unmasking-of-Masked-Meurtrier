using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() // Application.Quit() does not work in the editor so UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Qutting");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Debug.Log("Qutting");
        UnityEngine.Application.Quit();
#endif
    }
}
