using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public GameObject stage2StartGate;
    private GameObject player;
    public ParticleSystem particleFactory;
    public GameObject logInPlatform;

    Animator ani;

    public void Start()
    {
        player = GameObject.Find("Player");
        ani = stage2StartGate.GetComponent<Animator>();
        StartCoroutine(nameof(StageStartAni));
    }


    private IEnumerator StageStartAni()
    {
        ani.SetBool("gate", true);
        player.transform.position = stage2StartGate.transform.position;
        yield return new WaitForSeconds(0.5f);

        GameObject particle = Instantiate(particleFactory).gameObject;
        particle.transform.position = player.transform.position; 

        yield return new WaitForSeconds(0.5f);
        logInPlatform.SetActive(false);
        ani.SetBool("gate", false);
    }

}
