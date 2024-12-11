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
    [SerializeField] private TMP_Text moneyTXT;
    [SerializeField] public Transform deliveryPoint;
    [SerializeField] private Dictionary<GameObject, int> itemsCart = new Dictionary<GameObject, int>();
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject popUsable;
    [SerializeField] private SanityController sanityController;
    [SerializeField] private int money = 20;
    private int previousMoney;
    public Transform waitingToDelivery;

    protected override void Start()
    {
        base.Start();

        confirmItem.onClick.AddListener(() => StartCoroutine(ConfirmBuy()));

        foreach (Button _button in itemsButton)
        {
            _button.onClick.AddListener(() => AddOnCart(_button.GetComponent<ItemIndex>().index, _button.GetComponent<ItemIndex>().item, _button.GetComponent<ItemIndex>().price));
        }

        FindObjectOfType<GameManager>().parazon = this;
        waitingToDelivery.gameObject.SetActive(false);

        moneyTXT.text = "$" + money.ToString();
        cartCountTXT.text = "Empty";
    }

    private void AddOnCart(int _index, GameObject _itemOBJ, int _price)
    {
        Debug.Log("O item comprado tem o index de: " +  _index);

        previousMoney += _price;
        itemsCart.Add(_itemOBJ, _price);
        cartCountTXT.text = itemsCart.Count.ToString();
    }

    private IEnumerator ConfirmBuy()
    {
        if (itemsCart.Count <= 0) yield break;

        Debug.Log("Confirmado");

        cartCountTXT.text = "Vazio";

        if (money - previousMoney < 0)
        {
            moneyTXT.color = Color.red;

            yield return new WaitForSeconds(0.2f);
            moneyTXT.color = Color.black;

            itemsCart.Clear();
            previousMoney = 0;

            Debug.Log("Sem grana");

            yield break;
        }

        Debug.Log("Foreach chamado");
        foreach (var _item in itemsCart)
        {
            Debug.Log($"item comprado");

            previousMoney = 0;
            money -= _item.Value;
            moneyTXT.text = "$" + money.ToString();

            GameObject _object = Instantiate(_item.Key, waitingToDelivery.position, Quaternion.identity);
            //_object.GetComponent<ConsumeItem>().popUpUsable = popUsable;

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

    protected override void OpenWindow()
    {
        base.OpenWindow();
        controlWindows.isActive = true;
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
        controlWindows.isActive = false;
    }

    public void GiveMoney()
    {
        money += 20;
        moneyTXT.text = "$" + money.ToString();
    }
}