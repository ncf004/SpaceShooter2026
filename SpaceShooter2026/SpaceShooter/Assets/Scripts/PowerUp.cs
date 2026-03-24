using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Shield,
        MachineGun,
        BigBullet
    }

    public PowerUpType type;
    public float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
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
                ApplyEffect(p);
            }

            Destroy(gameObject);
        }

        if (c.CompareTag("ScreenOutOfBounds"))
        {
            Destroy(gameObject);
        }
    }

    void ApplyEffect(Player player)
    {
        switch (type)
        {
            case PowerUpType.Shield:
                player.ActivateShield();
                break;

            case PowerUpType.MachineGun:
                player.EnableMachineGun();
                break;

            case PowerUpType.BigBullet:
                player.EnableBigBullet();
                break;
        }
    }
}