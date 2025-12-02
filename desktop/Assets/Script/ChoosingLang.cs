using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static System.TimeZoneInfo;

public class ChoosingLang : MonoBehaviour
{
    public string Load1;
    public string Load2;
    public string Indonesia_id;
    public string English_id;
    public Animator pageAnimator;
    public float transitionTime = 2f;
    private string sceneToLoad;
    private string languageToLoad;

    public void Load1Scene()
    {
        languageToLoad = Indonesia_id;
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.SetLanguage(languageToLoad);
        }
        else
        {
            Debug.LogError("LocalizationManager instance not found!");
        }
        sceneToLoad = Load1;
        StartCoroutine(LoadSceneAfterAnimation());
    }
    public void Load2Scene()
    {
        languageToLoad = English_id;
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.SetLanguage(languageToLoad);
        }
        else
        {
            Debug.LogError("LocalizationManager instance not found!");
        }
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