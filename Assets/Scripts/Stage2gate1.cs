using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2gate1 : MonoBehaviour
{
    private bool isOverlap;
    public Transform overlapPos;
    public LayerMask stageLayer;

    private void Update()
    {
        // 해당 오브젝트와 플레이어가 겹쳤을때만 isOverlap 변수에 true
        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, stageLayer);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            DoorOpen();
    }

    public void DoorOpen()
    {
        if (isOverlap == false)
            return;
        else
        {   
            // isOverlap이 true 인 상태에서 윗 방향키를 눌러 포탈에 들어오면 frontDoor bool값을 true
            GameObject.Find("GameManager").GetComponent<GameManager>().frontDoor = true;
            // stage3로 가기전에 PlayerPrefs에 미리 원하는 stage3 위치 좌표 x,y값 저장  
            PlayerPrefs.SetFloat("PlayerX", -7.5f);
            PlayerPrefs.SetFloat("PlayerY", -1.75f);
            SceneManager.LoadScene("Stage3");
        }
    }
}
