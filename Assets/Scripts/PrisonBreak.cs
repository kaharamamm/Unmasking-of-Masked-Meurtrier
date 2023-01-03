using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Managing.Object;
using FishNet.Object;
using TMPro;
using UnityEngine.UI;

public class PrisonBreak : MonoBehaviour
{
    public SceneManagement scenem;
    public AudioSource pickup;
    public GameObject musicmanager;
    public MusicManagementMenu musicmenu;

    void Update()
    {
        pickup.volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            scenem.points++;
            musicmenu.GetComponent<AudioSource>().Play();
            Debug.Log("Point Taken by " + other.gameObject.name + "Total Points = " + scenem.points);
            this.gameObject.SetActive(false);
        }
    }
}
