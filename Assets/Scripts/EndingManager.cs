using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingManager : MonoBehaviour
{
    [SerializeField] Canvas endingCanvas;
    [SerializeField] PlayFabManager playFabManager;
    int timeScore;
    int totalScore;

    private void Start()
    {
        // GameManager에서 엔딩씬에 도착했을때 기록해둔 timeScore 값을 가져와서 점수를 매김
        timeScore = GameManager.Instance.timeScore;
        totalScore = 1000000 / timeScore;
        Invoke("AppearCanvas", 6f);
    }

    // 6초 뒤에 나타나는 캔버스에 점수를 표기하고 서버의 리더보드로 점수를 보냄.
    private void AppearCanvas()
    {
        endingCanvas.gameObject.SetActive(true);
        endingCanvas.transform.Find("Score").gameObject.SetActive(true);
        GameObject.Find("Score").GetComponent<TMP_Text>().text = string.Format("Score : " + totalScore);
        playFabManager.SendLeaderboard(totalScore);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
