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
    [SerializeField] private TMP_Text taskTXT;
    [SerializeField] private TMP_Text sleepTimeTXT;
    [SerializeField] private TMP_Text sanityTXT;
    [SerializeField] private TMP_Text message;
    [SerializeField] private string[] messageList;
    public int dayScore;
    public int taskScore;
    public int sleepScore;
    public int sanityScore;
    private string[] split, split2, split3, split4;

    [Header("Score final Day")]
    [SerializeField] private int finalScore;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        split = dayTXT.text.Split(';');
        split2 = taskTXT.text.Split(';');
        split3 = sleepTimeTXT.text.Split(';');
        split4 = sanityTXT.text.Split(';');
    }

    public void UpdateDayScore()
    {
        dayTXT.text = split[0] + ": " + gameManager.days;
        dayScore = taskScore + sleepScore + sanityScore;

        taskTXT.text = split2[0] + ": " + taskScore;
        sleepTimeTXT.text = split3[0] + ": " + sleepScore;
        sanityTXT.text = split4[0] + ": " + sanityScore;

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

    public void ResetScore()
    {
        dayScore = 0;
        taskScore = 0;
        sleepScore = 0;
        sanityScore = 0;
    }

    private void FinalDayScore()
    {
        finalScore += dayScore;
        Debug.Log($"A pontuação foi de: {finalScore}");

        if (gameManager.days == 5) dayScoreTXT.text = finalScore.ToString();
    }
}