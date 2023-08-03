using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChickenExit : MonoBehaviour
{
    [SerializeField] GameObject pet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pet.GetComponent<PetMove>().activate)
        {
            pet.transform.position = transform.position + new Vector3(0.7f , 0.5f ,0);
        }
    }
}
