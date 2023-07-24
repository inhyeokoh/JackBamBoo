using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMove : MonoBehaviour
{
    public bool activate;
    float speed = 3f;
    float distance = 1f;
    float jumpPower = 5f;
    float teleportDist = 12f;

    Rigidbody2D rb;
    Animator ani;
    private Transform player;
    public LayerMask groundLayer;
    public ParticleSystem shining;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(7, 9);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activate = true;
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

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, groundLayer);

                RaycastHit2D hitDiagonal = Physics2D.Raycast(transform.position, new Vector2(DirectionPet(), 1), 2f, groundLayer);
                // 펫의 y좌표가 플레이어의 y좌표보다 높을 때 왜 초기화 하는지는 모르겠음
                if (player.position.y <= transform.position.y)
                    hitDiagonal = new RaycastHit2D();

                if (hit || hitDiagonal)
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
