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

    public void SetMarkerAktif(string namaMarker)
    {
        isTargetCurrentlyTracked = true;
        activeMarkerName = namaMarker;

        if (petaInfo.TryGetValue(namaMarker, out GameObject panelTerkait))
        {
            panelAktifSaatIni = panelTerkait;
            animatorPanelAktif = panelAktifSaatIni.GetComponent<Animator>();

            if (!panelAktifSaatIni.activeSelf)
            {
                if (tombolInformasi != null) tombolInformasi.SetActive(true);
                if (tombolTutup != null) tombolTutup.SetActive(false);
            }
        }
    }

    public void HapusMarkerAktif(string namaMarker)
    {
        if (activeMarkerName == namaMarker)
        {
            isTargetCurrentlyTracked = false;

            if (tombolInformasi != null && tombolInformasi.activeSelf)
            {
                tombolInformasi.SetActive(false);
            }
        }
    }

    public void OnTombolInformasiKlik()
    {
        if (panelAktifSaatIni != null && !panelAktifSaatIni.activeSelf)
        {
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
                    break;
                }
            }

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

    public void OnTombolTutupKlik()
    {
        if (panelAktifSaatIni != null && panelAktifSaatIni.activeSelf)
        {
            if (animatorPanelAktif != null)
            {
                animatorPanelAktif.SetTrigger("Sembunyi");
            }
            else
            {
                panelAktifSaatIni.SetActive(false);
            }
        }

        if (studioAktifSaatIni != null)
        {
            studioAktifSaatIni.SetActive(false);
            studioAktifSaatIni = null;
        }

        if (tombolTutup != null)
        {
            tombolTutup.SetActive(false);
        }

        if (isTargetCurrentlyTracked)
        {
            if (tombolInformasi != null)
            {
                tombolInformasi.SetActive(true);
            }
        }
    }
}