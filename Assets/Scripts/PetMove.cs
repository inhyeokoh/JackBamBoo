using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMove : MonoBehaviour
{
    [SerializeField] ParticleSystem shining;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    Animator ani;
    Transform player;

    public bool activate;
    float speed = 3f;
    float distance = 1f;
    float jumpPower = 5f;
    float teleportDist = 12f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activate = true;
            shining.gameObject.SetActive(false);
        }
    }

    private void Update() 
    {
        if (activate)
        {
            if (Mathf.Abs(transform.position.x - player.position.x) > distance)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
                ani.SetBool("isRun", true);  

                // Vector3.right은 (1,0,0)으로 고정이지만 tranform.right은 local 좌표상에서의 오른쪽.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, groundLayer);

                // 대각선 윗방향 (1,1) 또는 (-1,1)로 raycast를 쏨.
                RaycastHit2D hitDiagonal = Physics2D.Raycast(transform.position, new Vector2(DirectionPet(), 1), 2f, groundLayer);
                
                // 플레이어가 펫보다 아래에 있을 때 펫 혼자서 점프지형에 올라가는것을 방지
                if (player.position.y <= transform.position.y)
                    hitDiagonal = new RaycastHit2D();
                else if (hit || hitDiagonal)
                {
                    rb.velocity = Vector2.up * jumpPower;
                }
            }  
            else
            {
                ani.SetBool("isRun", false);
            }

            if (Vector2.Distance(player.position, transform.position) > teleportDist)
            {
                transform.position = player.position;
            }            
        }
    }

    private float DirectionPet()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return 1;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }
    }

}
