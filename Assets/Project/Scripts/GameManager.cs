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
    [SerializeField] private int minutes;
    [SerializeField] private int days;
    [SerializeField] private bool cycleCompleted;
    [SerializeField] private bool dayCycleCompleted;

    private void Start()
    {
        StartCoroutine(LostSanity());
        dayTXT.text = "Days: " + days;
    }

    private void Update()
    {
        timeElpase += Time.deltaTime * multiplier;

        UpdateClock(timeElpase);

        if (minutes == 2 && !dayCycleCompleted)
            DayCycle();
    }

    private void UpdateClock(float _time)
    {
        if (minutes >= 24)
        {
            cycleCompleted = true;
            dayCycleCompleted = false;
        }

        if (cycleCompleted)
            minutes = -5;
        else
            minutes = 19;

        minutes += Mathf.FloorToInt(_time / 60) % 60;
        int _seconds = Mathf.FloorToInt(_time % 60);

        clockTXT.text = string.Format("{0:00}:{1:00}", minutes, _seconds);
    }

    private void DayCycle()
    {
        days++;
        minutes = 19;

        dayCycleCompleted = true;
        cycleCompleted = false;

        dayTXT.text = "Days: " + days;
    }

    private IEnumerator LostSanity()
    {
        yield return new WaitForSeconds(10);
        sanity.LostSanity(2);

        yield return new WaitForSeconds(10);
        StartCoroutine(LostSanity());
    }
}