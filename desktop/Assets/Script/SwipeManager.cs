using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;
    float distance;

    [Header("Audio Narasi")]
    public List<string> audioLocalizationKeys;

    public AudioSource audioSource;

    private int currentCardIndex = 0;


    void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        currentCardIndex = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                if (currentCardIndex != i)
                {
                    currentCardIndex = i;
                    if (audioSource != null && audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                }

                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);

                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }

    public void PlayCurrentAudio()
    {
        if (audioSource == null || LocalizationManager.instance == null || audioLocalizationKeys == null)
        {
            Debug.LogError("Setup belum lengkap! (AudioSource, LocalizationManager, atau Keys List hilang)");
            return;
        }
        if (currentCardIndex < 0 || currentCardIndex >= audioLocalizationKeys.Count)
        {
            Debug.LogError("currentCardIndex di luar jangkauan list keys.");
            return;
        }

        string key = audioLocalizationKeys[currentCardIndex];
        string clipName = LocalizationManager.instance.GetLocalizedValue(key);

        if (clipName == "KEY_NOT_FOUND")
        {
            Debug.LogError($"Key '{key}' tidak ditemukan di file bahasa Anda.");
            return;
        }

        string currentLanguageCode = LocalizationManager.instance.GetCurrentLanguage();
        string fullPath = "Audio/" + currentLanguageCode + "/" + clipName;
        AudioClip clipToPlay = Resources.Load<AudioClip>(fullPath);

        if (clipToPlay != null)
        {
            audioSource.Stop(); 
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"AudioClip GAGAL di-load. Cek path: 'Resources/{fullPath}'");
        }
    }
}