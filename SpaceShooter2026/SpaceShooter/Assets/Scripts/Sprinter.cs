using UnityEngine;

public class Sprinter : MonoBehaviour
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