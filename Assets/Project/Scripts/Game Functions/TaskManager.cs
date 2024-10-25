using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    private ScoreCounting scoreCounting;

    [SerializeField] private GameObject missionPanel;
    [SerializeField] private TMP_Text missionTXT;
    [SerializeField] private List <string> dayMissions;
    [SerializeField] private List <string> totalMissions;
    [SerializeField] private int rng;
    [SerializeField] private int index;
    [SerializeField] private int missionsCompletedCount;
    [SerializeField] private bool missionCompleted;
    public string missionObject;

    private void Start()
    {
        scoreCounting = GameObject.FindGameObjectWithTag("ScoreCounting").GetComponent<ScoreCounting>();
        missionsCompletedCount = 0;

        SetDayMissions();
        RandomMission();
    }

    private void Update()
    {
        if (missionsCompletedCount == dayMissions.Count)
        {
            AllMissionsCompleted();
            return;
        }

        if (missionCompleted)
        {
            Debug.Log("Completada");

            missionCompleted = false;

            index = -1;

            missionTXT.text = "Missao concluida";
            Invoke(nameof(RandomMission), 5);
        }
    }

    private void RandomMission()
    {
        print("Missao aleatoria");

        IEnumerator NewMission()
        {
            rng = Random.Range(0, dayMissions.Count);
            while (dayMissions[rng] == null)
            {
                rng = Random.Range(0, dayMissions.Count);

                yield return new WaitForSeconds(0.1f);
            }

            missionTXT.text = dayMissions[rng];

            yield return new WaitForSeconds(0.05f);
            dayMissions[rng] = null;
        }
        StartCoroutine(NewMission());
    }

    public void MissionCompleted(int _index)
    {
        if (_index == rng)
        {
            index = _index;
            missionCompleted = true;

            scoreCounting.taskScore += 150;

            Invoke(nameof(CompletedCountAdd), 4.5f);
        }
        else
            missionCompleted = false;
    }

    private void CompletedCountAdd()
    {
        CancelInvoke(nameof(CompletedCountAdd));
        missionsCompletedCount++;
    }

    private void AllMissionsCompleted()
    {
        CancelInvoke(nameof(RandomMission));
        missionTXT.text = "Todas as miss�es foram completas";

        scoreCounting.taskScore += 400;
    }

    public void SetDayMissions()
    {
        if (totalMissions.Count == 0) return;

        dayMissions.Clear();

        for (int i = 1; i <= 3; i++)
        {
            int _rng = Random.Range(0, totalMissions.Count);

            dayMissions.Add(totalMissions[_rng]);
            totalMissions.Remove(totalMissions[_rng]);
        }
    }
}