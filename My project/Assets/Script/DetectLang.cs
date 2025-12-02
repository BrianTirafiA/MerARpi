using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))] 
public class LocalizedText : MonoBehaviour
{
    public string localizationKey;

    private TextMeshProUGUI textComponent;

    void OnEnable()
    {
        LocalizationManager.OnLanguageChanged += UpdateText;
    }

    void OnDisable()
    {
        LocalizationManager.OnLanguageChanged -= UpdateText;
    }

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
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