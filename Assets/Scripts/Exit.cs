using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Qutting");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Debug.Log("Qutting");
        UnityEngine.Application.Quit();
#endif
    }
}
