using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMoving : MonoBehaviour
{
    public float speed;
    public Transform startPoint;
    public Transform endPoint;
    private Transform destination;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        destination = endPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * speed);
        if (endPoint.position == transform.position)
        {
            destination = startPoint;
            if (gameObject.CompareTag("Dead"))
                sr.flipX = true;

        }
        else if (startPoint.position == transform.position)
        {
            destination = endPoint;
            if (gameObject.CompareTag("Dead"))
                sr.flipX = false;
        }
    }
}
