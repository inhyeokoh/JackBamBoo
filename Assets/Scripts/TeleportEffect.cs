using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem shining;
    [SerializeField] GameObject platform;

    Animator ani;

    public void Start()
    {       
        ani = GetComponent<Animator>();
        if (!GameManager.Instance.visitStage)
        {
            StartCoroutine("StageStartAni");
        }
    }

    // stage2 첫 도착시에만 발생하는 애니메이션
    private IEnumerator StageStartAni()
    {
        ani.SetBool("gate", true);

        PlayerMove.Instance.enabled = false;
        platform.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        shining.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        platform.SetActive(false);
        shining.gameObject.SetActive(false);
        ani.SetBool("gate", false);

        PlayerMove.Instance.enabled = true;
        GameManager.Instance.visitStage = true;
    }
}
