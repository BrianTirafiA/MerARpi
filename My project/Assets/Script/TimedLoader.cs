using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedLoader : MonoBehaviour
{
    public string sceneToLoad;
    public float delayInSeconds = 5f;
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneToLoad);
    }
}