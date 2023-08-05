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
    int totalScore;

    private void Start()
    {
        // �������� ���������� GameManager���� timeScore ���� �����ͼ� ������ ����.
        totalScore = 1000000 / (int)GameManager.Instance.timeCount;
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
