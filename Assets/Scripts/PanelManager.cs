using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private UIDocument MainMenu;
    [SerializeField] private UIDocument SettingsMenu;

    private VisualElement rootMainMenu;
    private VisualElement rootSettingsMenu;

    private void OnEnable()
    {
        rootMainMenu = MainMenu.rootVisualElement;
        rootSettingsMenu = SettingsMenu.rootVisualElement;

        var startButton = rootMainMenu.Q<Button>("btnStart");
        var tutorialButton = rootMainMenu.Q<Button>("btnTutorial");
        var settingsButton = rootMainMenu.Q<Button>("btnSettings");
        var quitButton = rootMainMenu.Q<Button>("btnQuit");

        var checkTon = rootSettingsMenu.Q<Toggle>("checkAudio");
        var checkGost = rootSettingsMenu.Q<Toggle>("checkGhost");
        var saveButton = rootSettingsMenu.Q<Button>("btnSave");
        var returnButton = rootSettingsMenu.Q<Button>("btnReturn");

        if (startButton != null)
        {
            startButton.clickable.clicked += () =>
            {
                Debug.Log("StartButton wurde gedrückt");
                startGame();
            };
        }

        if (tutorialButton != null)
        {
            tutorialButton.clickable.clicked += () =>
            {
                Debug.Log("TutorialButton wurde gedrückt");
                mainToTutorial();
            };
        }

        if (settingsButton != null)
        {
            settingsButton.clickable.clicked += () =>
            {
                Debug.Log("SettingsButton wurde gedrückt");
                mainToSettings();
            };
        }

        if (quitButton != null)
        {
            quitButton.clickable.clicked += () =>
            {
                Debug.Log("QuitButton wurde gedrückt");
                quitApplication();
            };
        }

        if (saveButton != null)
        {
            saveButton.clickable.clicked += () => { Debug.Log("saveButton wurde gedrückt"); };
        }

        if (returnButton != null)
        {
            returnButton.clickable.clicked += () =>
            {
                Debug.Log("returnButton wurde gedrückt");
                settingToMain();
            };
        }

        showMain();
    }

    private void startGame()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void showMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void mainToSettings()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.Flex;
    }

    private void mainToTutorial()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void settingToMain()
    {
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootMainMenu.style.display = DisplayStyle.Flex;
    }

    private void quitApplication()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}