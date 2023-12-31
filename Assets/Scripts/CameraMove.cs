using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject player;

    [SerializeField] Vector2 center;
    // 맵의 전체 크기
    [SerializeField] Vector2 size;
    float speed = 3f;
    float height;
    float width;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        // 카메라가 비추는 영역의 가로 , 세로 크기의 절반을 각각 width와 height 변수로 저장.      
        // Screen.width, Screen.height는 해상도 가로, 세로 크기의 픽셀 값을 반환.
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        // Lerp(선형 보간)를 사용하여 부드러운 움직임을 표현이 가능함.
        // a에서 b까지 이동시에 t값이 클수록 빠르게 도달함.
        transform.position = Vector3.Lerp(player.transform.position,
                                            transform.position,
                                            Time.deltaTime * speed);

        // 카메라의 중심점이 size의 절반에서 카메라가 비추는 폭의 절반만큼(width)을 빼는 길이까지만
        // 이동하도록 제한해야 카메라가 비추는 영역이 맵을 벗어나지 않음. 맵에서 카메라 사각형을 끝에 붙인다고 생각하면 편함.
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);

        // 카메라가 캐릭터와 같은 선상에 위치하면 안되므로 z좌표를 0이 아닌 -10f로 고정시킴.
        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
