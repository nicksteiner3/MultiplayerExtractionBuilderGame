using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoubleClickDetector : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    private System.Action onDoubleClick;

    public void SetDoubleClickCallback(System.Action callback)
    {
        onDoubleClick = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick < doubleClickThreshold)
        {
            // Double click detected
            onDoubleClick?.Invoke();
            lastClickTime = 0f;
        }
        else
        {
            // First click of potential double click
            lastClickTime = Time.time;
        }
    }
}
