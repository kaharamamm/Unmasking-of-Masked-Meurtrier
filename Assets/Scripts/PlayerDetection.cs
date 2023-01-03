using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FishNet.Object;
using FishNet.Connection;

public class PlayerDetection : NetworkBehaviour
{
    public bool found = false;
    public GameObject Endgame;
    public Image endresult;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Detected!");
            found = true;
            PlayerPrefs.SetInt("EndGame", 0);
            Cursor.visible= true;
            Endgame.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        found = false;
    }

    void Update()
    {
        if (found)
        {
            PlayerPrefs.SetInt("EndGame", 0);
            Cursor.visible = true;
            Endgame.SetActive(true);
        }
    }
}
