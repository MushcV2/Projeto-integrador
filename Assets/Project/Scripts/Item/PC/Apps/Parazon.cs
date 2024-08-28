using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private Button confirmItem;
    [SerializeField] private List<GameObject> items;

    private void Start()
    {
        confirmItem.onClick.AddListener(ConfirmBuy);
    }

    private void ConfirmBuy()
    {

    }
}