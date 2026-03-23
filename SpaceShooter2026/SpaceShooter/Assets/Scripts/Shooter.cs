using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed;
    public GameObject expoPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;

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

        // Phase 1: Move until 5 units left
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

        // Phase 2: Shoot once
        else if (!hasShot)
        {
            Shoot();
            hasShot = true;
        }

        // Phase 3: Wait 2 seconds, then resume movement
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
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenOutOfBounds"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);
            Destroy(expoObj, expoObj.GetComponentInChildren<ParticleSystem>().main.duration);

            Destroy(c.gameObject);
            Destroy(gameObject);
            Score.Instance.HitEnemy();
        }

        if (c.gameObject.CompareTag("Player"))
        {
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
            Destroy(gameObject);
        }
    }
}