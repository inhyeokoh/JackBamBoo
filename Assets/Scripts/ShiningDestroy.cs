using UnityEngine;

// �÷��̾ �߰� �����ϸ� �� �ֺ� �� ����
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
