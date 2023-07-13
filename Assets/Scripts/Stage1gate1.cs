using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1gate1 : MonoBehaviour
{
    private GameObject player;
    public Transform overlapPos;
    public LayerMask teleportLayer;

    public ParticleSystem particleFactory;

    Animator ani;
    private bool isOverlap = true;
    private bool doOnce = false;


    private void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, teleportLayer);
        if (isOverlap)
        {
            ani.SetBool("gate", true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                DoorOpen();
        }
        else
        {
            ani.SetBool("gate", false);
        }

        if (SceneManager.GetActiveScene().name == "Jungle" && doOnce == false)
        {
            ani.SetBool("gate", true);
            ani.SetBool("gate", false);
            GameObject particle = Instantiate(particleFactory).gameObject;
            particle.transform.position = player.transform.position;
            doOnce = true;
        }

    }

    public void DoorOpen()
    {
        if (isOverlap == false)
            return;
        else
        {
            GameObject particle = Instantiate(particleFactory).gameObject;
            particle.transform.position = player.transform.position;
            Invoke("TeleportMove", 2f);
        }
    }


    private void TeleportMove()
    {
        SceneManager.LoadScene("Stage2");
    }  

}
