using FishNet.Example.Scened;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FishNet.Object;
using FishNet.Connection;

public class SceneManagement : NetworkBehaviour
{
    [SerializeField] private GameObject Settings, playerhud, ironbars1, ironbars2, Endgame;
    [SerializeField] private GameObject[] Cubes;
    public TextMeshProUGUI pointstext;
    public int points, endcheck = 0;
    public Image endresult;
    void Update()
    {
        playerhud.SetActive(true);
        pointstext.text = points.ToString() + "/10";
        if (points == 5)
        {
            ironbars1.SetActive(false);
            ironbars2.SetActive(false);
            Debug.Log("Points 5 oldu");
        }
        if (points == 10)
        {
            pointstext.text = "";
            points = 0;
            ironbars1.SetActive(true);
            ironbars2.SetActive(true);
            foreach (GameObject item in Cubes)
            {
                item.SetActive(true);
            }
            Cursor.visible = true;
            Endgame.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // If ESC button is pressed, active the settings screen and pause the game.
        {
            if (Settings.activeSelf == true)
            {
                Cursor.visible = false;
                Settings.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                Settings.SetActive(true);
            }
        }
    }
}
