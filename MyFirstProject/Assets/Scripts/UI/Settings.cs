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

    private void Awake()
    {
        _okButton.onClick.AddListener(ShowMenu);
    }
    private void ShowMenu()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }
}
