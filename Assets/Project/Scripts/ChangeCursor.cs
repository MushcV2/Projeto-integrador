using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private Image crossHairImage;
    [SerializeField] private float newScale;
    private Sprite defaultImage;
    private Vector3 defaultScale;

    private void Start()
    {
        defaultImage = crossHairImage.sprite;
        defaultScale = crossHairImage.transform.localScale;
    }

    private void OnMouseEnter()
    {

        crossHairImage.transform.localScale = new Vector3(newScale, newScale, newScale);
        crossHairImage.sprite = cursorSprite;
    }

    private void OnMouseExit()
    {
        crossHairImage.transform.localScale = defaultScale;
        crossHairImage.sprite = defaultImage;
    }
}