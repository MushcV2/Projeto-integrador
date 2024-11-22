using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Button confirmItem;
    [SerializeField] private Button[] itemsButton;
    [SerializeField] private TMP_Text cartCountTXT;
    [SerializeField] public Transform deliveryPoint;
    [SerializeField] private List<GameObject> itemsCart;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject popUsable;
    [SerializeField] private SanityController sanityController;
    public Transform waitingToDelivery;

    protected override void Start()
    {
        base.Start();

        confirmItem.onClick.AddListener(() => StartCoroutine(ConfirmBuy()));

        foreach (Button _button in itemsButton)
        {
            _button.onClick.AddListener(() => AddOnCart(_button.GetComponent<ItemIndex>().index, _button.GetComponent<ItemIndex>().item));
        }

        FindObjectOfType<GameManager>().parazon = this;
        waitingToDelivery.gameObject.SetActive(false);

        cartCountTXT.text = "Empty";
    }

    private void AddOnCart(int _index, GameObject _itemOBJ)
    {
        Debug.Log("O item comprado tem o index de: " +  _index);

        itemsCart.Add(_itemOBJ);
        cartCountTXT.text = itemsCart.Count.ToString();
    }

    private IEnumerator ConfirmBuy()
    {
        if (itemsCart.Count == 0) yield break;

        Debug.Log("Confirmado");

        cartCountTXT.text = "Empty";

        foreach (GameObject _item in itemsCart)
        {
            GameObject _object = Instantiate(_item, waitingToDelivery.position, Quaternion.identity);
            _object.GetComponent<ConsumeItem>().popUpUsable = popUsable;

            _object.GetComponent<ObjectsInteract>().dayToDestroy = gameManager.days + 2;
            _object.transform.parent = waitingToDelivery;

            yield return new WaitForSeconds(0.25f);
        }

        taskManager.MissionCompleted(3);
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
            if (_item.gameObject.GetComponent<ObjectsInteract>().dayToDestroy == gameManager.days)
            {
                sanityController.LostSanity(5);
                Destroy(_item.gameObject);
                Debug.Log($"Item {_item.gameObject.name} destruido");
            }
        }
    }
}