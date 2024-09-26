using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private SceneTransition _sceneTransition;

    [SerializeField]
    private float _endOfWave = 60.0f;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _sceneTransition = FindObjectOfType<SceneTransition>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager.StartSpawnEnemies();
        _spawnManager.StartSpawnPowerups();
    }

    // Update is called once per frame
    void Update()
    {
        _endOfWave -= Time.deltaTime;

        if (_endOfWave <= 0)
        {
            _spawnManager.StopSpawnEnemies();
            _spawnManager.SpawnBoss();
            _endOfWave = float.MaxValue;
        }
    }

    public void GameOver()
    {
        IsGameOver = true;

        // Reload the current scene
        _sceneTransition.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
