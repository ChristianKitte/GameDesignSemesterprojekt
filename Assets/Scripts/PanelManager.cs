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

    private Button startButton;
    private Button tastaturButton;
    private Button settingsButton;
    private Button quitButton;

    private Toggle checkTon;
    private Toggle checkGost;
    private Button saveButton;
    private Button returnFromSettingButton;

    private Button returnFromTutorialButton;

    /// <summary>
    /// Startroutine
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Belegt die Menüelemente beim Start
    /// </summary>
    private void OnEnable()
    {
        // Nicht in Start verschieben, da ansonsten die Referenzen nicht existieren!

        rootMainMenu = HauptMenu.rootVisualElement;
        rootSettingsMenu = EinstellungMenu.rootVisualElement;
        rootTutorialMenu = TastaturMenu.rootVisualElement;

        startButton = rootMainMenu.Q<Button>("btnStart");
        tastaturButton = rootMainMenu.Q<Button>("btnTastatur");
        settingsButton = rootMainMenu.Q<Button>("btnSettings");
        quitButton = rootMainMenu.Q<Button>("btnQuit");

        checkTon = rootSettingsMenu.Q<Toggle>("checkAudio");
        checkGost = rootSettingsMenu.Q<Toggle>("checkGhost");
        saveButton = rootSettingsMenu.Q<Button>("btnSave");
        returnFromSettingButton = rootSettingsMenu.Q<Button>("btnReturn");

        returnFromTutorialButton = rootTutorialMenu.Q<Button>("btnReturn");

        EventManager.Instance().MainMenueCallEvent += () => { showMain(); };

        if (startButton != null)
        {
            if (startButton != null)
                startButton.clickable.clicked += () =>
                {
                    Debug.Log("StartButton wurde gedrückt");
                    returnToGame();
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

    private void returnToGame()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;

        SoundManager.Instance.PlayBackgroundMusic();

        if (GameState.Instance().GameIsPlaying)
        {
            GameState.Instance().GameIsPaused = false;

            // zurück zum Spiel
            EventManager.Instance().ResumeGamePlay();
        }
        else
        {
            GameState.Instance().GameIsPlaying = true;
            GameState.Instance().GameIsPaused = false;

            // Neues Spiel starten
            EventManager.Instance().StartGamePlay();
            EventManager.Instance().StartNewGame();
        }
    }

    /// <summary>
    /// Zeigt das Hauptmenü an und ist der Haupteinstieg in das Spiel
    /// </summary>
    private void showMain()
    {
        EventManager.Instance().StopGamePlay();

        GameState.Instance().GameIsPaused = true;
        SoundManager.Instance.PlayMenueMusic();

        if (GameState.Instance().GameIsPlaying)
        {
            startButton.text = "Zurück";
        }

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
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}