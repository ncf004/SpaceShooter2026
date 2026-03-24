using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public static PowerUpSpawner Instance;

    public GameObject shieldPrefab;
    public GameObject machineGunPrefab;
    public GameObject bigBulletPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void TrySpawnPowerUp(Vector3 spawnPosition)
    {
        float dropRoll = Random.value;

        if (dropRoll < 0.25f)
        {
            float typeRoll = Random.value;

            if (typeRoll < 0.5f)
            {
                if (shieldPrefab != null)
                {
                    Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);
                }
            }
            else if (typeRoll < 0.75f)
            {
                if (machineGunPrefab != null)
                {
                    Instantiate(machineGunPrefab, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                if (bigBulletPrefab != null)
                {
                    Instantiate(bigBulletPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}