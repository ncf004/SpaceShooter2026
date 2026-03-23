using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public float speed = 6f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Shield shield;
    public Slider sliderHealth;

    private float health;

    private const float Y_LIMIT = 4.6f;
    private const float X_LIMIT_LEFT = -.1f;
    private const float X_LIMIT_RIGHT = 16.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        sliderHealth.value = health;

        if (UserInput.Instance.input.Fire.WasPressedThisFrame())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }

        var vertMove = UserInput.Instance.input.MoveVertically.ReadValue<float>();
        this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

        var horiMove =  UserInput.Instance.input.MoveHorizontally.ReadValue<float>();
        this.transform.Translate(Vector3.right * speed * Time.deltaTime * horiMove);

        if (this.transform.position.y > Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
        }
        if (this.transform.position.y < -Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
        }
        if (this.transform.position.x > X_LIMIT_RIGHT)
        {
            this.transform.position = new Vector3(X_LIMIT_RIGHT, transform.position.y);
        }
        if (this.transform.position.x < X_LIMIT_LEFT)
        {
            this.transform.position = new Vector3(X_LIMIT_LEFT, transform.position.y);
        }

    }

    public void DamageFromEnemy()
    {
        if (!shield.IsActive)
        {
            health -= 0.25f;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

