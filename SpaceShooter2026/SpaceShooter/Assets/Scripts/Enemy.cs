using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject expoPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet tag matched");

            Debug.Log("Destroying bullet");
            Destroy(c.gameObject);

            Debug.Log("Destroying enemy");
            Destroy(gameObject);

            Debug.Log("Updating score");
            Score.Instance.HitEnemy();

            var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);

            Debug.Log("Explosion instantiated");
            ParticleSystem ps = expoObj.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Debug.Log("Particle system found");
                Destroy(expoObj, ps.main.duration);
            }
            else
            {
                Debug.Log("No particle system found");
                Destroy(expoObj, 1f);
            }
        }

        if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
    }

   
    
}
