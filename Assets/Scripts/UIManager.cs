using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private Player_Input _inputActions;
    private GameObject _currentPanel;
    private GameObject _previousPanel;
    private bool _isPause;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InitInput();
        _musicSlider.onValueChanged.AddListener(delegate { AudioManager.Instance.SetMusicVolume(_musicSlider.value); });
        _sfxSlider.onValueChanged.AddListener(delegate { AudioManager.Instance.SetSfxVolume(_sfxSlider.value); });
        _currentPanel = _mainPanel;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("level", 0) > 1)
        {
            _continueButton.gameObject.SetActive(true);
        }
        SetMusicSliderValue(PlayerPrefs.GetFloat("music", 1f));
        SetSoundSliderValue(PlayerPrefs.GetFloat("sound", 1f));
    }

    private void InitInput()
    {
        _inputActions = new Player_Input();
        _inputActions.UI.Pause.performed += ctx => OnPause();
    }

    public void OnPause()
    {
        try
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;

            if (levelIndex == 0 || levelIndex == SceneManager.sceneCountInBuildSettings - 1 || _optionsPanel.activeInHierarchy)
            {
                return;
            }
            _isPause = !_isPause;
            Time.timeScale = _isPause ? 0 : 1;
            ShowPausePanel(_isPause);

        }
        catch (MissingReferenceException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ShowPausePanel(bool value)
    {
        if (_pausePanel != null)
        {
            _pausePanel.SetActive(value);
        }
        if (value)
        {
            _currentPanel = _pausePanel;
        }
        else
        {
            _currentPanel = null;
        }
    }
    public void ShowMainPanel()
    {
        ShowPanel(_mainPanel);
    }

    public void ShowOptionPanel()
    {
        ShowPanel(_optionsPanel);
    }

    public void ShowCreditsPanel()
    {
        ShowPanel(_creditsPanel);
    }

    private void ShowPanel(GameObject panel)
    {
        _currentPanel.SetActive(false);
        panel.SetActive(true);
        _previousPanel = _currentPanel;
        _currentPanel = panel;
    }

    public void Back()
    {
        ShowPanel(_previousPanel);
    }

    public void SetMusicSliderValue(float value)
    {
        _musicSlider.value = value * 100;
    }

    public void SetSoundSliderValue(float value)
    {
        _sfxSlider.value = value * 100;
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable();
    }
}
