using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;

    private float endOfWave = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

        spawnManager.StartSpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= endOfWave)
        {
            spawnManager.SpawnBoss();
            endOfWave = float.MaxValue;
        }
    }
}
