using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour
{
    [Header("Computer Variables")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private Button turnOnButton;
    [SerializeField] private Button template;
    [SerializeField] private GameObject screenPanel;
    [SerializeField] private GameObject appsPanel;
    [SerializeField] private GameObject windownPanel;
    [SerializeField] private bool isOn;
    [SerializeField] private bool canTurnOff;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        turnOnButton.onClick.AddListener(TurnOnPc);
        //template.onClick.AddListener(OpenWindow);
        windownPanel.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);

        appsPanel.SetActive(false);
        windownPanel.SetActive(false);
    }
    private void TurnOnPc()
    {
        if (!isOn && !canTurnOff) 
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOn");
            Invoke(nameof(ActiveApps), 1.5f);
            Invoke(nameof(CanTurnOff), 2);

            gameManager.multiplier = 2;
            isOn = true;
        }
        else if (isOn && canTurnOff)
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOff");
            Invoke(nameof(CanTurnOff), 2);

            windownPanel.SetActive(false);
            appsPanel.SetActive(false);

            gameManager.multiplier = 1;
            isOn = false;
        }
    }

    private void CanTurnOff()
    {
        if (!canTurnOff) canTurnOff = true;
        else canTurnOff = false;
    }

    private void ActiveApps()
    {
        if (!isOn) return;

        appsPanel.SetActive(true);
    }

    private void OpenWindow()
    {
        windownPanel.SetActive(true);
    }

    private void CloseWindow()
    {
        windownPanel.SetActive(false);
    }
}