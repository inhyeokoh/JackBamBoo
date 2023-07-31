using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    [SerializeField] float getInactive;
    [SerializeField] float getActive;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("InActive", getInactive);
        }
    }

    private void InActive()
    {
        gameObject.SetActive(false);
        Invoke("Active", getActive);
    }
    private void Active()
    {
        gameObject.SetActive(true);
    }
}
