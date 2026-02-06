using UnityEngine;
using System.Collections; // Required for Coroutines

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public bool isMasked = false;
    public bool underToOver = true;
    public bool overToUnder = true;

    [Header("World References")]
    public GameObject overworldParent;
    public GameObject underworldParent;
    public GameObject underworldObjects;
    public GameObject underworldArea;
    public GameObject overworldArea;

    [Header("Shooting Settings")]
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 movement;
    int maskedLayerIndex;
    private Coroutine speedBoostCoroutine;

    [Header("UI References")]
    public GameObject pickupPanel; // Drag your panel here in the Inspector

    public void FlashPickupPanel()
    {
        if (pickupPanel != null)
        {
            pickupPanel.SetActive(true);
            // This calls the method below after 1 second
            Invoke(nameof(DeactivatePanel), 1f); 
        }
    }

    void DeactivatePanel()
    {
        pickupPanel.SetActive(false);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maskedLayerIndex = animator.GetLayerIndex("Masked Layer");
        
        // Set initial world state
        UpdateWorldState();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "OverArea")
        {
            underToOver = true;
        }
        if (collision.gameObject.name == "UnderArea")
        {
            overToUnder = true;
        }

        if (collision.CompareTag("Enemy"))
        {
            if (GetComponent<PlayerHealth>() != null)
                GetComponent<PlayerHealth>().TakeDamage(20f);
        }

        if (collision.CompareTag("WinBox"))
        {
            GameManager.Instance.PlayerWon();
            AudioManager.Instance.PlayLostSound();
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.name == "OverArea")
        {
            underToOver = false;
        }
        if (collision.gameObject.name == "UnderArea")
        {
            overToUnder = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if ((isMasked == false && overToUnder == true) || (isMasked == true && underToOver == true))
            {
                isMasked = !isMasked;
                UpdateWorldState(); // Switch worlds when toggling mask
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        animator.SetLayerWeight(maskedLayerIndex, isMasked ? 1f : 0f);
        animator.SetBool("isWalking", movement != Vector2.zero);

        if (movement.x < 0) {
            spriteRenderer.flipX = true;
            firePoint.right = Vector2.left; 
            firePoint.localPosition = new Vector3(0, firePoint.localPosition.y, 0);
        }
        else if (movement.x > 0) {
            spriteRenderer.flipX = false;
            firePoint.right = Vector2.right; 
            firePoint.localPosition = new Vector3(0, firePoint.localPosition.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isMasked && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    // This handles the visual switch
    void UpdateWorldState()
    {
        if (overworldParent != null && underworldParent != null)
        {
            // Underworld is ACTIVE when masked
            underworldParent.SetActive(isMasked);
            
            // Overworld is ACTIVE when NOT masked
            overworldParent.SetActive(!isMasked);
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
            AudioManager.Instance.PlayBulletSound();
        }
    }

    // --- SPEED BOOST METHODS ---
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostCoroutine != null) StopCoroutine(speedBoostCoroutine);
        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        float originalSpeed = speed;
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        speedBoostCoroutine = null;
    }

    void FixedUpdate() => rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

}