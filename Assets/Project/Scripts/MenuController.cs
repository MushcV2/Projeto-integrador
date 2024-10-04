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
    [SerializeField] private Button openPauseScreen;

    [Header("Video UI")]
    [SerializeField] private GameObject videoScreen;
    [SerializeField] private Button closeVideoScreen;
    [SerializeField] private Button openVideoScreen;

    [Header("Audio UI")]
    [SerializeField] private GameObject audioScreen;
    [SerializeField] private Button closeAudioScreen;
    [SerializeField] private Button openAudioScreen;

    //[Header("Variables")]

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        configsBG.SetActive(false);
        videoScreen.SetActive(false);
        audioScreen.SetActive(false);

        playButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));

        configsButton.onClick.AddListener(() => OpenScreen("config"));
        closeConfigs.onClick.AddListener(() => CloseScreen("config"));

        openVideoScreen.onClick.AddListener(() => OpenScreen("video"));
        closeVideoScreen.onClick.AddListener(() => CloseScreen("video"));

        openAudioScreen.onClick.AddListener(() => OpenScreen("audio"));
        closeAudioScreen.onClick.AddListener(() => CloseScreen("audio")); 

        exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Game")
        {
            OpenScreen("pause");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        closePauseScreen.onClick.AddListener(() => CloseScreen("pause"));

        pauseScreen.SetActive(false);
    }

    private void OpenScreen(string _name)
    {
        switch (_name)
        {
            case "config":
                configsBG.SetActive(true);
                break;

            case "pause":
                pauseScreen.SetActive(true);
                break;

            case "video":
                videoScreen.SetActive(true);
                break;

            case "audio":
                audioScreen.SetActive(true);
                break;
        }
    }

    private void CloseScreen(string _name)
    {
        switch (_name)
        {
            case "config":
                configsBG.SetActive(false);
                break;

            case "pause":
                pauseScreen.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case "video":
                videoScreen.SetActive(false);
                break;

            case "audio":
                audioScreen.SetActive(false);
                break;
        }
    }

    private void Exit()
    {
        Application.Quit();
    }
}