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

        Vector3 _localPos = backgroundSize.InverseTransformPoint(_worldPos);
        Vector3 _minBounds = backgroundSize.rect.min;
        Vector3 _maxBounds = backgroundSize.rect.max;

        _localPos.x = Mathf.Clamp(_localPos.x, _minBounds.x + rectTransform.rect.width / 2, _maxBounds.x - rectTransform.rect.width / 2);
        _localPos.y = Mathf.Clamp(_localPos.y, _minBounds.y + rectTransform.rect.height / 2, _maxBounds.y - rectTransform.rect.height / 2);

        rectTransform.position = backgroundSize.TransformPoint(_localPos);
    }
}