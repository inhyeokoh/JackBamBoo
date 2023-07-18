using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject target;
    private float speed = 3f;

    public Vector2 center;
    // ���� ��ü ũ��
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

        // ī�޶� ���ߴ� ������ ����, ���� ũ���� ������ ���� width�� height ������ ����.
        // ī�޶� ������Ʈ�� size ���� ���ʿ� �߾Ӻ��� ��ܱ���(���� 1/2)�� �ǹ�.
        // Screen.width, Screen.height�� �ػ� ����, ���� ũ���� �ȼ� ���� ��ȯ.
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        // Lerp(���� ����)�� ����Ͽ� �ε巯�� �������� ǥ���� ������.
        // a���� b���� �̵��ÿ� t���� Ŭ���� ������ ������.
        transform.position = Vector3.Lerp(target.transform.position,
                                            transform.position,
                                            Time.deltaTime * speed);

        // ī�޶��� �߽����� size�� ���ݿ��� ī�޶� ���ߴ� ���� ���ݸ�ŭ(width)�� ����
        // ���������� �̵��ϵ��� �����ؾ� ī�޶� ���ߴ� ������ ���� ����� ����.
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);

        // ī�޶� ĳ���Ϳ� ���� ���� ��ġ�ϸ� �ȵǹǷ� z��ǥ�� 0�� �ƴ� -10f�� ������Ŵ.
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
