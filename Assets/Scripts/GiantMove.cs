using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMove : MonoBehaviour
{
    float speed;
    float timer;
    float changeAction = 5f;
    float jumpPower;
    float jumpCycle;

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
        if (timer > changeAction)
        {
            // 3분의 1확률로 정지해있음
            if (Random.Range(0, 3) == 0)
            {
                speed = 0f;
            }
            else
            {
                speed = Random.Range(-4f, 4f);            
            }

            // 4에서 9사이의 수를 골라 5보다 작으면 점프
            jumpCycle = Random.Range(4f, 9f);
            jumpPower = Random.Range(8f, 11f);

            if (timer > jumpCycle)
            {
                ani.SetTrigger("jump");
                rb.velocity = Vector2.up * jumpPower;
            }
            timer = 0;
        }

        if (speed < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;        
        }

/*        ani.SetFloat("speed", speed); - 지워도 될듯*/
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
        if (transform.position.x > 18.5f || transform.position.x < 2.85f)
            speed = speed * -1f;
    }

}
