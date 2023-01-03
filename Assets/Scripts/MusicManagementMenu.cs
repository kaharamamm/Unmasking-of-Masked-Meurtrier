using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;

public class MusicManagementMenu : MonoBehaviour
{
    [SerializeField] private Slider Music, Volume;
    public AudioSource musicsample, selectbutton;
    [SerializeField] private bool m_screenone, m_screentwo;
    void Start()
    {
        m_screenone = true;
        m_screentwo = true;
    }

    void Awake() // Adding Music and Volue values, If there was a saved Music, Volume values
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            Music.value = PlayerPrefs.GetFloat("Music");
        }
        if (PlayerPrefs.HasKey("Volume"))
        {
            Volume.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && m_screenone == true) // Playing the music even when the scenes switched (For Menu)
        {
            musicsample.Play();
            m_screenone = false;
            m_screentwo = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 && m_screentwo == true) // Playing the music even when the scenes switched (For Game)
        {
            musicsample.Play();
            m_screentwo = false;
            m_screenone = true;
        }
    }

    void OnGUI() // Configure the values on GUI change
    {
        musicsample.volume = Music.value;
        Music.value = Music.value;
        selectbutton.volume = Volume.value;
        Volume.value = Volume.value;
        PlayerPrefs.SetFloat("Music", Music.value);
        PlayerPrefs.SetFloat("Volume", Volume.value);
    }

}
