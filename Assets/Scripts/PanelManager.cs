using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Händelt die Anzeige und Steuerung fes Hauptmenüs
/// </summary>
public class PanelManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Das UI Dokument, das das Hauptmenü enthält")]
    private UIDocument HauptMenu;

    [SerializeField] [Tooltip("Das UI Dokument, das das Einstellungsmenü enthält")]
    private UIDocument EinstellungMenu;

    [SerializeField] [Tooltip("Das UI Dokument, das das Tastaturmenü enthält")]
    private UIDocument TastaturMenu;

    [SerializeField] [Tooltip("Das UI Dokument, das die Spielanleitung enthält")]
    private UIDocument SpielanleitungMenu;

    private VisualElement rootMainMenu;
    private VisualElement rootSettingsMenu;
    private VisualElement rootTutorialMenu;
    private VisualElement rootTastaturMenu;

    private Button SpielanleitungButton; //btnSpielanleitung
    private Button tastaturButton; //btnTastatur
    private Button settingsButton; //btnSettings
    private Button startButton; //btnStart

    private Toggle checkTon; //checkAudio
    private Toggle checkGeist; //checkGhost
    private Button saveEinstellungButton; //btnSave
    private Button returnFromEinstellungButton; //btnReturn

    private Button returnFromTutorialButton; //btnReturn
    private Button returnFromTastaturButton; //btnReturn

    /// <summary>
    /// Hält eine Instanz von GameState (Singleton)
    /// </summary>
    private GameState gameState;

    /// <summary>
    /// Hält eine Instanz von EventManager (Singleton)
    /// </summary>
    private EventManager eventManager;

    /// <summary>
    /// Belegt die Menüelemente und Events beim Start
    /// </summary>
    private void OnEnable()
    {
        // Nicht in Start verschieben, da ansonsten die Referenzen nicht existieren!

        // Variablen belegen
        rootMainMenu = HauptMenu.rootVisualElement;
        rootSettingsMenu = EinstellungMenu.rootVisualElement;
        rootTutorialMenu = SpielanleitungMenu.rootVisualElement;
        rootTastaturMenu = TastaturMenu.rootVisualElement;

        SpielanleitungButton = rootMainMenu.Q<Button>("btnSpielanleitung");
        tastaturButton = rootMainMenu.Q<Button>("btnTastatur");
        settingsButton = rootMainMenu.Q<Button>("btnSettings");
        startButton = rootMainMenu.Q<Button>("btnStart");

        checkTon = rootSettingsMenu.Q<Toggle>("checkAudio");
        checkGeist = rootSettingsMenu.Q<Toggle>("checkGhost");
        saveEinstellungButton = rootSettingsMenu.Q<Button>("btnSave");
        returnFromEinstellungButton = rootSettingsMenu.Q<Button>("btnReturn");

        returnFromTutorialButton = rootTutorialMenu.Q<Button>("btnReturn");

        returnFromTastaturButton = rootTastaturMenu.Q<Button>("btnReturn");

        // Click Event belegen
        if (SpielanleitungButton != null)
        {
            if (SpielanleitungButton != null)
                SpielanleitungButton.clickable.clicked += () =>
                {
                    Debug.Log("SpielanleitungButton wurde gedrückt");
                    mainToTutorial();
                };
        }

        if (tastaturButton != null)
        {
            if (tastaturButton != null)
                tastaturButton.clickable.clicked += () =>
                {
                    Debug.Log("tastaturButton wurde gedrückt");
                    mainToTastatur();
                };
        }

        if (settingsButton != null)
        {
            if (settingsButton != null)
                settingsButton.clickable.clicked += () =>
                {
                    Debug.Log("settingsButton wurde gedrückt");
                    mainToSettings();
                };
        }

        if (startButton != null)
        {
            if (startButton != null)
                startButton.clickable.clicked += () =>
                {
                    Debug.Log("StartButton wurde gedrückt");
                    returnToGame();
                };
        }

        checkTon?.RegisterValueChangedCallback(e => { Debug.Log(e.newValue.ToString()); });

        checkGeist?.RegisterValueChangedCallback(e => { Debug.Log(e.newValue.ToString()); });

        if (saveEinstellungButton != null)
        {
            saveEinstellungButton.clickable.clicked += () => { Debug.Log("saveButton wurde gedrückt"); };
        }

        if (returnFromEinstellungButton != null)
        {
            returnFromEinstellungButton.clickable.clicked += () =>
            {
                Debug.Log("returnFromEinstellungButton wurde gedrückt");
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

        if (returnFromTastaturButton != null)
        {
            returnFromTastaturButton.clickable.clicked += () =>
            {
                Debug.Log("returnFromTastaturButton wurde gedrückt");
                tastaturToMain();
            };
        }

        // Restliche Variablen belegen und Main aufrufen
        gameState = GameState.Instance();
        eventManager = EventManager.Instance();

        // Sicherstellen, dass das Hauptmenü von überall erreichbar ist
        eventManager.MainMenueCallEvent += showMain;

        showMain();
    }

    /// <summary>
    /// Löst verbundene EventListener
    /// </summary>
    private void OnDisable()
    {
        // Sicherstellen, dass das Hauptmenü nicht mehr von überall erreichbar ist
        eventManager.MainMenueCallEvent -= showMain;

        gameState = null;
        eventManager = null;
    }

    /// <summary>
    /// Zeigt das Hauptmenü an und ist der Haupteinstieg in das Spiel
    /// </summary>
    private void showMain()
    {
        eventManager.CallPauseGameTime();
        gameState.MainMenuVisible = true;

        SoundManager.Instance.PlayMenueMusic();

        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Verzweigung zur Spielanleitung
    /// </summary>
    private void mainToTutorial()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.Flex;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Verzweigung zur Tastaturbelegung
    /// </summary>
    private void mainToTastatur()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.Flex;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Verzweigung zu den Einstellungen
    /// </summary>
    private void mainToSettings()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Rücksprung zum Hauptmenü aus der Spielanleitung
    /// </summary>
    private void tutorialToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Rücksprung zum Hauptmenü aus der Tastaturbelegung
    /// </summary>
    private void tastaturToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Rücksprung zum Hauptmenü aus den Einstellungen
    /// </summary>
    private void settingToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Schließt das Menü und räumt auf. Es wird das Event
    /// CloseMainMenuEvent geworfen.
    /// </summary>
    private void returnToGame()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;

        SoundManager.Instance.PlayBackgroundMusic();

        gameState.MainMenuVisible = false;
        eventManager.CloseMainMenu();
    }
}