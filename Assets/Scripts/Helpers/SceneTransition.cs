using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private Animator _transition;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
