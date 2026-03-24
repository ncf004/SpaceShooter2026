using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 moveDirection = Vector3.left;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            Player p = c.GetComponent<Player>();
            if (p == null)
            {
                p = c.GetComponentInParent<Player>();
            }

            if (p != null)
            {
                p.DamageFromEnemy();
            }

            Destroy(gameObject);
        }

        if (c.CompareTag("ScreenOutOfBounds"))
        {
            Destroy(gameObject);
        }
    }
}