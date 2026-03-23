using UnityEngine;

public class Game : MonoBehaviour
{
    public BoxCollider2D enemySpawnRange;
    public GameObject sprinterPrefab;
    public GameObject shooterPrefab;
    public float enemySpawnDelay;

    private float enemySpawnTimer;
    private int enemySpawnType;

    private void SpawnEnemy()
    {
        int enemySpawnType = Random.Range(1, 3);
        Vector3 enemySpawnPoint = new Vector3(
            Random.Range(enemySpawnRange.bounds.min.x, enemySpawnRange.bounds.max.x),
            Random.Range(enemySpawnRange.bounds.min.y, enemySpawnRange.bounds.max.y),
            0);
        if (enemySpawnType == 1)
        {
            Instantiate(sprinterPrefab, enemySpawnPoint, Quaternion.identity);
        }
        else {
            Instantiate(shooterPrefab, enemySpawnPoint, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnDelay )
        {
            SpawnEnemy();
            enemySpawnTimer = 0.0f;
        }
    }
}
