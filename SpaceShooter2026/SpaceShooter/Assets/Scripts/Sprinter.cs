using UnityEngine;

public class Sprinter : MonoBehaviour
{
    public float speed;
    public GameObject expoPrefab;
    public float health = 1f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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