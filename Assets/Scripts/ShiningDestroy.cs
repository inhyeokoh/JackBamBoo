using UnityEngine;

// 플레이어가 닭과 접촉하면 닭 주변 빛 삭제
public class ShiningDestroy : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
