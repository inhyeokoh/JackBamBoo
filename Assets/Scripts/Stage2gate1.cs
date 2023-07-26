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
        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, stageLayer);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DoorOpen();        
        }
    }

    public void DoorOpen()
    {
        if (!isOverlap)
        {
            return;
        }        
        else
        {
            // isOverlap�� true �� ���¿��� �� ����Ű�� ���� ��Ż�� ������ frontDoor bool���� true
            GameManager.Instance.frontDoor = true;
            // stage3�� �������� PlayerPrefs�� �̵��� stage3 ��ġ ��ǥ x,y�� ����  
            PlayerPrefs.SetFloat("PlayerX", -7.5f);
            PlayerPrefs.SetFloat("PlayerY", -1.75f);
            SceneManager.LoadScene("Stage3");
        }
    }
}
