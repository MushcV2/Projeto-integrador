using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private Button confirmItem;
    [SerializeField] private Button[] itemsButton;
    [SerializeField] public Transform deliveryPoint;
    [SerializeField] private List<GameObject> itemsCart;
    [SerializeField] private GameManager gameManager;
    public Transform waitingToDelivery;

    protected override void Start()
    {
        base.Start();

        confirmItem.onClick.AddListener(() => StartCoroutine(ConfirmBuy()));

        foreach (Button _button in itemsButton)
        {
            _button.onClick.AddListener(() => BuyItem(_button.GetComponent<ItemIndex>().index, _button.GetComponent<ItemIndex>().item));
        }

        FindObjectOfType<GameManager>().parazon = this;
        waitingToDelivery.gameObject.SetActive(false);
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

        Debug.Log("Confirmado");

        foreach (GameObject _item in itemsCart)
        {
            GameObject _object = Instantiate(_item, waitingToDelivery.position, Quaternion.identity);

            _object.GetComponent<Item>().dayToDestroy = gameManager.days + 2;
            _object.transform.parent = waitingToDelivery;

            yield return new WaitForSeconds(0.25f);

            Debug.Log("Item Comprado");
        }
        itemsCart.Clear();
    }

    public void DeliverItems()
    {
        foreach (Transform _item in waitingToDelivery.GetComponentsInChildren<Transform>().Skip(1))
            _item.parent = deliveryPoint;

        Debug.Log("Items entregues");
    }

    public void DestroyItems()
    {
        Debug.Log("Destruir items chamado");

        foreach (Transform _item in deliveryPoint.GetComponentsInChildren<Transform>().Skip(1))
        {
            if (_item.gameObject.GetComponent<Item>().dayToDestroy == gameManager.days)
            {
                Destroy(_item.gameObject);
                Debug.Log($"Item {_item.gameObject.name} destruido");
            }
        }
    }
}