using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;

    [SerializeField]
    private float _endOfWave = 60.0f;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager.StartSpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _endOfWave)
        {
            _spawnManager.SpawnBoss();
            _endOfWave = float.MaxValue;
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
}
