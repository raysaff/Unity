using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _settingsMenu = null;
    [Space]
    [SerializeField] private Button _okButton = null;
    [SerializeField] private Toggle _hudToggle = null;
    [SerializeField] private Slider _enemySpeedSlider = null;
    [SerializeField] private Text _enemySpeed;

    private void Awake()
    {
        _okButton.onClick.AddListener(ShowMenu);
        _hudToggle.onValueChanged.AddListener(ShowHUD);
        _enemySpeedSlider.onValueChanged.AddListener(EnemySpeedChange);
    }

    private void ShowMenu()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    private void ShowHUD(bool value)
    {
        GameSet.instance.HUD = value;
    }
    private void EnemySpeedChange(float value)
    {
        GameSet.instance.enemySpeed = value;
        _enemySpeed.text = "Enemy Speed = " + value.ToString();
    }
}
