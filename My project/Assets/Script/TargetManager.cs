using UnityEngine;
using Vuforia; // Pastikan ini ada!

public class TargetManager : MonoBehaviour
{
    // 1. Beri nama unik untuk setiap marker di Inspector
    public string namaMarker;

    // Referensi ke ManajerUI
    private ManagerInfoUI manajerUI;

    // Referensi ke Observer Behaviour
    private ObserverBehaviour mObserverBehaviour;

    // Variabel untuk melacak status, agar fungsi tidak dipanggil setiap frame
    private bool isTargetVisible = false;

    void Start()
    {
        // Cari ManajerUI di scene Anda secara otomatis
        manajerUI = FindObjectOfType<ManagerInfoUI>();

        // Dapatkan komponen ObserverBehaviour dari GameObject ini
        mObserverBehaviour = GetComponent<ObserverBehaviour>();

        if (manajerUI == null)
        {
            Debug.LogError("TargetManager tidak dapat menemukan ManajerUI di scene!");
        }
        if (mObserverBehaviour == null)
        {
            Debug.LogError("TargetManager tidak dapat menemukan ObserverBehaviour!");
        }
    }

    void Update()
    {
        if (mObserverBehaviour == null || manajerUI == null) return;

        // Periksa status marker saat ini
        if (mObserverBehaviour.TargetStatus.Status == Status.TRACKED ||
            mObserverBehaviour.TargetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // --- MARKER DITEMUKAN ---

            // Jika sebelumnya tidak terlihat, sekarang terlihat
            if (!isTargetVisible)
            {
                isTargetVisible = true;
                manajerUI.SetMarkerAktif(namaMarker);
            }
        }
        else
        {
            // --- MARKER HILANG ---
            // (Status.NO_POSE, Status.LIMITED, dll)

            // Jika sebelumnya terlihat, sekarang hilang
            if (isTargetVisible)
            {
                isTargetVisible = false;
                manajerUI.HapusMarkerAktif(namaMarker);
            }
        }
    }
}