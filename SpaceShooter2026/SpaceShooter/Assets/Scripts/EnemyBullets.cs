using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public float speed = 6f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("ScreenOutOfBounds"))
        {
            Destroy(gameObject);
        }
    }
}