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
        // ������ �� ������ �÷��̾ ���������� isOverlap ������ true
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
                // ���� ����Ͽ��� �۵� �ȵɰ���
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
            // isOverlap�� true �� ���¿��� �� ����Ű�� ���� ��Ż�� ������ frontDoor bool���� true
            GameManager.Instance.frontDoor = true;
            SceneManager.LoadScene("Stage3");
        }
    }*/
}
