using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static System.TimeZoneInfo;

public class ButtonLoader : MonoBehaviour
{
    public string Load1;
    public string Load2;
    public Animator pageAnimator;
    public float transitionTime = 2f;
    private string sceneToLoad;
    public void Load1Scene()
    {
        sceneToLoad = Load1;
        StartCoroutine(LoadSceneAfterAnimation());
    }
    public void Load2Scene()
    {
        sceneToLoad = Load2;
        StartCoroutine(LoadSceneAfterAnimation());
    }
    IEnumerator LoadSceneAfterAnimation()
    {
        pageAnimator.SetTrigger("Next");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}