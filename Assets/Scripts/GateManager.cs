using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
/*    [SerializeField] LayerMask teleportLayer;
    [SerializeField] ParticleSystem particleFactory;
    [SerializeField] GameObject player;

    Vector3 overlapPos1;
    Vector3 overlapPos2;
    Animator ani;
    bool isOverlap1;
    bool isOverlap2;


    // 씬1은 텔레포트매니저, 씬2,3은 게이트매니저

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage1")
        {

            ani = GetComponent<Animator>();
            overlapPos1 = GameObject.Find("Gate").transform.position;
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {

            overlapPos1 = GameObject.Find("Stage2gate1").transform.position;
            overlapPos2 = GameObject.Find("Stage2gate2").transform.position;
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            overlapPos1 = GameObject.Find("Stage3gate1").transform.position;
            overlapPos2 = GameObject.Find("Stage3gate2").transform.position;
        }
    }
    private void Update()
    {
        isOverlap1 = Physics2D.OverlapCircle(overlapPos1, 1f, teleportLayer);
        isOverlap2 = Physics2D.OverlapCircle(overlapPos2, 1f, teleportLayer);
        if (isOverlap1)
        {
            ani.SetBool("gate", true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DoorOpen();
            }
        }
        else
        {
            ani.SetBool("gate", false);
        }

        if (isOverlap2)
        {
            ani.SetBool("gate", true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DoorOpen();
            }
        }
        else
        {
            ani.SetBool("gate", false);
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
            if (SceneManager.GetActiveScene().name == "Stage1")
            {
                GameObject particle = Instantiate(particleFactory).gameObject;
                particle.transform.position = player.transform.position;
                StartCoroutine(TeleportMove(1.5f));
            }
            else if (true)
            {

            }

        }
    }
    public void Door2Open()
    {
        if (!isOverlap)
        {
            return;
        }
        else
        {
            GameManager.Instance.frontDoor = true;
            SceneManager.LoadScene("Stage3");
        }
    }

    IEnumerator TeleportMove(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);

        SceneManager.LoadScene("Stage2");
    }*/
}
