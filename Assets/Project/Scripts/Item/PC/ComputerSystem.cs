using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour
{
    [Header("Computer Variables")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Button turnOnButton;
    [SerializeField] private GameObject screenPanel;
    [SerializeField] private bool isOn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        turnOnButton.onClick.AddListener(TurnOnPc);
    }

    private void TurnOnPc()
    {
        if (!isOn) 
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOn");
            isOn = true;
        }
        else
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOff");
            isOn = false;
        }
    }
}