using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject[] _powerupPrefabs;

    private GameArea _gameArea;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameArea = FindObjectOfType<GameArea>();

        _player = SpawnPlayer();
    }

    private GameObject SpawnPlayer()
    {
        return Instantiate(_playerPrefab, _playerPrefab.transform.position, _playerPrefab.transform.rotation);
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
        if (_player == null)
        {
            return null;
        }

        return Instantiate(_enemyPrefab, _gameArea.GetRandomPosition(0.5f, _player.transform.position, 6.0f), _enemyPrefab.transform.rotation);
    }

    public GameObject SpawnBoss()
    {
        if (_player == null)
        {
            return null;
        }

        return Instantiate(_bossPrefab, _gameArea.GetRandomPosition(0.5f, _player.transform.position, 6.0f), _bossPrefab.transform.rotation);
    }

    public void StartSpawnPowerups()
    {
        StartCoroutine(SpawnPowerups());
    }

    public void StopSpawnPowerups()
    {
        StopCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnPowerups()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 10.0f));

            SpawnPowerup();
        }
    }

    public GameObject SpawnPowerup()
    {
        if (_player == null)
        {
            return null;
        }

        var powerupPrefab = _powerupPrefabs[Random.Range(0, _powerupPrefabs.Length)];
        return Instantiate(powerupPrefab, _gameArea.GetRandomPosition(0.5f, _player.transform.position, 6.0f), powerupPrefab.transform.rotation);
    }
}
