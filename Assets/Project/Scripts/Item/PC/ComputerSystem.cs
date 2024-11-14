using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerSystem : MonoBehaviour
{
    [Header("Computer Variables")]
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private AppsManager[] appsManager;
    [SerializeField] private Button turnOnButton;
    [SerializeField] private Button taskBarTurnOff;
    [SerializeField] private Button restartTaskBar;
    [SerializeField] private GameObject taskBarPanel;
    [SerializeField] private GameObject screenPanel;
    [SerializeField] private GameObject appsPanel;
    [SerializeField] private bool isOn;
    [SerializeField] private bool canTurnOff;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        turnOnButton.onClick.AddListener(TurnOnPc);
        taskBarTurnOff.onClick.AddListener(TaskBarTurnOff);
        restartTaskBar.onClick.AddListener(Restart);

        appsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isOn && player.isSitting) audioS.Play();
    }

    private void TurnOnPc()
    {
        if (!isOn && !canTurnOff) 
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOn");

            audioS.clip = audioClip[0];
            audioS.Play();

            Invoke(nameof(ActiveApps), 1.5f);
            Invoke(nameof(CanTurnOff), 2);

            gameManager.multiplier = 2;
            isOn = true;
        }
        else if (isOn && canTurnOff)
        {
            screenPanel.GetComponent<Animator>().SetTrigger("TurnOff");
            taskBarPanel.transform.Find("SystemMenu").gameObject.SetActive(false);

            audioS.clip = audioClip[1];
            audioS.Play();

            foreach (var app in appsManager)
            {
                app.CloseWindow();
            }
            appsPanel.SetActive(false);

            Invoke(nameof(CanTurnOff), 2);

            int _rng = Random.Range(1, 10); // RNG TO BROKEN PC

            if (_rng == 1)
                Debug.Log("Computador quebrou");

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

    private void TaskBarTurnOff()
    {
        Debug.Log("Desativado pela barra de tarefas");

        screenPanel.GetComponent<Animator>().SetTrigger("TurnOff");
        taskBarPanel.transform.Find("SystemMenu").gameObject.SetActive(false);

        audioS.clip = audioClip[1];
        audioS.Play();

        foreach (var app in appsManager)
        {
            app.CloseWindow();
        }
        appsPanel.SetActive(false);

        Invoke(nameof(CanTurnOff), 2);
        gameManager.multiplier = 1;
        isOn = false;
    }

    private void Restart()
    {
        Debug.Log("Reiniciando");

        TaskBarTurnOff();
        Invoke(nameof(TurnOnPc), 4);
    }
}