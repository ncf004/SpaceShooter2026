using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    public float enterSpeed = 3f;
    public float verticalSpeed = 2f;
    public float stopX = 8f;
    public float yLimit = 4f;

    [Header("Combat")]
    public float health = 20f;
    public GameObject expoPrefab;
    public GameObject bossBulletPrefab;
    public Transform bossBulletSpawn;
    public float minShootDelay = 1.5f;
    public float maxShootDelay = 3f;

    private bool hasEntered = false;
    private float shootTimer;
    private int verticalDirection = 1;

    void Start()
    {
        shootTimer = Random.Range(minShootDelay, maxShootDelay);
    }

    void Update()
    {
        HandleMovement();

        if (hasEntered)
        {
            HandleShooting();
        }
    }

    private void HandleMovement()
    {
        if (!hasEntered)
        {
            if (transform.position.x > stopX)
            {
                transform.Translate(Vector3.left * enterSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(stopX, transform.position.y, transform.position.z);
                hasEntered = true;
            }

            return;
        }

        transform.Translate(Vector3.up * verticalDirection * verticalSpeed * Time.deltaTime);

        if (transform.position.y >= yLimit)
        {
            transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
            verticalDirection = -1;
        }
        else if (transform.position.y <= -yLimit)
        {
            transform.position = new Vector3(transform.position.x, -yLimit, transform.position.z);
            verticalDirection = 1;
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = Random.Range(minShootDelay, maxShootDelay);
        }
    }

    private void ShootAtPlayer()
    {
        if (bossBulletPrefab == null || bossBulletSpawn == null)
        {
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            return;
        }

        Vector3 targetPos = playerObj.transform.position;
        Vector3 direction = (targetPos - bossBulletSpawn.position).normalized;

        GameObject bulletObj = Instantiate(bossBulletPrefab, bossBulletSpawn.position, Quaternion.identity);

        BossBullet bossBullet = bulletObj.GetComponent<BossBullet>();
        if (bossBullet != null)
        {
            bossBullet.SetDirection(direction);
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

    private void Die()
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

        if (Score.Instance != null)
        {
            Score.Instance.HitEnemy();
        }
        
        Destroy(gameObject);
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
        }
    }
}