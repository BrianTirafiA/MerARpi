using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModelViewer : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform modelTransform;
    public Camera modelCamera;
    public ScrollRect parentScrollRect;

    [Header("Pengaturan")]
    public float rotationSpeed = 0.5f;
    public float zoomSpeed = 0.5f;
    public float minZoomFov = 20f;
    public float maxZoomFov = 60f;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.05f); 
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Zoom(scroll * zoomSpeed * 10f);
    }

    void Zoom(float delta)
    {
        if (modelCamera == null) return;
        modelCamera.fieldOfView = Mathf.Clamp(modelCamera.fieldOfView - (delta * zoomSpeed), minZoomFov, maxZoomFov);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (modelTransform == null) return;
        modelTransform.Rotate(Vector3.up, -eventData.delta.x * rotationSpeed, Space.World);
        modelTransform.Rotate(Vector3.right, eventData.delta.y * rotationSpeed, Space.World);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (parentScrollRect != null)
        {
            parentScrollRect.enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (parentScrollRect != null)
        {
            parentScrollRect.enabled = true;
        }
    }
}