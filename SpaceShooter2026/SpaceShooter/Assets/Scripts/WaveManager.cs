using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public GameObject sprinterPrefab;
    public GameObject shooterPrefab;
    public BoxCollider2D enemySpawnRange;

    public float normalSpawnDelay = 1.0f;
    public float breakBetweenWaves = 3.0f;

    public TextMeshProUGUI waveText;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveRunning = false;
    private bool breakRunning = false;
    private bool hasStarted = false;

    void Awake()
    {
        Instance = this;
    }

    public void BeginWaves()
    {
        if (hasStarted)
        {
            return;
        }

        hasStarted = true;

        StopAllCoroutines();

        currentWave = 0;
        enemiesAlive = 0;
        waveRunning = false;
        breakRunning = false;

        StartCoroutine(BeginNextWaveAfterBreak());
    }

    private IEnumerator BeginNextWaveAfterBreak()
    {
        if (breakRunning)
        {
            yield break;
        }

        breakRunning = true;

        if (waveText != null)
        {
            waveText.text = "Next Wave In...";
        }

        yield return new WaitForSeconds(breakBetweenWaves);

        breakRunning = false;
        StartCoroutine(RunWave());
    }

    private IEnumerator RunWave()
    {
        if (waveRunning)
        {
            yield break;
        }

        waveRunning = true;
        currentWave++;

        if (waveText != null)
        {
            waveText.text = "Wave " + currentWave;
        }

        int enemyCount = 5 + (currentWave - 1) * 3;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnNormalEnemy();
            enemiesAlive++;
            yield return new WaitForSeconds(normalSpawnDelay);
        }
    }

    private void SpawnNormalEnemy()
    {
        Vector3 spawnPoint = new Vector3(
            Random.Range(enemySpawnRange.bounds.min.x, enemySpawnRange.bounds.max.x),
            Random.Range(enemySpawnRange.bounds.min.y, enemySpawnRange.bounds.max.y),
            0f
        );

        GameObject prefabToSpawn = Random.Range(0, 2) == 0 ? sprinterPrefab : shooterPrefab;
        Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive < 0)
        {
            enemiesAlive = 0;
        }

        CheckWaveFinished();
    }

    private void CheckWaveFinished()
    {
        if (waveRunning && enemiesAlive == 0)
        {
            waveRunning = false;
            StartCoroutine(BeginNextWaveAfterBreak());
        }
    }
}