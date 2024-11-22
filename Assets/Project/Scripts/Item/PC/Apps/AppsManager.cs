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
    [SerializeField] protected bool isActive;

    protected virtual void Awake()
    {
        windowPanel = transform.Find("Window");
        windowPanel.gameObject.SetActive(false);

        initialPos = windowRect.position;

        isActive = false;
    }

    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenWindow);
        windowPanel.GetComponentInChildren<Button>().onClick.AddListener(CloseWindow);
    }

    protected virtual void OpenWindow()
    {
        if (isActive) return;

        windowPanel.gameObject.SetActive(true);

        GetComponent<Button>().enabled = false;
        isActive = true;
    }

    public void CloseWindow()
    {
        windowPanel.gameObject.SetActive(false);
        windowRect.position = initialPos;
        isActive = false;

        GetComponent<Button>().enabled = true;
    }
}