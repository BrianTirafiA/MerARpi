using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoader : MonoBehaviour
{
    public string Load1;
    public string Load2;
    public void Load1Scene()
    {
        SceneManager.LoadScene(Load1);
    }
    public void Load2Scene()
    {
        SceneManager.LoadScene(Load2);
    }
}