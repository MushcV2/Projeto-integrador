using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChangeButtonSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float size = 2;
    private RectTransform imageTransform;
    private Vector3 defaultScale;

    private void Start()
    {
        imageTransform = GetComponent<RectTransform>();
        defaultScale = imageTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageTransform.localScale = Vector3.Lerp(imageTransform.localScale, new Vector3(size, size, size), 10f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageTransform.localScale = Vector3.Lerp(imageTransform.localScale, defaultScale, 10f);
    }
}