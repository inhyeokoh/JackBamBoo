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
        // �ش� ������Ʈ�� �÷��̾ ���������� isOverlap ������ true
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
            // isOverlap�� true �� ���¿��� �� ����Ű�� ���� ��Ż�� ������ frontDoor bool���� true
            GameObject.Find("GameManager").GetComponent<GameManager>().frontDoor = true;
            // stage3�� �������� PlayerPrefs�� �̸� ���ϴ� stage3 ��ġ ��ǥ x,y�� ����  
            PlayerPrefs.SetFloat("PlayerX", -7.5f);
            PlayerPrefs.SetFloat("PlayerY", -1.75f);
            SceneManager.LoadScene("Stage3");
        }
    }
}
