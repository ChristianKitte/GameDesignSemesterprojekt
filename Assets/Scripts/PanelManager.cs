using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private UIDocument HauptMenu;
    [SerializeField] private UIDocument EinstellungMenu;
    [SerializeField] private UIDocument TastaturMenu;
    [SerializeField] private UIDocument SpielanleitungMenu;

    private VisualElement rootMainMenu;
    private VisualElement rootSettingsMenu;
    private VisualElement rootTutorialMenu;
    private VisualElement rootTastaturMenu;

    private Button SpielanleitungButton; //btnSpielanleitung
    private Button tastaturButton; //btnTastatur
    private Button settingsButton; //btnSettings
    private Button startButton; //btnStart

    private Toggle checkTon; //checkAudio
    private Toggle checkGost; //checkGhost
    private Button saveEinstellungButton; //btnSave
    private Button returnFromEinstellungButton; //btnReturn

    private Button returnFromTutorialButton; //btnReturn
    private Button returnFromTastaturButton; //btnReturn


    private GameState gameStateInstance;

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
        checkGost = rootSettingsMenu.Q<Toggle>("checkGhost");
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

        checkGost?.RegisterValueChangedCallback(e => { Debug.Log(e.newValue.ToString()); });

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
        gameStateInstance = GameState.Instance();
        EventManager.Instance().MainMenueCallEvent += () => { showMain(); };
        showMain();
    }

    /// <summary>
    /// Zeigt das Hauptmenü an und ist der Haupteinstieg in das Spiel
    /// </summary>
    private void showMain()
    {
        EventManager.Instance().StopGamePlay();
        gameStateInstance.MainMenuVisible = true;

        //GameState.Instance().GameIsPaused = true;

        SoundManager.Instance.PlayMenueMusic();

        /*
        if (GameState.Instance().GameIsPlaying || GameState.Instance().GameLevelDlgIsShowing)
        {
            startButton.text = "Zurück";
        }*/

        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void mainToTutorial()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.Flex;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void mainToTastatur()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.Flex;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void mainToSettings()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.Flex;
    }

    private void tutorialToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void tastaturToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void settingToMain()
    {
        rootMainMenu.style.display = DisplayStyle.Flex;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;
    }

    private void returnToGame()
    {
        rootMainMenu.style.display = DisplayStyle.None;
        rootTutorialMenu.style.display = DisplayStyle.None;
        rootTastaturMenu.style.display = DisplayStyle.None;
        rootSettingsMenu.style.display = DisplayStyle.None;

        SoundManager.Instance.PlayBackgroundMusic();

        gameStateInstance.MainMenuVisible = false;

        if (!gameStateInstance.GameIsPlaying)
        {
            // Der erste Start des Spiels. Dies wird vermerkt
            gameStateInstance.GameIsPlaying = true;
            // zurück zu Spiel
            EventManager.Instance().ResumeGamePlay();
            // Spiel starten
            EventManager.Instance().StartGamePlay();
        }
        else
        {
            // nur zurück zum Spiel
            EventManager.Instance().ResumeGamePlay();
        }

        /*
        if (gameStateInstance.GameIsPlaying && gameStateInstance.GameIsPaused &&
            !gameStateInstance.GameLevelDlgIsShowing)
        {
            gameStateInstance.GameIsPaused = false;

            // zurück zum Spiel
            EventManager.Instance().ResumeGamePlay();
        }
        else if (gameStateInstance.GameIsPlaying && !gameStateInstance.GameIsPaused &&
                 !gameStateInstance.GameLevelDlgIsShowing)
        {
        }
        else if (!gameStateInstance.GameIsPlaying && gameStateInstance.GameIsPaused &&
                 !gameStateInstance.GameLevelDlgIsShowing)
        {
            gameStateInstance.GameIsPlaying = true;
            gameStateInstance.GameIsPaused = false;

            // Neues Spiel starten
            EventManager.Instance().StartGamePlay();
            EventManager.Instance().StartNewGame();
        }*/
    }
}