using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed;
    public GameObject expoPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float health = 1f;

    private Vector3 startPos;
    private bool hasStopped = false;
    private bool hasShot = false;
    private float waitTimer = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float targetX = startPos.x - 5f;

        if (!hasStopped)
        {
            if (transform.position.x > targetX)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                hasStopped = true;
            }
        }
        else if (!hasShot)
        {
            Shoot();
            hasShot = true;
        }
        else
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= 2f)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (expoPrefab != null)
        {
            var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = expoObj.GetComponentInChildren<ParticleSystem>();

            if (ps != null)
            {
                Destroy(expoObj, ps.main.duration);
            }
            else
            {
                Destroy(expoObj, 1f);
            }
        }

        if (PowerUpSpawner.Instance != null)
        {
            PowerUpSpawner.Instance.TrySpawnPowerUp(transform.position);
        }

        if (Score.Instance != null)
        {
            Score.Instance.HitEnemy();
        }
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyKilled();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenOutOfBounds"))
        {
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.EnemyKilled();
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Player p = c.gameObject.GetComponent<Player>();
            if (p == null)
            {
                p = c.gameObject.GetComponentInParent<Player>();
            }

            if (p != null)
            {
                p.DamageFromEnemy();
            }

            Destroy(gameObject);
        }
    }
}