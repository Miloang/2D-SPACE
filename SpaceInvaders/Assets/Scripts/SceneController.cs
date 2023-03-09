using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string scene) => SceneManager.LoadScene(scene);

    public void ChangeScene(string scene, float delay)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeSceneDelay(scene, delay));
    }

    IEnumerator ChangeSceneDelay(string scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeScene(scene);
    }
}