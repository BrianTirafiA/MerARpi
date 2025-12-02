using UnityEngine;
using Vuforia;

public class TargetManager : MonoBehaviour
{
    public string namaMarker;
    private ManagerInfoUI manajerUI;
    private ObserverBehaviour mObserverBehaviour;
    private bool isTargetVisible = false;

    void Start()
    {
        manajerUI = FindObjectOfType<ManagerInfoUI>();
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
        if (mObserverBehaviour.TargetStatus.Status == Status.TRACKED ||
            mObserverBehaviour.TargetStatus.Status == Status.EXTENDED_TRACKED)
        {
            if (!isTargetVisible)
            {
                isTargetVisible = true;
                manajerUI.SetMarkerAktif(namaMarker);
            }
        }
        else
        {
            if (isTargetVisible)
            {
                isTargetVisible = false;
                manajerUI.HapusMarkerAktif(namaMarker);
            }
        }
    }
}