using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private GameArea gameArea;

    // Start is called before the first frame update
    void Start()
    {
        gameArea = FindObjectOfType<GameArea>();

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, gameArea.GetRandomPosition(0.5f), enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
}
