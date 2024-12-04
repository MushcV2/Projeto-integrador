using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PCGame : AppsManager
{
    [Header("Updgrade Buttons")]
    [SerializeField] private Button[] updgradeButtons;

    [Header("System")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Button clickerButton;
    [SerializeField] private TMP_Text pointsTXT;
    [SerializeField] private TMP_Text pointPerClickTXT;
    [SerializeField] private TMP_Text multiplierTXT;
    [SerializeField] private int points;
    [SerializeField] private int pointsPerClick;
    [SerializeField] private float multiplier;

    protected override void Start()
    {
        base.Start();

        clickerButton.onClick.AddListener(EarnPoints);

        pointsTXT.text = points.ToString("N0");
        pointPerClickTXT.text = "CPS: " + (multiplier * pointsPerClick).ToString();
        multiplierTXT.text = "Mult: " + multiplier.ToString();

        updgradeButtons[0].onClick.AddListener(() => BuyUpdgrade(99 , 1.5f, 0));
        updgradeButtons[1].onClick.AddListener(() => BuyUpdgrade(799, 3.5f, 1));
        updgradeButtons[2].onClick.AddListener(() => BuyUpdgrade(6799, 5f, 2));
        updgradeButtons[3].onClick.AddListener(() => BuyUpdgrade(24999, 10f, 3));
    }

    private void EarnPoints()
    {
        points += Mathf.RoundToInt(multiplier * pointsPerClick);

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
        multiplierTXT.text = "Mult: " + multiplier.ToString();
        pointPerClickTXT.text = "CPS: " + (multiplier * pointsPerClick).ToString();

        updgradeButtons[_index].gameObject.SetActive(false);
    }

    protected override void OpenWindow()
    {
        base.OpenWindow();

        controlWindows.isActive = true;
        gameManager.multiplier = 4;
    }

    public override void CloseWindow()
    {
        base.CloseWindow();

        controlWindows.isActive = false;
        gameManager.multiplier = 2;
    }
}