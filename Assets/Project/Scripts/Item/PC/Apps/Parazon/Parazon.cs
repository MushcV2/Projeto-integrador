using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private Button confirmItem;
    [SerializeField] private Button[] itemsButton;
    [SerializeField] private Transform deliveryPoint;
    [SerializeField] private List<GameObject> itemsCart;
    [SerializeField] private GameManager gameManager;
    private int destroyItemsDay;

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
        if (itemsCart.Count == 0)
        {
            Debug.Log("Sem itens");
            yield break;
        }

        destroyItemsDay = gameManager.days + 2;

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

    public void DestroyItems()
    {
        Debug.Log("Destruir items chamado");

        if (gameManager.days != destroyItemsDay)
        {
            Debug.Log("Ainda nao é o dia da destruição");
            return;
        }

        foreach (Transform _item in deliveryPoint.GetComponentsInParent<Transform>().Skip(1))
        {
            Destroy(_item.gameObject);
            Debug.Log($"{_item.gameObject.name} item destruido");
        }
    }
}