using UnityEngine;

public class WebButton : MonoBehaviour
{
    public string url;

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }
}