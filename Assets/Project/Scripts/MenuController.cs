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

    [Header("Video UI")]
    [SerializeField] private GameObject videoScreen;
    [SerializeField] private Button closeVideoScreen;
    [SerializeField] private Button openVideoScreen;
    [SerializeField] private Button low, mid, high;

    [Header("Audio UI")]
    [SerializeField] private GameObject audioScreen;
    [SerializeField] private Button closeAudioScreen;
    [SerializeField] private Button openAudioScreen;

    [Header("Credits UI")]
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private Button closeCreditsScreen;
    [SerializeField] private Button openCreditsScreen;

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
        creditsScreen.SetActive(false);

        playButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));

        configsButton.onClick.AddListener(() => OpenScreen("config"));
        closeConfigs.onClick.AddListener(() => CloseScreen("config"));

        openVideoScreen.onClick.AddListener(() => OpenScreen("video"));
        closeVideoScreen.onClick.AddListener(() => CloseScreen("video"));
        low.onClick.AddListener(() => ChangeGraphics(0));
        mid.onClick.AddListener(() => ChangeGraphics(1));
        high.onClick.AddListener(() => ChangeGraphics(2));

        openAudioScreen.onClick.AddListener(() => OpenScreen("audio"));
        closeAudioScreen.onClick.AddListener(() => CloseScreen("audio"));

        openCreditsScreen.onClick.AddListener(() => OpenScreen("credits"));
        closeCreditsScreen.onClick.AddListener(() => CloseScreen("credits"));

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

            case "credits":
                creditsScreen.SetActive(true);
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

            case "credits":
                creditsScreen.SetActive(false);
                break;
        }
    }

    private void ChangeGraphics(int _value)
    {
        QualitySettings.SetQualityLevel(_value);
    }

    private void Exit()
    {
        Application.Quit();
    }
}