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
        // GameManager���� �������� ���������� ����ص� timeScore ���� �����ͼ� ������ �ű�
        timeScore = GameObject.Find("GameManager").GetComponent<GameManager>().timeScore;
        totalScore = 1000000 / timeScore;
        playFabManager = GetComponent<PlayFabManager>();
        Invoke("AppearCanvas", 6f);
    }

    // ĵ������ 6�� �ڿ� ��Ÿ���� ĵ������ ������ ǥ���ϰ� ������ ��������� ������ ����.
    // if������ �ְ����� �ٷ�� �־����� playFab�������忡���� ���Žÿ� �˾Ƽ� �ְ����� �ٷ�Ƿ� ����� ���ǹ�
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
