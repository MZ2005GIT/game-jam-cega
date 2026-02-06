using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 1f;
    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnEnable()
    {
        rb.velocity = transform.right * speed;
        Invoke("Deactivate", lifeTime);
    }

    void Deactivate() => gameObject.SetActive(false);

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            // Enemy.TakeDamage logic here
            gameObject.SetActive(false);
        }
    }

    void OnDisable() => CancelInvoke(); 
}