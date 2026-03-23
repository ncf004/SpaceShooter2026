using UnityEngine;

public class Game : MonoBehaviour
{
    public BoxCollider2D enemySpawnRange;
    public GameObject enemyPrefab;
    public float enemySpawnDelay;

    private float enemySpawnTimer;

    private void SpawnEnemy()
    {
        Vector3 enemySpawnPoint = new Vector3(
            Random.Range(enemySpawnRange.bounds.min.x, enemySpawnRange.bounds.max.x),
            Random.Range(enemySpawnRange.bounds.min.y, enemySpawnRange.bounds.max.y),
            0);
        Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
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
