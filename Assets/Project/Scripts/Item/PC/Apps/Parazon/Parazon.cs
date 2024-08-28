using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private Button confirmItem;
    [SerializeField] private Button[] itemsButton;
    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private List<GameObject> itemsCart;

    protected override void Start()
    {
        base.Start();

        confirmItem.onClick.AddListener(ConfirmBuy);
        deliveryPoint = GameObject.FindGameObjectWithTag("ItemsDelivery").transform;

        foreach (Button _button in itemsButton)
        {
            _button.onClick.AddListener(() => BuyItem(_button.GetComponent<ItemIndex>().index, _button.GetComponent<ItemIndex>().item));
        }
    }

    private void BuyItem(int _index, GameObject _itemOBJ)
    {
        Debug.Log("O item comprado tem o index de: " +  _index);
        itemsCart.Add(_itemOBJ);
    }

    private void ConfirmBuy()
    {
        if (itemsCart.Count == 0) return;

        Debug.Log("Confirmado");

        foreach (GameObject _item in itemsCart)
        {
            Instantiate(_item, deliveryPoint.position, Quaternion.identity);
            Debug.Log("Item Comprado");
        }
        itemsCart.Clear();
    }
}