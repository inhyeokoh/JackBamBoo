using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1gate : MonoBehaviour
{
    [SerializeField] Transform overlapPos;
    [SerializeField] LayerMask teleportLayer;
    [SerializeField] ParticleSystem shining;
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
            shining.gameObject.SetActive(true);            
            StartCoroutine(TeleportMove(1.5f));
        }
    }

    IEnumerator TeleportMove(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);

        shining.gameObject.SetActive(false);
        SceneManager.LoadScene("Stage2");
    }

}
