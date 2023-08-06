using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject tipboard;

    private void Start()
    {
        if (GameObject.Find("GameManager") != null && GameManager.Instance.restart)
        {
            StartGame();
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

    public void OpenTipboard()
    {
        tipboard.SetActive(true);
    }

    public void CloseTipboard()
    {
        tipboard.SetActive(false);
    }

}
