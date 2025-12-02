using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelToggle : MonoBehaviour
{
    public GameObject view2D;
    public GameObject view3D;
    public TextMeshProUGUI buttonText;

    private bool is3DActive = false;

    public void ToggleView()
    {
        is3DActive = !is3DActive;

        view2D.SetActive(!is3DActive);
        view3D.SetActive(is3DActive);

        if (buttonText != null)
        {
            buttonText.text = is3DActive ? "2D" : "3D";
        }
    }
}