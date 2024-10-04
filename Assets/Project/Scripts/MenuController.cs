using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button configsButton;
    [SerializeField] private Button closeConfigs;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject configsBG;

    [Header("Pause UI")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button closePauseScreen;

    //[Header("Variables")]

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        configsBG.SetActive(false);

        playButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));
        configsButton.onClick.AddListener(OpenConfigs);
        closeConfigs.onClick.AddListener(CloseConfigs);
        exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game")
        {
            Debug.Log("Pausado");
            pauseScreen.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B)) StartCoroutine(LoadGameScene());
    }
    private IEnumerator LoadGameScene()
    {
        AsyncOperation _async = SceneManager.LoadSceneAsync("Game");

        while (!_async.isDone)
        {
            yield return null;
        }

        pauseScreen = GameObject.Find("PauseScreen");

        closePauseScreen = pauseScreen.transform.Find("Close").GetComponent<Button>();
        closePauseScreen.onClick.AddListener(ClosePauseScreen);

        pauseScreen.SetActive(false);
    }

    private void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    private void OpenConfigs()
    {
        configsBG.SetActive(true);
    }

    private void CloseConfigs()
    {
        configsBG.SetActive(false);
    }

    private void Exit()
    {
        Application.Quit();
    }
}