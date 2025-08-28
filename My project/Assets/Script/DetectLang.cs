using UnityEngine;
using TMPro; // Use this for TextMeshPro

// Remove the line below if you are using TextMeshPro
// using UnityEngine.UI; 

[RequireComponent(typeof(TextMeshProUGUI))] // Change this to TextMeshProUGUI
public class LocalizedText : MonoBehaviour
{
    // The key to look for in the language file
    public string localizationKey;

    private TextMeshProUGUI textComponent; // Change this to TextMeshProUGUI

    void OnEnable()
    {
        // Subscribe to the language changed event
        LocalizationManager.OnLanguageChanged += UpdateText;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        LocalizationManager.OnLanguageChanged -= UpdateText;
    }

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>(); // Change this to TextMeshProUGUI
        UpdateText();
    }

    void UpdateText()
    {
        if (textComponent != null && LocalizationManager.instance != null)
        {
            textComponent.text = LocalizationManager.instance.GetLocalizedValue(localizationKey);
        }
    }
}