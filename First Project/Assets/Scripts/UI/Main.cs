using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;
    [Space]
    [SerializeField] private Button _startButton = null;
    [SerializeField] private Button _settingsButton = null;
    [SerializeField] private Button _quitButton = null;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(ShowSettings);
        _quitButton.onClick.AddListener(EndGame);
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowSettings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    private void EndGame()
    {
        Application.Quit();
    }
}
