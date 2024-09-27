using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private SceneTransition _sceneTransition;

    [SerializeField]
    private TextMeshProUGUI _counterText;
    [SerializeField]
    private GameObject _winScreen;

    private float _endOfWave = 60.0f;
    private bool _waveEnded = false;

    private GameObject _boss;

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
        if (IsGameOver)
        {
            return;
        }

        if (_waveEnded)
        {
            if (_boss == null)
            {
                _winScreen.SetActive(true);
                IsGameOver = true;
            }

            return;
        }

        _endOfWave -= Time.deltaTime;

        if (_endOfWave <= 0)
        {
            _waveEnded = true;

            _spawnManager.StopSpawnEnemies();
            _boss = _spawnManager.SpawnBoss();

            _counterText.text = "SURVIVE: (è>é)";
        }
        else
        {
            _counterText.text = $"SURVIVE: {_endOfWave:0}s";
        }
    }

    public void GameOver()
    {
        IsGameOver = true;

        // Reload the current scene
        _sceneTransition.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
