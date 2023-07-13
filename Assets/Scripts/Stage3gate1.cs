using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3gate1 : MonoBehaviour
{
    private bool isOverlap;
    public Transform overlapPos;
    public LayerMask stageLayer;

    private void Update()
    {
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
            SceneManager.LoadScene("Stage2");
        }
    }
}
