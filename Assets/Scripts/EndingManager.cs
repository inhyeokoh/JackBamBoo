using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingManager : MonoBehaviour
{
    [SerializeField] Canvas endingCanvas;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] PlayFabManager playFabManager;
    int timeScore;
    int totalScore;

    private void Start()
    {
        // �������� ���������� GameManager���� timeScore ���� �����ͼ� ������ �ű�
        timeScore = GameManager.Instance.timeScore;
        totalScore = 1000000 / timeScore;
        Invoke("AppearCanvas", 6f);
    }

    // 6�� �ڿ� ��Ÿ���� ĵ������ ������ ǥ���ϰ� ������ ��������� ������ ����.
    private void AppearCanvas()
    {
        endingCanvas.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        scoreText.text = string.Format("Score : " + totalScore);
        playFabManager.SendLeaderboard(totalScore);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
