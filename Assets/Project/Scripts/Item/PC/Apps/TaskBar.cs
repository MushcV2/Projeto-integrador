using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Button activeMenu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject systemInfo;
    [SerializeField] private bool menuIsActive;
    [SerializeField] private bool debounce;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        menuPanel.SetActive(false);
        systemInfo.SetActive(false);

        activeMenu.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        if (!menuIsActive && !debounce)
        {
            menuIsActive = true;
            menuPanel.SetActive(true);
            Invoke(nameof(ActiveSystemInfo), 0.35f);

            anim.SetTrigger("Active");

            Invoke(nameof(Debounce), 0.8f);
        }
        else if (menuIsActive && debounce)
        {
            menuIsActive = false;
            systemInfo.SetActive(false);

            anim.SetTrigger("Disable");

            Invoke(nameof(Debounce), 0.8f);
        }
    }

    private void ActiveSystemInfo()
    {
        systemInfo.SetActive(true);
    }

    private void Debounce()
    {
        if (!debounce) debounce = true;
        else debounce = false;
    }
}