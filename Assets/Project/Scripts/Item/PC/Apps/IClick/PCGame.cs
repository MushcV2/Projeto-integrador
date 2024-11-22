using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PCGame : AppsManager
{
    [Header("Updgrade Buttons")]
    [SerializeField] private Button[] updgradeButtons;
    [SerializeField] private Button updgrade_1;
    [SerializeField] private Button updgrade_2;
    [SerializeField] private Button updgrade_3;
    [SerializeField] private Button updgrade_4;

    [Header("System")]
    [SerializeField] private Button clickerButton;
    [SerializeField] private TMP_Text pointsTXT;
    [SerializeField] private int points;
    [SerializeField] private float multiplier;

    protected override void Start()
    {
        base.Start();

        clickerButton.onClick.AddListener(EarnPoints);

        pointsTXT.text = points.ToString("N0");

        updgradeButtons[0].onClick.AddListener(() => BuyUpdgrade(100 , 1.5f, 0));
        updgradeButtons[1].onClick.AddListener(() => BuyUpdgrade(800, 3.5f, 1));
        updgradeButtons[2].onClick.AddListener(() => BuyUpdgrade(6800, 5f, 2));
        updgradeButtons[3].onClick.AddListener(() => BuyUpdgrade(25000, 10f, 3));
    }

    private void EarnPoints()
    {
        points = Mathf.RoundToInt(multiplier * points);
        pointsTXT.text = points.ToString("N0");
    }

    private void LostPoints(int _value)
    {
        points = Mathf.Max(points - _value, 0);
        pointsTXT.text = points.ToString("N0");
    }

    private void BuyUpdgrade(int _value, float _multiplier, int _index)
    {
        if (points - _value <= 0) return;

        LostPoints(_value);

        multiplier += multiplier;
        updgradeButtons[_index].gameObject.SetActive(false);
    }
}