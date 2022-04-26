using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private UIDocument HauptMenu;
    [SerializeField] private UIDocument EinstellungMenu;
    [SerializeField] private UIDocument TastaturMenu;

    private VisualElement rootMainMenu;
    private VisualElement rootSettingsMenu;
    private VisualElement rootTutorialMenu;

    private void OnEnable()
    {
        rootMainMenu = HauptMenu.rootVisualElement;
        rootSettingsMenu = EinstellungMenu.rootVisualElement;
        rootTutorialMenu = TastaturMenu.rootVisualElement;

        var startButton = rootMainMenu.Q<Button>("btnStart");
        var tastaturButton = rootMainMenu.Q<Button>("btnTastatur");
        var settingsButton = rootMainMenu.Q<Button>("btnSettings");
        var quitButton = rootMainMenu.Q<Button>("btnQuit");

        var checkTon = rootSettingsMenu.Q<Toggle>("checkAudio");
        var checkGost = rootSettingsMenu.Q<Toggle>("checkGhost");
        var saveButton = rootSettingsMenu.Q<Button>("btnSave");
        var returnFromSettingButton = rootSettingsMenu.Q<Button>("btnReturn");

        var returnFromTutorialButton = rootTutorialMenu.Q<Button>("btnReturn");

        if (startButton != null)
        {
            startButton.clickable.clicked += () =>
            {
                Debug.Log("StartButton wurde gedrückt");
                startGame();
            };
        }

        if (tastaturButton != null)
        {
            tastaturButton.clickable.clicked += () =>
            {
                Debug.Log("TastaturButton wurde gedrückt");
                mainToTastatur();
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

        if (returnFromSettingButton != null)
        {
            returnFromSettingButton.clickable.clicked += () =>
            {
                Debug.Log("ReturnFromSettingButton wurde gedrückt");
                settingToMain();
            };
        }

        if (returnFromTutorialButton != null)
        {
            returnFromTutorialButton.clickable.clicked += () =>
            {
                Debug.Log("ReturnFromTutorialButton wurde gedrückt");
                tutorialToMain();
            };
        }

        checkTon?.RegisterValueChangedCallback(e => { Debug.Log(e.newValue.ToString()); });

        checkGost?.RegisterValueChangedCallback(e => { Debug.Log(e.newValue.ToString()); });

        showMain();
    }

    private void startGame()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
    }

    private void showMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
    }

    private void mainToSettings()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
    }

    private void mainToTastatur()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.Flex;
    }

    private void settingToMain()
    {
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
    }

    private void tutorialToMain()
    {
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
    }

    private void quitApplication()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}