using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    private GameArea gameArea;
    private GameObject player;

    private float endOfWave = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameArea = FindObjectOfType<GameArea>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, gameArea.GetRandomPosition(0.5f, player.transform.position, 6.0f), enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));

            if (Time.time >= endOfWave)
            {
                StopCoroutine(SpawnEnemy());
                Instantiate(bossPrefab, gameArea.GetRandomPosition(0.5f, player.transform.position, 6.0f), bossPrefab.transform.rotation);
                break;
            }
        }
    }
}
