using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Button activeMenu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private bool menuIsActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        menuPanel.SetActive(false);

        activeMenu.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        if (!menuIsActive)
        {
            menuIsActive = true;
            menuPanel.SetActive(true);
            anim.SetTrigger("Active");
        }
        else
        {
            menuIsActive = false;
            anim.SetTrigger("Disable");
        }
    }
}