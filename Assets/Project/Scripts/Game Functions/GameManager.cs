using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Clock Variables")]
    [SerializeField] private TMP_Text clockTXT;
    [SerializeField] private TMP_Text dayTXT;
    [SerializeField] private TMP_Text dayComputerTXT;
    [SerializeField] private float timeElapse;
    [SerializeField] private int minutes;
    [SerializeField] private bool timeIsRunning;
    public float seconds;
    public int days;
    public float multiplier;

    [Header("Day System Variables")]
    [SerializeField] private GameObject dayPointsPanel;
    [SerializeField] private GameObject sanityHUD;
    [SerializeField] private GameObject clockHUD;
    [SerializeField] private GameObject taskHUD;
    [SerializeField] private GameObject crosshairHUD;
    [SerializeField] private GameObject computerChair;
    [SerializeField] private GameObject computer;
    [SerializeField] private Button startNextDay;
    [SerializeField] private Transform newDayPos;
    [SerializeField] private Transform playerPos;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private PlayerController playerControl;
    [SerializeField] private SanityController sanityController;
    [SerializeField] private ScoreCounting scoreCounting;
    [SerializeField] private CleaningTask cleaningTask;
    public Parazon parazon;
    private string[] textParts;

    [Header("System")]
    [SerializeField] private Volume volume;
    [SerializeField] private MotionBlur motionBlur;
    public Button forceNewDay;
    public bool activeCursor;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        textParts = dayTXT.text.Split(';');
        dayTXT.text = textParts[0] + " " + days;
        dayComputerTXT.text = textParts[0] + " " + days;

        timeIsRunning = true;

        dayPointsPanel.SetActive(false);

        startNextDay.onClick.AddListener(StartNewDay);
        forceNewDay.onClick.AddListener(DayCycle);

        if (Application.isEditor)
            forceNewDay.gameObject.SetActive(true);
        else
            forceNewDay.gameObject.SetActive(false);

        if (volume.profile.TryGet<MotionBlur>(out motionBlur))
        {
            if (PlayerPrefs.GetInt("HaveMotion") == 1) motionBlur.active = true;
            else motionBlur.active = false;
        }

        /*
        if (volume != null && volume.profile != null)
            if (volume.profile.TryGet(out MotionBlur _blur))
                if (PlayerPrefs.GetInt("HaveMotion") == 1) _blur.active = true;
                else _blur.active = false;
        */
    }

    private void Update()
    {
        clockTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeIsRunning)
        {
            timeElapse += Time.deltaTime * multiplier;

            if (timeElapse >= 1f)
            {
                UpdateClock(timeElapse);
                timeElapse = 0f;
            }
        }

        #region ACTIVE_CURSOR
        if (Input.GetKeyDown(KeyCode.M) && !activeCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            activeCursor = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && activeCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            activeCursor = false;
        }
        #endregion
    }

    private void UpdateClock(float _time)
    {
        seconds += Mathf.FloorToInt(_time % 60);

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;

            sanityController.LostSanity(10);
        }

        if (minutes >= 24)
            minutes = 0;

        if (minutes == 2)
            DayCycle();
    }

    private void DayCycle()
    {
        Debug.Log("Dia passou");

        if (minutes >= 17 && minutes <= 22) scoreCounting.sleepScore += 250;
        else scoreCounting.sleepScore += 125;

        if (sanityController.currentSanity >= 75) scoreCounting.sanityScore += 400;
        else if (sanityController.currentSanity < 75 && sanityController.currentSanity >= 50) scoreCounting.sanityScore += 200;
        else if (sanityController.currentSanity < 50 && sanityController.currentSanity >= 25) scoreCounting.sanityScore += 100;
        else scoreCounting.sanityScore += 50;

        scoreCounting.UpdateDayScore();
        taskManager.SetDayMission();

        timeIsRunning = false;
        days++;

        DisplayDayPoints();

        dayTXT.text = textParts[0] + " " + days;
        dayComputerTXT.text = textParts[0] + " " + days;
    }

    private void DisplayDayPoints()
    {
        playerControl.canMove = false;
        playerPos.position = newDayPos.position;

        if (parazon != null)
        {
            parazon.DeliverItems();
            parazon.DestroyItems();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dayPointsPanel.SetActive(true);
    }

    private void StartNewDay()
    {
        Debug.Log("Novo dia");

        dayPointsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInteract.canInteract = false;
        playerInteract.alreadyInteract = false;
        playerControl.isSitting = false;
        playerControl.canMove = true;
        playerControl.clockUI.SetActive(false);
        playerControl.GetComponent<SanityController>().GainSanity(playerControl.GetComponent<SanityController>().currentSanity / 2);

        Camera.main.GetComponent<CameraController>().stopFollowing = false;

        computer.layer = LayerMask.NameToLayer("Default");
        computerChair.layer = LayerMask.NameToLayer("Interact");

        // ---ACTIVE HUD--- //
        taskHUD.SetActive(true);
        sanityHUD.SetActive(true);
        crosshairHUD.SetActive(true);

        if (playerControl.isCrouching) playerControl.Crounch();

        scoreCounting.ResetScore();
        cleaningTask.ResetMission();

        multiplier = 1;
        minutes = 19;
        timeElapse = 0;
        seconds = 0;

        timeIsRunning = true;
    }
}