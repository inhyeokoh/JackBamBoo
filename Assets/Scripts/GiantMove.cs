using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMove : MonoBehaviour
{
    private float speed;
    private float timer;
    private float changeMove = 5f;
    private float randJumpPower;
    private float randJumpTime;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator ani;
    public AudioSource footstepsAudio;
    public AudioSource jumpAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // 5초마다 동작 변경
        if (timer > changeMove)
        {
            // 3분의 1확률로 정지해있음
            if (Random.Range(0,3) == 0)
                speed = 0f;
            else
                speed = Random.Range(-4f, 4f);
            // 4에서 9사이의 수를 골라 5보다 작으면 점프
            randJumpTime = Random.Range(4f, 9f);
            randJumpPower = Random.Range(8f, 11f);

            if (timer > randJumpTime)
            {
                ani.SetTrigger("jump");
                rb.velocity = Vector2.up * randJumpPower;
            }
            timer = 0;
        }
        if (speed < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
        ani.SetFloat("speed", speed);
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        ani.SetFloat("speed", Mathf.Abs(speed));

        LimitGiantXpos();
    }

    public void FootStepsSound()
    {
        footstepsAudio.Play();
    }

    public void JumpSound()
    {
        jumpAudio.Play();
    }

    private void LimitGiantXpos()
    {
        if (transform.position.x > 18.5f || transform.position.x < 3.8f)
            speed = speed * -1f;
    }

}
