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

        confirmItem.onClick.AddListener(() => StartCoroutine(ConfirmBuy()));

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

    private IEnumerator ConfirmBuy()
    {
        if (itemsCart.Count == 0) StopCoroutine(ConfirmBuy());

        Debug.Log("Confirmado");

        foreach (GameObject _item in itemsCart)
        {
            GameObject _object = Instantiate(_item, deliveryPoint.position, Quaternion.identity);
            _object.transform.parent = deliveryPoint.transform;

            yield return new WaitForSeconds(0.25f);

            Debug.Log("Item Comprado");
        }
        itemsCart.Clear();
    }
}