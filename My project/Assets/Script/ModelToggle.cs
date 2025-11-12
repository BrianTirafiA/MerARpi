using UnityEngine;
using UnityEngine.UI; // Jika Anda ingin ganti teks tombol
using TMPro; // Jika Anda pakai TextMeshPro

public class ModelToggle : MonoBehaviour
{
    public GameObject view2D;
    public GameObject view3D;
    public TextMeshProUGUI buttonText; // Ganti ke Text jika pakai UI Text biasa

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