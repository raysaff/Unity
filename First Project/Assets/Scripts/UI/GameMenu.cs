using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu = null;

    [SerializeField] private Button _playNext = null;
    [SerializeField] private Button _GoToMainMenu = null;

    private void Awake()
    {
        _playNext.onClick.AddListener(PlayNext);
        _GoToMainMenu.onClick.AddListener(GoToMainMenu);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PlayNext();
    }

    private void PlayNext()
    {
        _gameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
