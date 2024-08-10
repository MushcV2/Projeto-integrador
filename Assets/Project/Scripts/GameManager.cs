using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Clock Variables")]
    [SerializeField] private SanityController sanity;
    [SerializeField] private TMP_Text clockTXT;
    [SerializeField] private TMP_Text dayTXT;
    [SerializeField] private float timeElapse;
    [SerializeField] private float multiplier;
    [SerializeField] private float seconds;
    [SerializeField] private int minutes;
    [SerializeField] private int days;
    [SerializeField] private bool dayCycleCompleted;
    [SerializeField] private bool timeIsRunning;

    [Header("Start New Day Variables")]
    [SerializeField] private GameObject dayPointsPanel;
    [SerializeField] private Button startNextDay;

    [Header("ONLY IN EDITOR VARIABLES")]
    public Button forceNewDay;
    public bool activeCursor;

    private void Start()
    {
        StartCoroutine(LostSanity());
        dayTXT.text = "Days: " + days;

        timeIsRunning = true;

        dayPointsPanel.SetActive(false);

        startNextDay.onClick.AddListener(StartNewDay);
        forceNewDay.onClick.AddListener(DayCycle);

        if (Application.isEditor)
            forceNewDay.gameObject.SetActive(true);
        else
            forceNewDay.gameObject.SetActive(false);
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
        //if (minutes == 2 && !dayCycleCompleted) DayCycle();
        seconds += Mathf.FloorToInt(_time % 60);

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }

        if (minutes >= 24)
            minutes = 0;

        if (minutes == 2)
            DayCycle();
    }

    private void DayCycle()
    {
        Debug.Log("Dia passou");

        dayCycleCompleted = true;
        timeIsRunning = false;
        days++;

        DisplayDayPoints();

        dayTXT.text = "Days: " + days;
    }

    private IEnumerator LostSanity()
    {
        yield return new WaitForSeconds(10);
        sanity.LostSanity(2);

        yield return new WaitForSeconds(10);
        StartCoroutine(LostSanity());
    }

    private void DisplayDayPoints()
    {
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

        minutes = 19;
        timeElapse = 0;
        seconds = 0;

        timeIsRunning = true;
        //cycleCompleted = false;
    }
}