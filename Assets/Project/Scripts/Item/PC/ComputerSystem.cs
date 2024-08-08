using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour
{
    [Header("Computer Variables")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Button turnOnButton;
    [SerializeField] private Button template;
    [SerializeField] private GameObject screenPanel;
    [SerializeField] private GameObject appsPanel;
    [SerializeField] private GameObject windownPanel;
    [SerializeField] private bool isOn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        turnOnButton.onClick.AddListener(TurnOnPc);
        template.onClick.AddListener(OpenWindow);
        windownPanel.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);

        appsPanel.SetActive(false);
        windownPanel.SetActive(false);
    }

    private void TurnOnPc()
    {
        if (!isOn) 
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOn");
            Invoke(nameof(ActiveApps), 1.5f);

            isOn = true;
        }
        else
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOff");

            windownPanel.SetActive(false);
            appsPanel.SetActive(false);

            isOn = false;
        }
    }

    private void ActiveApps()
    {
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