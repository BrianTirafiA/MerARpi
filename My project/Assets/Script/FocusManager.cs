using UnityEngine;
using Vuforia;

public class FocusManager : MonoBehaviour
{
    void Start()
    {
        if (VuforiaApplication.Instance != null)
            VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
    }

    private void OnVuforiaStarted()
    {
        var device = VuforiaBehaviour.Instance?.CameraDevice;

        if (device == null)
        {
            Debug.LogWarning("CameraDevice is NULL.");
            return;
        }

        bool success = device.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        Debug.Log(success
            ? "Autofocus set to ContinuousAuto"
            : "Failed to set ContinuousAuto focus");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var device = VuforiaBehaviour.Instance?.CameraDevice;
            if (device != null)
            {
                device.SetFocusMode(FocusMode.FOCUS_MODE_TRIGGERAUTO);
                Debug.Log("TriggerAuto focus activated");
            }
        }
    }

    void OnDestroy()
    {
        if (VuforiaApplication.Instance != null)
            VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
    }
}
