using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMove : MonoBehaviour
{
    [SerializeField] AudioSource footstepsAudio;
    [SerializeField] AudioSource jumpAudio;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator ani;

    float speed;
    float timer;
    float changeAction = 5f;
    float jumpPower;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > changeAction)
        {
            // 1/3확률로 정지, 2/9 확률로 뛰면서 점프, 4/9 확률로 뛰어다님. 속도와 점프높이는 랜덤값 부여.
            if (Random.Range(0, 3) == 0)
            {
                speed = 0f;
            }
            else
            {
                speed = Random.Range(-4f, 4f);
                if (Random.Range(0, 3) == 0)
                {
                    ani.SetTrigger("jump");
                    jumpPower = Random.Range(8f, 11f);
                    rb.velocity = Vector2.up * jumpPower;
                }
            }
            timer = 0;
        }
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

    // 제한 범위 이탈시에 방향만 바꿔주었더니 같은 자리에서 방향을 무한으로 변경하는 잔버그가 있었음.
    private void LimitGiantXpos()
    {
        // speed 값이 0 미만이면 true 아니면 반대.
        sr.flipX = speed < 0;
        if (transform.position.x > 18.5f)
        {
            transform.position = new Vector3 (transform.position.x - 0.1f, transform.position.y, 0);
            speed = speed * -1f;
        }
        else if(transform.position.x < 2.85f)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
            speed = speed * -1f;
        }
    }
}
