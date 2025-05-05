using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    public float spawnInterval = 2f;
    public Transform playerTransform;

    private float timer;

    void Update()
    {
        if (playerTransform == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[index];

        int side = Random.Range(0, 2); // 0 = izquierda, 1 = derecha
        Transform spawnPoint = side == 0 ? leftSpawnPoint : rightSpawnPoint;

        Vector3 spawnPosition = spawnPoint.position;

        GameObject enemyInstance = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        




        // 🔥 Le asignamos el player al enemigo recién creado
        Enemy enemyScript = enemyInstance.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.player = playerTransform;
        }
    }
}
