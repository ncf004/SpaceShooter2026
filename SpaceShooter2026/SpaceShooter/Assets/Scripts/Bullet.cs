using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 1f;
    public bool piercesEnemies = false;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ScreenOutOfBounds"))
        {
            Destroy(gameObject);
            return;
        }

        if (c.CompareTag("Shooter"))
        {
            Shooter shooter = c.GetComponent<Shooter>();
            if (shooter != null)
            {
                shooter.TakeDamage(damage);
            }

            if (!piercesEnemies) Destroy(gameObject);
            return;
        }

        if (c.CompareTag("Sprinter"))
        {
            Sprinter sprinter = c.GetComponent<Sprinter>();
            if (sprinter != null)
            {
                sprinter.TakeDamage(damage);
            }

            if (!piercesEnemies) Destroy(gameObject);
            return;
        }

        if (c.CompareTag("Boss"))
        {
            Boss boss = c.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            if (!piercesEnemies) Destroy(gameObject);
            return;
        }
    }
}