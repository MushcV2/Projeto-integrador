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
    [SerializeField] private Button returnToGame;
    [SerializeField] private Button returnToMenu;

    [Header("Video UI")]
    [SerializeField] private GameObject videoScreen;
    [SerializeField] private Toggle haveMotionBlur;
    [SerializeField] private Button closeVideoScreen;
    [SerializeField] private Button openVideoScreen;
    [SerializeField] private Button[] graphicsButton;

    [Header("Audio UI")]
    [SerializeField] private GameObject audioScreen;
    [SerializeField] private Button closeAudioScreen;
    [SerializeField] private Button openAudioScreen;

    [Header("Credits UI")]
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private Button closeCreditsScreen;
    [SerializeField] private Button openCreditsScreen;

    [Header("Controls UI")]
    [SerializeField] private GameObject controlScreen;
    [SerializeField] private Button closeControlScreen;
    [SerializeField] private Button openControlScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GraphicsValue"));
        Debug.Log(QualitySettings.GetQualitySettings());

        configsBG.SetActive(false);
        videoScreen.SetActive(false);
        audioScreen.SetActive(false);
        creditsScreen.SetActive(false);
        controlScreen.SetActive(false);

        playButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));

        configsButton.onClick.AddListener(() => OpenScreen("config"));
        closeConfigs.onClick.AddListener(() => CloseScreen("config"));

        #region Video Screen

        openVideoScreen.onClick.AddListener(() => OpenScreen("video"));
        closeVideoScreen.onClick.AddListener(() => CloseScreen("video"));

        graphicsButton[0].onClick.AddListener(() => ChangeGraphics(0));
        graphicsButton[1].onClick.AddListener(() => ChangeGraphics(1));
        graphicsButton[2].onClick.AddListener(() => ChangeGraphics(2));

        haveMotionBlur.onValueChanged.AddListener(MotionBlur);

        #endregion

        #region Audio Screen

        openAudioScreen.onClick.AddListener(() => OpenScreen("audio"));
        closeAudioScreen.onClick.AddListener(() => CloseScreen("audio"));

        #endregion

        #region Credits Screen

        openCreditsScreen.onClick.AddListener(() => OpenScreen("credits"));
        closeCreditsScreen.onClick.AddListener(() => CloseScreen("credits"));

        #endregion

        #region Control Screen

        openControlScreen.onClick.AddListener(() => OpenScreen("control"));
        closeControlScreen.onClick.AddListener(() => CloseScreen("control"));

        #endregion

        exitButton.onClick.AddListener(Exit);

        if (PlayerPrefs.GetInt("HaveMotion") == 1)
            haveMotionBlur.isOn = true;
        else
            haveMotionBlur.isOn = false;
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
        returnToGame = pauseScreen.transform.Find("Game").GetComponent<Button>();
        returnToMenu = pauseScreen.transform.Find("Menu").GetComponent<Button>();

        returnToMenu.onClick.AddListener(ToMenu);
        returnToGame.onClick.AddListener(() => CloseScreen("pause"));
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

            case "control":
                controlScreen.SetActive(true);
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

                Time.timeScale = 1f;
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

            case "control":
                controlScreen.SetActive(false);
                break;
        }
    }

    private void ChangeGraphics(int _value)
    {
        QualitySettings.SetQualityLevel(_value);
        PlayerPrefs.SetInt("GraphicsValue", _value);

        Debug.Log(QualitySettings.names[_value]);
    }

    private void MotionBlur(bool _isOn)
    {
        if (_isOn)
            PlayerPrefs.SetInt("HaveMotion", 1);
        else
            PlayerPrefs.SetInt("HaveMotion", 0);
    }

    private void ToMenu()
    {
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }

    private void Exit()
    {
        Application.Quit();
    }
}