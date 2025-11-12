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

    // --- PERUBAHAN DI SINI ---
    [Header("Audio Narasi")]
    // 1. GANTI List<AudioClip> menjadi List<string>
    // Ini akan menampung KUNCI LOKALISASI, bukan file audio
    public List<string> audioLocalizationKeys;

    // 2. Referensi ke AudioSource (tetap sama)
    public AudioSource audioSource;

    // 3. Index kartu aktif (tetap sama)
    private int currentCardIndex = 0;
    // --- AKHIR PERUBAHAN ---


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
        // Logika swipe/scroll Anda tetap sama, tidak perlu diubah
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
                // Saat kartu berubah, hentikan audio yang sedang diputar
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

    // --- PERUBAHAN BESAR DI SINI ---
    // Fungsi ini sekarang akan mencari nama file audio berdasarkan bahasa
    // dan me-loadnya dari folder Resources.
    public void PlayCurrentAudio()
    {
        // 1. Cek apakah semua sistem siap
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

        // 2. Dapatkan KUNCI lokalisasi untuk halaman saat ini
        string key = audioLocalizationKeys[currentCardIndex];

        // 3. Dapatkan NAMA FILE dari LocalizationManager
        string clipName = LocalizationManager.instance.GetLocalizedValue(key);

        if (clipName == "KEY_NOT_FOUND")
        {
            Debug.LogError($"Key '{key}' tidak ditemukan di file bahasa Anda.");
            return;
        }

        // --- INI BAGIAN YANG DIPERBARUI ---

        // 4. Dapatkan KODE BAHASA saat ini (misal: "id" atau "en")
        string currentLanguageCode = LocalizationManager.instance.GetCurrentLanguage();

        // 5. Gabungkan path lengkapnya
        // Contoh path akan menjadi: "Audio/id/narasi_markerA_hal1_id"
        string fullPath = "Audio/" + currentLanguageCode + "/" + clipName;

        // 6. Load AudioClip dari path yang baru
        AudioClip clipToPlay = Resources.Load<AudioClip>(fullPath);

        // --- AKHIR BAGIAN YANG DIPERBARUI ---

        // 7. Mainkan klip
        if (clipToPlay != null)
        {
            audioSource.Stop(); // Hentikan klip sebelumnya
            audioSource.clip = clipToPlay; // Set klip baru
            audioSource.Play(); // Mainkan
        }
        else
        {
            // Pesan error ini sekarang lebih jelas
            Debug.LogError($"AudioClip GAGAL di-load. Cek path: 'Resources/{fullPath}'");
        }
    }
    // --- AKHIR PERUBAHAN ---
}