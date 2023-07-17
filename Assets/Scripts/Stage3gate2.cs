using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3gate2 : MonoBehaviour
{
    private bool isOverlap;
    public Transform overlapPos;
    public LayerMask stageLayer;
    [SerializeField] GameObject Chicken;
    bool petActivate;

    private void Update()
    {
        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, stageLayer);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            DoorOpen();
    }

    public void DoorOpen()
    {
        petActivate = Chicken.GetComponent<PetMove>().activate;
        if (isOverlap == false || !petActivate)
            return;
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
