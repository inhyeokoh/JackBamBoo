using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
    public static GateManager Instance;
    [SerializeField] LayerMask layerMask;
    
    GameObject chicken;
    Vector3 overlapPos1;
    Vector3 overlapPos2;
    bool isOverlap1;
    bool isOverlap2;
    bool petActivate;

    // 씬1은 텔레포트매니저, 씬2,3은 게이트매니저
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            overlapPos1 = GameObject.Find("Stage2gate1").transform.position;
            overlapPos2 = GameObject.Find("Stage2gate2").transform.position;
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            overlapPos1 = GameObject.Find("Stage3gate1").transform.position;
            overlapPos2 = GameObject.Find("Stage3gate2").transform.position;
            chicken = GameObject.Find("Chicken");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DoorsOpen();
        }
    }

    public void DoorsOpen()
    {
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            Door1Open();
            Door2Open();
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            Door3Open();
            Door4Open();
        }
    }

    private void Door1Open()
    {
        isOverlap1 = Physics2D.OverlapCircle(overlapPos1, 1f, layerMask);
        if (!isOverlap1 || GameManager.Instance.frontDoor)
        {
            return;
        }
        else
        {
            GameManager.Instance.frontDoor = true;
            SceneManager.LoadScene("Stage3");
        }
    }

    private void Door2Open()
    {
        isOverlap2 = Physics2D.OverlapCircle(overlapPos2, 1f, layerMask);
        if (!isOverlap2)
        {
            return;
        }
        else
        {
            GameManager.Instance.smokeStack = true;
            SceneManager.LoadScene("Stage3");
        }
    }

    private void Door3Open()
    {
        isOverlap1 = Physics2D.OverlapCircle(overlapPos1, 1f, layerMask);
        if (!isOverlap1)
        {
            return;
        }
        else
        {
            SceneManager.LoadScene("Stage2");
        }
    }

    private void Door4Open()
    {
        isOverlap2 = Physics2D.OverlapCircle(overlapPos2, 1f, layerMask);
        petActivate = chicken.GetComponent<PetMove>().activate;
        if (!isOverlap2 || !petActivate)
            return;
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }
}
