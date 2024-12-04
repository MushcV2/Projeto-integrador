using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class Mission
{
    public int id;
    public string name;
    public Sprite icon;

    public void GetMission(int _id, string _name, Sprite _icon)
    {
        id = _id;
        name = _name;
        icon = _icon;
    }
}

public class TaskManager : MonoBehaviour
{
    private ScoreCounting scoreCounting;

    [SerializeField] private AudioSource finishedSoundEffect;
    [SerializeField] private Image taskIcon;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private TMP_Text missionTXT;
    [SerializeField] private List <Mission> dayMissions;
    [SerializeField] private List <Mission> totalMissions;
    [SerializeField] private Mission currentMission;
    [SerializeField] private int index;
    [SerializeField] private int missionsCompletedCount;
    [SerializeField] private bool missionCompleted;
    [SerializeField] private int nullMissions;
    public string missionObject;

    private void Start()
    {
        scoreCounting = GameObject.FindGameObjectWithTag("ScoreCounting").GetComponent<ScoreCounting>();
        missionsCompletedCount = 0;

        AddThreeMissions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && Application.isEditor) RandomMission();

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
            Invoke(nameof(RandomMission), 30);
        }
    }

    public void RandomMission()
    {
        IEnumerator NewMission()
        {
            int _rng = Random.Range(0, dayMissions.Count);
            while (dayMissions[_rng] == null || dayMissions[_rng].name == null)
            {
                _rng = Random.Range(0, dayMissions.Count);

                yield return new WaitForSeconds(0.1f);
            }

            Debug.Log($"MISSAO ALEATORIA ESCOLHIDA: {_rng}");

            currentMission = dayMissions[_rng];
            missionTXT.text = currentMission.name;

            if (currentMission.icon == null) taskIcon.gameObject.SetActive(false);
            else
            {
                taskIcon.sprite = currentMission.icon;
                taskIcon.gameObject.SetActive(true);
            }

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
            finishedSoundEffect.Play();
            taskIcon.gameObject.SetActive(false);

            scoreCounting.taskScore += 125;
            playerController.GainSanity(10);

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
        missionTXT.text = "Todas as missões foram completas";
        taskIcon.gameObject.SetActive(false);
    }

    public void AddThreeMissions()
    {
        if (totalMissions.Count == 0) return;

        missionsCompletedCount = 0;
        dayMissions.Clear();

        for (int i = 1; i <= 3; i++)
        {
            int _rng = Random.Range(0, totalMissions.Count);

            dayMissions.Add(totalMissions[_rng]);
            totalMissions.Remove(totalMissions[_rng]);
        }

        RandomMission();
    }

    public void SetDayMission()
    {
        int _nullMissions = 0;

        foreach (var _mission in dayMissions)
        {
            if (_mission == null || _mission.name == null)
            {
                _nullMissions++;
                Debug.Log($"Missoes nulas {_nullMissions}");
            }
        }

        if (_nullMissions == dayMissions.Count) AddThreeMissions();
        else
        {
            for (int i = dayMissions.Count - 1; i >= 0; i--)
            {
                if (dayMissions[i] == null || dayMissions[i].name == null)
                {
                    int _rng = Random.Range(0, totalMissions.Count);

                    dayMissions[i] = totalMissions[_rng];
                    totalMissions.RemoveAt(_rng);
                }
            }

            Invoke(nameof(RandomMission), 30);
        }
    }
}