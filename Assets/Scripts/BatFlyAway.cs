using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyAway : MonoBehaviour
{
    Animator ani;
    AudioSource flyingAudio;
    bool isFlying;

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
            isFlying = true;
            Destroy(gameObject, 15f);
        }
    }

    private void Update()
    {
        if (isFlying)
        {
            transform.Translate(new Vector3(-1, 1, 0) * Time.deltaTime);
        }
    }

}
