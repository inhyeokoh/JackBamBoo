using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleFactory;
    [SerializeField] GameObject logInPlatform;

    Animator ani;

    public void Start()
    {
        ani = GetComponent<Animator>();
        if (!GameManager.Instance.visitStage)
        {
            StartCoroutine("StageStartAni");
        }
    }

    private IEnumerator StageStartAni()
    {
        ani.SetBool("gate", true);
        yield return new WaitForSeconds(0.5f);

        GameObject particle = Instantiate(particleFactory).gameObject;
        transform.position = particle.transform.position; 

        yield return new WaitForSeconds(0.5f);
        logInPlatform.SetActive(false);
        ani.SetBool("gate", false);

        GameManager.Instance.visitStage = true;
    }
}
