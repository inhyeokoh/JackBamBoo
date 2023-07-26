using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2gate1 : MonoBehaviour
{
    [SerializeField] Transform overlapPos;
    [SerializeField] LayerMask stageLayer;
    bool isOverlap;

    private void Update()
    {
        // 거인의 집 정문과 플레이어가 겹쳤을때만 isOverlap 변수에 true
        /*        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, stageLayer);
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    DoorOpen();        
                }*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, stageLayer);
            if (!isOverlap)
            {
                return;
            }
            else
            {
                // 지금 모바일에선 작동 안될거임
                GameManager.Instance.frontDoor = true;
                SceneManager.LoadScene("Stage3");
            }
        }

    }


/*    public void DoorOpen()
    {
        if (!isOverlap)
        {
            return;
        }        
        else
        {
            // isOverlap이 true 인 상태에서 윗 방향키를 눌러 포탈에 들어오면 frontDoor bool값을 true
            GameManager.Instance.frontDoor = true;
            SceneManager.LoadScene("Stage3");
        }
    }*/
}
