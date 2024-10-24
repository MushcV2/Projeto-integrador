using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounting : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Score per Day")]
    [SerializeField] private TMP_Text dayTXT;
    [SerializeField] private TMP_Text dayScoreTXT;
    [SerializeField] private TMP_Text message;
    [SerializeField] private string[] messageList;
    public int dayScore;
    private string[] split;

    [Header("Score final Day")]
    [SerializeField] private int finalScore;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        split = dayTXT.text.Split(';');
    }

    public void UpdateDayScore()
    {
        dayTXT.text = split[0] + " : " + gameManager.days;

        if (dayScore <= 500)
        {
            dayScoreTXT.text = "D";
            message.text = messageList[0];
        }

        else if (dayScore >= 501 && dayScore <= 1000)
        {
            dayScoreTXT.text = "C";
            message.text = messageList[1];
        }

        else if (dayScore >= 1001 && dayScore <= 1500)
        {
            dayScoreTXT.text = "B";
            message.text = messageList[2];
        }

        else
        {
            dayScoreTXT.text = "A";
            message.text = messageList[3];
        }

        FinalDayScore();
    }

    private void FinalDayScore()
    {
        finalScore += dayScore;
        Debug.Log($"A pontuação foi de: {finalScore}");

        if (gameManager.days == 5) dayScoreTXT.text = finalScore.ToString();
    }
}