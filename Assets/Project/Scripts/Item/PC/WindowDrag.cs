using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform backgroundSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 _mousePos = Input.mousePosition;
        Vector3 _worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(backgroundSize, _mousePos, Camera.main, out _worldPos);

        Vector3 localPosition = backgroundSize.InverseTransformPoint(_worldPos);
        Vector3 minBounds = backgroundSize.rect.min;
        Vector3 maxBounds = backgroundSize.rect.max;

        localPosition.x = Mathf.Clamp(localPosition.x, minBounds.x + rectTransform.rect.width / 2, maxBounds.x - rectTransform.rect.width / 2);
        localPosition.y = Mathf.Clamp(localPosition.y, minBounds.y + rectTransform.rect.height / 2, maxBounds.y - rectTransform.rect.height / 2);

        rectTransform.position = backgroundSize.TransformPoint(localPosition);
    }
}