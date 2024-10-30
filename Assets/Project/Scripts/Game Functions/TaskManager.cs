using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Mission
{
    public int id;
    public string name;

    public void GetMission(int _id, string _name)
    {
        id = _id;
        name = _name;
    }
}

public class TaskManager : MonoBehaviour
{
    private ScoreCounting scoreCounting;

    [SerializeField] private GameObject missionPanel;
    [SerializeField] private TMP_Text missionTXT;
    [SerializeField] private List <Mission> dayMissions;
    [SerializeField] private List <Mission> totalMissions;
    [SerializeField] private Mission currentMission;
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

    public void RandomMission()
    {
        IEnumerator NewMission()
        {
            int _rng = Random.Range(0, dayMissions.Count);
            while (dayMissions[_rng].name == null)
            {
                _rng = Random.Range(0, dayMissions.Count);

                yield return new WaitForSeconds(0.1f);
            }

            Debug.Log($"MISSAO ALEATORIA ESCOLHIDA: {_rng}");

            currentMission = dayMissions[_rng];
            missionTXT.text = currentMission.name;

            yield return new WaitForSeconds(0.05f);
            dayMissions[_rng] = null;
        }
        StartCoroutine(NewMission());
    }

    public void MissionCompleted(int _index)
    {
        if (_index == currentMission.id)
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

        currentMission = dayMissions[0];
    }
}