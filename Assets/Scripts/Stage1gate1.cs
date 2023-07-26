using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1gate1 : MonoBehaviour
{
    [SerializeField] Transform overlapPos;
    [SerializeField] LayerMask teleportLayer;
    [SerializeField] ParticleSystem particleFactory;
    [SerializeField] GameObject player;
    Animator ani;
    bool isOverlap;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        isOverlap = Physics2D.OverlapCircle(overlapPos.position, 1f, teleportLayer);
        if (isOverlap)
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
