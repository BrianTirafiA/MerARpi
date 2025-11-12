using UnityEngine;
using System.Collections.Generic;

public class ManagerInfoUI : MonoBehaviour
{
    [Header("Tombol UI")]
    public GameObject tombolInformasi;
    public GameObject tombolTutup;

    [Header("Pemetaan Marker")]
    public List<PemetaanInfoMarker> daftarPemetaan;

    [System.Serializable]
    public class PemetaanInfoMarker
    {
        public string namaMarker;
        public GameObject panelInfo;
        public GameObject modelStudio;
    }

    // --- VARIABEL BARU UNTUK MELACAK STATUS ---
    private Dictionary<string, GameObject> petaInfo = new Dictionary<string, GameObject>();
    private GameObject panelAktifSaatIni = null;
    private Animator animatorPanelAktif = null;
    private string activeMarkerName = null;
    private bool isTargetCurrentlyTracked = false;
    private GameObject studioAktifSaatIni = null;


    void Awake()
    {
        foreach (var pemetaan in daftarPemetaan)
        {
            if (!petaInfo.ContainsKey(pemetaan.namaMarker))
            {
                petaInfo.Add(pemetaan.namaMarker, pemetaan.panelInfo);
            }
        }

        if (tombolInformasi != null)
        {
            tombolInformasi.SetActive(false);
        }
        if (tombolTutup != null)
        {
            tombolTutup.SetActive(false);
        }
    }

    // --- FUNGSI UNTUK VUFORIA (DIPERBARUI) ---

    // Fungsi ini dipanggil oleh TargetManager saat DITEMUKAN
    public void SetMarkerAktif(string namaMarker)
    {
        // 1. Selalu perbarui status pelacakan
        isTargetCurrentlyTracked = true;
        activeMarkerName = namaMarker; // Simpan nama marker terbaru yang terlihat

        if (petaInfo.TryGetValue(namaMarker, out GameObject panelTerkait))
        {
            panelAktifSaatIni = panelTerkait;
            animatorPanelAktif = panelAktifSaatIni.GetComponent<Animator>();

            // 2. Hanya tampilkan tombol "Informasi" jika panelnya BELUM terbuka
            if (!panelAktifSaatIni.activeSelf)
            {
                if (tombolInformasi != null) tombolInformasi.SetActive(true);
                if (tombolTutup != null) tombolTutup.SetActive(false);
            }
        }
    }

    // Fungsi ini dipanggil oleh TargetManager saat HILANG
    public void HapusMarkerAktif(string namaMarker)
    {
        // 3. Hanya proses jika marker yang hilang adalah marker yang sedang kita lacak
        if (activeMarkerName == namaMarker)
        {
            // Set status bahwa marker sudah tidak terlihat
            isTargetCurrentlyTracked = false;

            // 4. Sembunyikan tombol "Informasi" HANYA JIKA panel tidak sedang terbuka
            // (Jika panel terbuka, tombol "Tutup" yang terlihat, jadi biarkan saja)
            if (tombolInformasi != null && tombolInformasi.activeSelf)
            {
                tombolInformasi.SetActive(false);
            }
        }
    }

    // --- FUNGSI UNTUK TOMBOL (DIPERBARUI) ---

    // Fungsi ini TIDAK BERUBAH
    public void OnTombolInformasiKlik()
    {
        if (panelAktifSaatIni != null && !panelAktifSaatIni.activeSelf)
        {
            // Tampilkan panel
            panelAktifSaatIni.SetActive(true);
            if (animatorPanelAktif != null)
            {
                animatorPanelAktif.SetTrigger("Muncul");
            }

            foreach (var pemetaan in daftarPemetaan)
            {
                if (pemetaan.panelInfo == panelAktifSaatIni)
                {
                    studioAktifSaatIni = pemetaan.modelStudio;
                    if (studioAktifSaatIni != null)
                    {
                        studioAktifSaatIni.SetActive(true);
                    }
                    break; // Keluar dari loop
                }
            }

            // Tukar tombol
            if (tombolInformasi != null)
            {
                tombolInformasi.SetActive(false);
            }
            if (tombolTutup != null)
            {
                tombolTutup.SetActive(true);
            }
        }
    }

    // Fungsi ini DIPERBARUI dengan logika baru Anda
    public void OnTombolTutupKlik()
    {
        // 1. Sembunyikan panel (picu animasi)
        if (panelAktifSaatIni != null && panelAktifSaatIni.activeSelf)
        {
            if (animatorPanelAktif != null)
            {
                // Skrip InfoPanelHelper Anda akan menonaktifkan panel setelah animasi
                animatorPanelAktif.SetTrigger("Sembunyi");
            }
            else
            {
                // Fallback jika tidak ada animator
                panelAktifSaatIni.SetActive(false);
            }
        }

        if (studioAktifSaatIni != null)
        {
            studioAktifSaatIni.SetActive(false);
            studioAktifSaatIni = null;
        }

        // 2. Sembunyikan tombol "Tutup"
        if (tombolTutup != null)
        {
            tombolTutup.SetActive(false);
        }

        // --- 3. LOGIKA BARU ANDA ---
        // Cek apakah target masih dilacak oleh Vuforia
        if (isTargetCurrentlyTracked)
        {
            // Jika ya, tampilkan kembali tombol "Informasi"
            if (tombolInformasi != null)
            {
                tombolInformasi.SetActive(true);
            }
        }
    }
}