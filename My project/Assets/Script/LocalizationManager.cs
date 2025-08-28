using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private Dictionary<string, string> localizedText;
    private string currentLanguage = "en";
    private const string LanguagePlayerPrefsKey = "SelectedLanguage";
    public static System.Action OnLanguageChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        currentLanguage = PlayerPrefs.GetString(LanguagePlayerPrefsKey, "en");
        LoadLocalizedText(currentLanguage);
    }

    public void LoadLocalizedText(string languageCode)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = "Languages/" + languageCode;
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        if (targetFile != null)
        {
            string dataAsJson = targetFile.text;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Localization data loaded for language: " + languageCode);
        }
        else
        {
            Debug.LogError("Cannot find language file: " + filePath);
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        return "KEY_NOT_FOUND";
    }

    public void SetLanguage(string languageCode)
    {
        currentLanguage = languageCode;
        PlayerPrefs.SetString(LanguagePlayerPrefsKey, currentLanguage);
        PlayerPrefs.Save();
        LoadLocalizedText(currentLanguage);

        OnLanguageChanged?.Invoke();
    }

    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}
