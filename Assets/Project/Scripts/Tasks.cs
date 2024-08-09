using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tasks : MonoBehaviour
{
    public string missionObject;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private TMP_Text missionTXT;
    [SerializeField] private List <string> missions;
    [SerializeField] private int rng;
    [SerializeField] private int index;
    [SerializeField] private int missionsCompletedCount;
    [SerializeField] private bool missionCompleted;

    private void Start()
    {
        missionsCompletedCount = 0;

        RandomMission();
    }

    private void Update()
    {
        if (missionsCompletedCount == missions.Count)
            AllMissionsCompleted();

        if (missionCompleted)
        {
            Debug.Log("Completada");

            missionCompleted = false;

            index = -1;

            missionTXT.text = "Missao concluida";
            Invoke("RandomMission", 5);
            return;
        }
    }

    private void RandomMission()
    {
        if (missionsCompletedCount == missions.Count) return;

        rng = Random.Range(0, missions.Count);

        while (missions[rng] == null)
            rng = Random.Range(0, missions.Count);

        missionTXT.text = missions[rng];

        missions[rng] = null;
    }

    public void MissionCompleted(int _index)
    {
        if (_index == rng)
        {
            index = _index;
            missionCompleted = true;

            Invoke(nameof(CompletedCountAdd), 4.5f);
        }
        else
            missionCompleted = false;
    }

    private void CompletedCountAdd()
    {
        missionsCompletedCount++;
    }

    private void AllMissionsCompleted()
    {
        missionTXT.text = "Todas as missões foram completas";
        rng = -1;

        return;
    }
}