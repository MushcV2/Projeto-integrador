using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    [Header("Clock Variables")]
    [SerializeField] private SanityController sanity;
    [SerializeField] private TMP_Text clockTXT;
    [SerializeField] private TMP_Text dayTXT;
    [SerializeField] private float timeElpase;
    [SerializeField] private float multiplier;
    [SerializeField] private float seconds;
    [SerializeField] private int minutes;
    [SerializeField] private int days;
    [SerializeField] private bool cycleCompleted;
    [SerializeField] private bool dayCycleCompleted;


    [Header("Star New Day Variables")]
    [SerializeField] private GameObject dayPointsPanel;
    [SerializeField] private Button startNextDay;



    private void Start()
    {
        StartCoroutine(LostSanity());
        dayTXT.text = "Days: " + days;

        dayPointsPanel.SetActive(false);

        startNextDay.onClick.AddListener(StarNewDay);
    }

    private void Update()
    {
        timeElpase += Time.deltaTime * multiplier;

        UpdateClock(timeElpase);
    }

    private void UpdateClock(float _time)
    {
        if (minutes == 2 && !dayCycleCompleted) DayCycle();

        if (minutes >= 24)
            cycleCompleted = true;

        if (cycleCompleted)
            minutes = -5;
        else
            minutes = 19;

        minutes += Mathf.FloorToInt(_time / 60) % 60;
        seconds = Mathf.FloorToInt(_time % 60);

        clockTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void DayCycle()
    {
        print("Dia virou");

        dayCycleCompleted = true;
        multiplier = 0;
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

    private void StarNewDay()
    {
        dayPointsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        minutes = 19;
        multiplier = 1;
    }
}