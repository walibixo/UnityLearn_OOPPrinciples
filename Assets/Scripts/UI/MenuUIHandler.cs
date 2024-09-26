using UnityEditor;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    private void Awake()
    {
        _sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame()
    {
        _sceneTransition.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
