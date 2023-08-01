using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMove : MonoBehaviour
{
    float speed;
    float timer;
    float changeAction = 5f;
    float jumpPower;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator ani;
    [SerializeField] AudioSource footstepsAudio;
    [SerializeField] AudioSource jumpAudio;

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
            // 1/3Ȯ���� ����, 2/9 Ȯ���� �ٸ鼭 ����, 4/9 Ȯ���� �پ�ٴ�. �ӵ��� ���� ���̴� ���� �� ������ �ο�
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

    // ���� ���� ��Ż�ÿ� ���⸸ �ٲ��־����� ���� �ڸ����� ������ �������� �����ϴ� �ܹ��װ� �־���
    private void LimitGiantXpos()
    {
        /*        if (speed < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;        
        }*/
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
