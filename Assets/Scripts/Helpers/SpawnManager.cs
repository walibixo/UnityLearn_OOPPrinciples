using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    private GameArea gameArea;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameArea = FindObjectOfType<GameArea>();

        player = SpawnPlayer();
    }

    private GameObject SpawnPlayer()
    {
        return Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
    }

    public void StartSpawnEnemies()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawnEnemies()
    {
        StopCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            SpawnEnemy();
        }
    }

    private GameObject SpawnEnemy()
    {
        return Instantiate(enemyPrefab, gameArea.GetRandomPosition(0.5f, player.transform.position, 6.0f), enemyPrefab.transform.rotation);
    }

    public void SpawnBoss()
    {
        Instantiate(bossPrefab, gameArea.GetRandomPosition(0.5f, player.transform.position, 6.0f), bossPrefab.transform.rotation);
    }
}
