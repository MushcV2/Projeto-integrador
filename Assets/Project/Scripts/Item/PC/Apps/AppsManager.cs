using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppsManager : MonoBehaviour
{
    [SerializeField] private Transform windowPanel;

    private void Awake()
    {
        windowPanel = transform.Find("Window");
        windowPanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenWindow);
        windowPanel.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);
    }

    protected virtual void OpenWindow()
    {
        windowPanel.gameObject.SetActive(true);
        GetComponent<Button>().enabled = false;

        Debug.Log("Ativado");
    }

    private void CloseWindow()
    {
        windowPanel.gameObject.SetActive(false);
        GetComponent<Button>().enabled = true;
    }
}