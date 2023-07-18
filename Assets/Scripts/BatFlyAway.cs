using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyAway : MonoBehaviour
{
    Animator ani;
    AudioSource flyingAudio;
    private bool bFlying;
    private void Start()
    {
        ani = GetComponent<Animator>();
        flyingAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            ani.SetTrigger("FlyAway");
            flyingAudio.Play();
            bFlying = true;
            Destroy(gameObject, 15f);
        }
    }

    private void Update()
    {
        if (bFlying)
        {
            transform.Translate(new Vector3(-1, 1, 0) * Time.deltaTime);
        }
    }

}
