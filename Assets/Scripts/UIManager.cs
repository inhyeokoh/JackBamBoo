using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager.restart)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
