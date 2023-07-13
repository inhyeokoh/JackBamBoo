using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public Canvas endingCanvas;
    PlayFabManager playFabManager;
    int timeScore;
    int totalScore;

    private void Start()
    {
        // GameManager에서 엔딩씬에 도착했을때 기록해둔 timeScore 값을 가져와서 점수를 매김
        timeScore = GameObject.Find("GameManager").GetComponent<GameManager>().timeScore;
        totalScore = 1000000 / timeScore;
        playFabManager = GetComponent<PlayFabManager>();
        Invoke("AppearCanvas", 6f);
    }

    // 캔버스를 6초 뒤에 나타나는 캔버스에 점수를 표기하고 서버의 리더보드로 점수를 보냄.
    // if문으로 최고점을 다루고 있었으나 playFab리더보드에서는 갱신시에 알아서 최고점을 다루므로 현재는 무의미
    private void AppearCanvas()
    {
        endingCanvas.gameObject.SetActive(true);
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        Debug.Log(timeScore);
        Debug.Log(totalScore);
        Debug.Log(bestScore);
        endingCanvas.transform.Find("InputName").gameObject.SetActive(true);
        endingCanvas.transform.Find("OKbtn").gameObject.SetActive(true);
        endingCanvas.transform.Find("Score").gameObject.SetActive(true);
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = string.Format("Score : " + totalScore);
        PlayerPrefs.SetInt("BestScore", totalScore);
        PlayerPrefs.Save();        
    }

    public void PressOK()
    {
        playFabManager.SubmitName();
        playFabManager.SendLeaderboard(totalScore);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
