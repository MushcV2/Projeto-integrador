using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parazon : AppsManager
{
    [SerializeField] private Button confirmItem;
    [SerializeField] private Button[] itemsButton;
    [SerializeField] private List<GameObject> itemsCart;

    protected override void Start()
    {
        base.Start();

        confirmItem.onClick.AddListener(ConfirmBuy);

        foreach (Button _button in itemsButton)
        {
            _button.onClick.AddListener(() => ClickOnItem(_button));
        }
    }

    private void ConfirmBuy()
    {
        Debug.Log("Confirmado");
    }

    private void ClickOnItem(Button _button)
    {
        Debug.Log("Botao clicado");
        BuyItem(_button.GetComponent<ItemIndex>().index);
    }

    private void BuyItem(int _index)
    {
        Debug.Log("O item comprado tem o index de: " +  _index);
    }
}