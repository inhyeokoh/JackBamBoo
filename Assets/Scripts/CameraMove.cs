using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject target;
    private float speed = 3f;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void Start()
    {
        target = GameObject.Find("Player");
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = target.transform.position;
        transform.position = Vector3.Lerp(playerPos, transform.position, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x , center.x - lx, center.x + lx);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y , center.y - ly, center.y + ly);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
