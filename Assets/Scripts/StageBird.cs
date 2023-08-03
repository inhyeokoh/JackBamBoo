using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBird : MonoBehaviour
{
    [SerializeField] Transform destination;
    float speed = 2f;
    bool startMove; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startMove = true;
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
        if (startMove)
        {            
            transform.position = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * speed);
        }        
    }
}
