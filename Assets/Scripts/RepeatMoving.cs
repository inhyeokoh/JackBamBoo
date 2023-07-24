using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݺ������� �����̴� ������ ���� ���� 
public class RepeatMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    Transform destination;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        destination = endPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
            // ���� flipX ����
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
