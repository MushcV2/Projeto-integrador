using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AppsManager : MonoBehaviour
{
    [SerializeField] private Transform windowPanel;
    [SerializeField] private RectTransform windowRect;
    [SerializeField] private Vector3 initialPos;

    private void Awake()
    {
        windowPanel = transform.Find("Window");
        windowPanel.gameObject.SetActive(false);

        windowRect = windowPanel.gameObject.GetComponent<RectTransform>();
        initialPos = windowRect.position;
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

    public void CloseWindow()
    {
        windowPanel.gameObject.SetActive(false);
        windowRect.position = initialPos;

        GetComponent<Button>().enabled = true;
    }
}