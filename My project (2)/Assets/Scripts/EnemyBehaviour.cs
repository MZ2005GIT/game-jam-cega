using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet")) {
            Destroy(gameObject);
        }
    }
}
