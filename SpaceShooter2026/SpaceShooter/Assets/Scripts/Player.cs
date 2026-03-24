using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    public float maxHealth = 1f;
    public float health = 1f;
    public Slider healthBar;

    public Shield shield;
    public Transform firePoint;

    public GameObject normalBulletPrefab;
    public GameObject machineGunBulletPrefab;
    public GameObject bigBulletPrefab;

    public float normalFireRate = 0.35f;
    public float machineGunFireRate = 0.12f;
    public float bigBulletFireRate = 0.7f;

    private float fireTimer = 0f;

    private const float Y_LIMIT = 4.6f;
    private const float X_LIMIT_LEFT = -0.1f;
    private const float X_LIMIT_RIGHT = 10f;

    public enum WeaponType
    {
        Normal,
        MachineGun,
        BigBullet
    }

    public WeaponType currentWeapon = WeaponType.Normal;

    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        var vertMove = UserInput.Instance.input.MoveVertically.ReadValue<float>();
        transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

        var horiMove = UserInput.Instance.input.MoveHorizontally.ReadValue<float>();
        transform.Translate(Vector3.right * speed * Time.deltaTime * horiMove);

        if (transform.position.y > Y_LIMIT)
        {
            transform.position = new Vector3(transform.position.x, Y_LIMIT, transform.position.z);
        }
        if (transform.position.y < -Y_LIMIT)
        {
            transform.position = new Vector3(transform.position.x, -Y_LIMIT, transform.position.z);
        }
        if (transform.position.x > X_LIMIT_RIGHT)
        {
            transform.position = new Vector3(X_LIMIT_RIGHT, transform.position.y, transform.position.z);
        }
        if (transform.position.x < X_LIMIT_LEFT)
        {
            transform.position = new Vector3(X_LIMIT_LEFT, transform.position.y, transform.position.z);
        }
    }

    private void HandleShooting()
    {
        fireTimer -= Time.deltaTime;

        if (UserInput.Instance.input.Fire.IsPressed() && fireTimer <= 0f)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletToSpawn = normalBulletPrefab;
        float nextFireDelay = normalFireRate;

        switch (currentWeapon)
        {
            case WeaponType.MachineGun:
                bulletToSpawn = machineGunBulletPrefab;
                nextFireDelay = machineGunFireRate;
                break;

            case WeaponType.BigBullet:
                bulletToSpawn = bigBulletPrefab;
                nextFireDelay = bigBulletFireRate;
                break;
        }

        if (bulletToSpawn != null && firePoint != null)
        {
            Instantiate(bulletToSpawn, firePoint.position, Quaternion.identity);
        }

        fireTimer = nextFireDelay;
    }

    public void DamageFromEnemy()
    {
        if (shield != null && shield.IsActive)
        {
            return;
        }

        health -= 0.25f;

        if (health < 0f)
        {
            health = 0f;
        }

        UpdateHealthBar();

        if (health <= 0f)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }

            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = health / maxHealth;
        }
    }

    public void ActivateShield()
    {
        if (shield != null)
        {
            shield.RefillShield();
        }
    }

    public void EnableMachineGun()
    {
        currentWeapon = WeaponType.MachineGun;
    }

    public void EnableBigBullet()
    {
        currentWeapon = WeaponType.BigBullet;
    }

    public void ResetWeapon()
    {
        currentWeapon = WeaponType.Normal;
    }
}