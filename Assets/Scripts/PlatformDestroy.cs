using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("SetFalse", 0.7f);
        }
    }

    private void SetFalse()
    {
        gameObject.SetActive(false);
        Invoke("SetTrue", 2f);
    }
    private void SetTrue()
    {
        gameObject.SetActive(true);
    }


}
