using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject expoPrefab;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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
            Instantiate(expoPrefab, transform.position, Quaternion.identity);

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