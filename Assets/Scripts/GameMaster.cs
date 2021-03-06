using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace.UI;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
using TMPro;

/// <summary>
/// Der GameMaster steuert den Spielverlauf und hält die aktuellen Zustände. Es handelt sich
/// um ein Singleton Objekt.
/// </summary>
public class GameMaster : MonoBehaviour
{
    #region Dialog und UI

    [SerializeField] private LevelDialogManager levelDialogManager;
    [SerializeField] private PointShowManager pointShowManager;

    #endregion

    #region Einstellungen und Variablen

    [SerializeField] private TMP_Text TextComponent;

    [Tooltip("Rundenzeit in Sekunden")] [SerializeField]
    private int TimePerRoundInSeconds;

    [Tooltip("Der zu verwendende SliderManager")] [SerializeField]
    private SliderManager sliderManager;

    [Tooltip("Die zu verwendende WallFactory")] [SerializeField]
    private GameObject wallFactory;

    [FormerlySerializedAs("ProviderFactory")] [Tooltip("Die zu verwendende ProviderFactory")] [SerializeField]
    private GameObject providerFactory;

    [Tooltip("Das zur Anzeige des Levels zu nutzende TextMeshPro Element")] [SerializeField]
    private TextMeshProUGUI levelString;

    private int playedSecondsSinceStart;

    private string currentTimeText;

    private int currentLevel = 1;

    #endregion

    #region Einstellungen für WallDimension

    [Tooltip("Die linke Position, ab der ein WallObject startet")] [SerializeField]
    private float leftStartXPosition = 203f;

    [Tooltip("Die obere (forward) Position, ab der ein WallObject startet")] [SerializeField]
    private float topStartZPosition = 191f;

    [Tooltip("Die rechte Position, ab der ein WallObject startet")] [SerializeField]
    private float rightStartXPosition = 203f;

    [Tooltip("Die untere (backward) Position, ab der ein WallObject startet")] [SerializeField]
    private float bottomStartZPosition = 191f;

    [Tooltip("Die Höhe, ab der ein WallObject startet")] [SerializeField]
    private float HeightStartYPosition = 0.5f;

    [Tooltip("Die linke Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float leftDestroyXPosition = 130f;

    [Tooltip("Die obere (forward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float topDestroyZPosition = 330f;

    [Tooltip("Die rechte Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float rightDestroyXPosition = 280f;

    [Tooltip("Die untere (backward) Position, ab der ein WallObject sich zerstört")] [SerializeField]
    private float bottomDestroyZPosition = 101f;

    [Tooltip("Referenzwert in Level1 - Minimale Wartezeit zwischen neuen Mauern in Sekunden")] [SerializeField]
    private int MinWallIntervallInSeconds;

    [Tooltip("Referenzwert in Level1 - Maximale Wartezeit zwischen neuen Mauern in Sekunden")] [SerializeField]
    private int MaxWallIntervallInSeconds;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Geschwindigkeit der Wälle")] [SerializeField]
    private float StartMinMoveSpeed = 5.0f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Geschwindigkeit der Wälle")] [SerializeField]
    private float StartMaxMoveSpeed = 10.0f;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Höhe der Wälle")] [SerializeField]
    private float StartMinHeight = 0.5f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Höhe der Wälle")] [SerializeField]
    private float StartMaxHeight = 4.0f;

    [Tooltip("Referenzwert in Level1 - Minimaler Anfangswert der Dicke der Wälle")] [SerializeField]
    private float StartMinThickness = 0.25f;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Dicke der Wälle")] [SerializeField]
    private float StartMaxThickness = 1.5f;

    [Tooltip(
        "Referenzwert in Level1 - Minimaler Anfangswert der Länge der Wälle in Units. ein UnNit entspricht 0.5 Meter")]
    [SerializeField]
    private int StartMinLengthUnits = 5;

    [Tooltip("Referenzwert in Level1 - Maximaler Endwert der Länge der Wälle in Units. ein Unit entspricht 0.5 Meter")]
    [SerializeField]
    private int StartMaxLengthUnit = 40;

    #endregion

    #region Einstellungen für ProviderDimension

    [Tooltip("Die linke Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float leftStartXPositionProvider;

    [Tooltip("Die obere (forward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float topStartZPositionProvider;

    [Tooltip("Die rechte Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float rightStartXPositionProvider;

    [Tooltip("Die untere (backward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float bottomStartZPositionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an BananaProvider")] [SerializeField]
    private int MinCountBananaProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an BananaProvider")] [SerializeField]
    private int MaxCountBananaProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MinCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MaxCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an WallProtetionProvider")] [SerializeField]
    private int MinCountWallProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an WallProtectionProvider")] [SerializeField]
    private int MaxCountWallProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an NPCGhost im Spiel")] [SerializeField]
    private int MinCountNPCGhostProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an NPCGhost im Spiel")] [SerializeField]
    private int MaxCountNPCGhostProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Bananen für BananaProvider")] [SerializeField]
    private int MinValueBananaProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Bananen für BananaProvider")] [SerializeField]
    private int MaxValueBananaProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Sekunden für GhostProtectionProvider")] [SerializeField]
    private int MinValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Sekunden für GhostProtectionProvider")] [SerializeField]
    private int MaxValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Sekunden für WallProtectionProvider")] [SerializeField]
    private int MinValueWallProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Sekunden für WallProtectionProvider")] [SerializeField]
    private int MaxValueWallProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Sekunden für WallProtectionProvider")] [SerializeField]
    private int MinValueNPCGhostProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Sekunden für WallProtectionProvider")] [SerializeField]
    private int MaxValueNPCGhostProvider;

    #endregion

    #region Einstellugnen für PlayerDimension

    [Tooltip("Die linke Position, ab der ein Player plaziert werden darf")] [SerializeField]
    private float leftStartXPositionPlayer;

    [Tooltip("Die obere (forward) Position, ab der ein Player plaziert werden darf")] [SerializeField]
    private float topStartZPositionPlayer;

    [Tooltip("Die rechte Position, ab der ein Player plaziert werden darf")] [SerializeField]
    private float rightStartXPositionPlayer;

    [Tooltip("Die untere (backward) Position, ab der ein Player plaziert werden darf")] [SerializeField]
    private float bottomStartZPositionPlayer;

    #endregion

    /// <summary>
    /// Hält eine Instanz von GameState (Singleton)
    /// </summary>
    private GameState gameState;

    /// <summary>
    /// Hält eine Instanz von EventManager (Singleton)
    /// </summary>
    private EventManager eventManager;

    private PointShowManager PointHightlightManager;

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente enabled wird
    /// </summary>
    private void OnEnable()
    {
        gameState = GameState.Instance();
        eventManager = EventManager.Instance();

        eventManager.CloseMainMenuEvent += handleReturnFromMainMenu;
        EventManager.Instance().SecondTick += HandleSecondEvent;
        EventManager.Instance().CollisionDetected += HandleCollisionDetectedEvent;

        PointHightlightManager = GetComponent<PointShowManager>();
    }

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente disabled wird
    /// </summary>
    private void OnDisable()
    {
        eventManager.CloseMainMenuEvent -= handleReturnFromMainMenu;
        EventManager.Instance().SecondTick -= HandleSecondEvent;
        EventManager.Instance().CollisionDetected -= HandleCollisionDetectedEvent;

        gameState = null;
        eventManager = null;
    }

    /// <summary>
    /// Handelt das Ergebnis / die Rückkehr aus dem Hauptmenü. Es kann hierbei drei Optionen
    /// geben:
    /// 1)  Es wird ein ganz neues Spiel gestartet
    /// 2)  Das Hauptmenü wurd im Spiel aufgerufen
    /// 3)  Das Level wurde beendet und man hat sich gegen eine Fortführung entschieden (in diesen Fall
    ///     erscheint das Hauptmenü)
    /// </summary>
    private void handleReturnFromMainMenu()
    {
        if (gameState.GameIsPlaying)
        {
            // Das Spiel ist im Gange. 
            if (gameState.LevelBreak)
            {
                // Es wurde gerade ein Level beendet und aus dem Leveldialog wurde das Hauptmenü aufgerufen.
                // Der aktuelle Status ist nach wie vor LevelBreak. Bei der Fortführung muss mit dem aktuellen
                // Level eine neue Spielrunde gestartet werden. Hierbei wird der Level auf False zurück gesetzt.

                startGameLevel();
            }
            else
            {
                // Es war eine Unterbrechung in einer laufenden Spielrunde. Das Spiel muss somit
                // nur fortgeführt werden

                eventManager.CallResumeGameTime();
            }
        }
        else
        {
            // Das Spiel beginnt neu. Entweder weil die Seite gerade betreten wurde, oder aber
            // eine neues Spiel gestartet wurde (was jedoch aktuell nicht implementiert ist).
            // Alle notwendigen Schritte hierfür müssen ausgeführt werden

            eventManager.CallResumeGameTime();

            startNewGame();
        }
    }

    /// <summary>
    /// Startet ein neues Spiel vom Anfang an. Alle bis dahin gehaltene Werte gehen verloren. Der Spieler
    /// erhält seinen anfänglich Bestand an Lebenspunkten
    /// </summary>
    private void startNewGame()
    {
        gameState = GameState.Instance().reset();

        gameState.GameIsPlaying = true;
        gameState.LevelBreak = false;

        gameState.currentLevel = 1;
        gameState.collectedBananaProviderBanana = 0;
        gameState.defaultSecondsToPlayPerLevel = TimePerRoundInSeconds;

        sliderManager.GetBananaBar().SetCurrentValue(gameState.collectedBananaProviderBanana);

        startGameLevel();
    }

    /// <summary>
    /// Konfiguriert und startet das aktuell gesetzte Level mit den aktuell geltenden Einstellungen
    /// </summary>
    private void startGameLevel()
    {
        // nur hier erfolgt der Übergang zum Status LevelBreak = False
        gameState.LevelBreak = false;
        gameState.LevelMenuVisible = false;
        eventManager.CallResumeGameTime();

        // Hier fehlt noch die auf das aktuelle Level basierende Anpassung

        if (levelString != null)
        {
            levelString.SetText($"Level {gameState.currentLevel}");
        }

        // Bananen werden auf 0 gesetzt, sofern der Spieler weniger als 0 Bananen hat
        if (gameState.collectedBananaProviderBanana < 0)
        {
            gameState.collectedBananaProviderBanana = 0;
            sliderManager.GetBananaBar().SetCurrentValue(0);
        }

        // Spielzeit einstellen
        var secondsToPlay = gameState.defaultSecondsToPlayPerLevel;
        gameState.remainingSecondsToPlayLevel = secondsToPlay;

        // PlayTime Bar setzen
        sliderManager.GetPlayTimeBar().SetBarMaximum(secondsToPlay);
        sliderManager.GetPlayTimeBar().SetCurrentValue(secondsToPlay);

        // WallProtection wird zurück gesetzt 
        gameState.collectedWallProtectionProviderSeconds = 0;
        sliderManager.GetWallProtectionBar().SetBarMaximum(0);
        sliderManager.GetWallProtectionBar().SetCurrentValue(0);

        // GhostProtection wird zurück gesetzt
        gameState.collectedGhostProtectionProviderSeconds = 0;
        sliderManager.GetGhostProtectionBar().SetBarMaximum(0);
        sliderManager.GetGhostProtectionBar().SetCurrentValue(0);

        // Provider und Player auf das aktuelle Level anpassen - Aktuell wird currentLevel nicht ausgewertet
        providerFactory.GetComponent<ProviderMaker>()?.CreateProvider(getNewProviderDimension(currentLevel));
        //GetComponent<CharacterMaker>()?.SetPlayer(getNewPlayerDimension(currentLevel));

        // Spiel starten
        //GameState.Instance().GameIsPaused = false;

        //EventManager.Instance().ResumeGamePlay();
        eventManager.SendResetTimer(secondsToPlay);
        eventManager.SendStartTimer();
    }

    /// <summary>
    /// Beendet das aktuell gesetzte Level. Ist der LevelResultTyp Winner, so wird in das nächste Level
    /// gewechselt. Ist der Typ Loser, so wird in das vorhergehende Level gewechselt. Ist in diesen Fall
    /// das erste Level bereits erreicht, so verbleibt man in diesen Level.
    /// </summary>
    private void finishCurrentGameLevel(LevelResultTyp levelResultTyp)
    {
        if (!gameState.LevelMenuVisible)
        {
            eventManager.SendKillSignalForProvider();

            if (levelResultTyp == LevelResultTyp.Loser) // Das Level wurde verloren
            {
                if (gameState.currentLevel == 1)
                {
                    gameState.currentLevel = 1;
                }
                else
                {
                    gameState.currentLevel--;
                }

                levelDialogManager.Show(
                    "Das war leider nichts! Lass es uns nochmal versuchen...",
                    () => startGameLevel(),
                    () => cancelAction(),
                    "Bin dabei",
                    "Nicht heute");
            }
            else if (levelResultTyp == LevelResultTyp.Winner)
            {
                gameState.currentLevel++;

                levelDialogManager.Show(
                    "Gut gemacht! Lass uns weiter machen...",
                    () => startGameLevel(),
                    () => cancelAction(),
                    "Auf jeden Fall",
                    "Nicht heute");
            }
        }
    }

    /// <summary>
    /// Handelt den Fall, dass im Level Dialog auf die Aufforderung, weiter zu spielen,
    /// negativ reagiert wurde. In diesem Fall wird das Hauptmenü aufgerufen. Hierbei
    /// bleibt der Status LevelBreak erhalten! 
    /// </summary>
    private void cancelAction()
    {
        eventManager.ShowMainMenue();
    }

    #region Game Events

    /// <summary>
    /// Wird aufgerufen, wenn ein GameObjekt mit der Komponente DeleteOnCollision oder SendMessageOnCollision
    /// eine Kollision mit einem in TargetTag der Komponente festgelegten GameObjekt hat
    /// </summary>
    /// <param name="collisionType">Der Typ der Kollision</param>
    /// <param name="value">Der dem Objekt zugeordnete Wert</param>
    private void HandleCollisionDetectedEvent(CollisionObjektTyp collisionType, int value)
    {
        switch (collisionType)
        {
            case CollisionObjektTyp.BananaProvider:
                gameState.collectedBananaProviderBanana += value;
                PointHightlightManager.showPoints(value);

                sliderManager.GetBananaBar().CountUp(value);

                break;
            case CollisionObjektTyp.WallProtectionProvider:
                gameState.collectedWallProtectionProviderSeconds += value;

                if (sliderManager.GetWallProtectionBar().GetCurrentValue() < value)
                {
                    var newValue = value - sliderManager.GetWallProtectionBar().GetCurrentValue();

                    sliderManager.GetWallProtectionBar().SetBarMaximum(newValue);
                    sliderManager.GetWallProtectionBar().SetCurrentValue(newValue);
                }

                gameState.collectedWallProtectionProviderSeconds =
                    sliderManager.GetWallProtectionBar().GetCurrentValue();

                PointHightlightManager.showHint("Schutz vor den Wänden",
                    gameState.collectedWallProtectionProviderSeconds);

                break;
            case CollisionObjektTyp.GhostProtectionProvider:
                gameState.collectedGhostProtectionProviderSeconds += value;

                if (sliderManager.GetGhostProtectionBar().GetCurrentValue() < value)
                {
                    var newValue = value - sliderManager.GetGhostProtectionBar().GetCurrentValue();

                    sliderManager.GetGhostProtectionBar().SetBarMaximum(newValue);
                    sliderManager.GetGhostProtectionBar().SetCurrentValue(newValue);
                }

                gameState.collectedGhostProtectionProviderSeconds =
                    sliderManager.GetGhostProtectionBar().GetCurrentValue();

                PointHightlightManager.showHint("Schutz vor den Geistern",
                    gameState.collectedGhostProtectionProviderSeconds);

                break;
            case CollisionObjektTyp.MovingWall:
                if (gameState.collectedWallProtectionProviderSeconds <= 0)
                {
                    gameState.collectedBananaProviderBanana -= value;
                    PointHightlightManager.showPoints(value * -1);

                    sliderManager.GetBananaBar().CountDown(value);
                }

                break;
            case CollisionObjektTyp.Ghost:
                if (gameState.collectedGhostProtectionProviderSeconds <= 0)
                {
                    gameState.collectedBananaProviderBanana -= value;
                    PointHightlightManager.showPoints(value * -1);

                    sliderManager.GetBananaBar().CountDown(value);
                }

                break;
            case CollisionObjektTyp.MainTarget:
                Debug.Log("Kollision Main Target");

                // nur hier erfolgt der Übergang zum Status LevelBreak = True
                gameState.LevelBreak = true;
                eventManager.CallPauseGameTime();

                if (sliderManager.GetBananaBar().GetCurrentValue() > 0)
                {
                    finishCurrentGameLevel(LevelResultTyp.Winner);
                }
                else
                {
                    finishCurrentGameLevel(LevelResultTyp.Loser);
                }

                break;
        }
    }

    /// <summary>
    /// Wird einmal je Spielsekunde durch das Event SecondTick des EventManagers aufgerufen
    /// </summary>
    private void HandleSecondEvent()
    {
        // Bar Anzeigen aktualisieren
        gameState.remainingSecondsToPlayLevel--;
        sliderManager.GetPlayTimeBar().CountDown(1, false);

        if (sliderManager.GetGhostProtectionBar().GetCurrentValue() > 0)
        {
            sliderManager.GetGhostProtectionBar().CountDown(1);
            gameState.collectedGhostProtectionProviderSeconds =
                sliderManager.GetGhostProtectionBar().GetCurrentValue();
        }

        if (sliderManager.GetWallProtectionBar().GetCurrentValue() > 0)
        {
            sliderManager.GetWallProtectionBar().CountDown(1);
            gameState.collectedWallProtectionProviderSeconds =
                sliderManager.GetWallProtectionBar().GetCurrentValue();
        }

        // Spiel beenden oder verbleibende Spielzeit aktualisieren
        if (gameState.remainingSecondsToPlayLevel <= 0)
        {
            finishCurrentGameLevel(LevelResultTyp.Loser);
        }
        else
        {
            TimeSpan restzeit = TimeSpan.FromSeconds(gameState.remainingSecondsToPlayLevel);
            sliderManager.GetPlayTimeBar().SetValueText(
                $"{restzeit.Minutes.ToString()}:{restzeit.Seconds.ToString()}");
        }

        // Wände erzeugen
        int curWallIntervall = getNewWallIntervall(gameState.currentLevel);
        for (int i = 0; i < curWallIntervall; i++)
        {
            wallFactory.GetComponent<WallMaker>().createWall(getNewWallDimension(currentLevel));
        }
    }

    #endregion

    #region Hilfsfunktionen

    /// <summary>
    /// Gibt einen Zeitpunkt in Sekunden zurück. in der eine neue Wand erscheinen soll. Der Zeitpunkt befindet
    /// sich zwischen den auf Basis der Vorgaben und des aktuellen Levels festgelegten Minimalen und maximalen Wert.
    /// </summary>
    /// <param name="currentLevel">Der aktuelle Level</param>
    /// <returns>Die Zeit in Sekunden, wenn die nächste Wand erscheinen soll</returns>
    private int getNewWallIntervall(int currentLevel)
    {
        return RandomNumberGenerator.GetInt32(MinWallIntervallInSeconds, MaxWallIntervallInSeconds + 1);
    }

    /// <summary>
    /// Erzeugt auf Basis der anfänglichen Vorgaben und des aktuellen Levels einen neuen Record, der die minimalen
    /// und maximalen Abnessungen für die nächste zu generierende Wand enthält sowie deren räumliche Begrenzung.
    /// </summary>
    /// <param name="currentLevel">Das aktuelle Level</param>
    /// <returns>Die zur Generierung zu verwendenen Rahmenangaben für die Wandabmessungen als WallDimension</returns>
    private WallDimension getNewWallDimension(int currentLevel)
    {
        var newWallDimension = new WallDimension()
        {
            leftStartXPosition = leftStartXPosition,
            topStartZPosition = topStartZPosition,
            rightStartXPosition = rightStartXPosition,
            bottomStartZPosition = bottomStartZPosition,
            HeightStartYPosition = HeightStartYPosition,
            leftDestroyXPosition = leftDestroyXPosition,
            topDestroyZPosition = topDestroyZPosition,
            rightDestroyXPosition = rightDestroyXPosition,
            bottomDestroyZPosition = bottomDestroyZPosition,
            MinMoveSpeed = StartMinMoveSpeed,
            MaxMoveSpeed = StartMaxMoveSpeed,
            MinHeight = StartMinHeight,
            MaxHeight = StartMaxHeight,
            MinThickness = StartMinThickness,
            MaxThickness = StartMaxThickness,
            MinLengthUnit = StartMinLengthUnits,
            MaxLengthUnit = StartMaxLengthUnit
        };

        return newWallDimension;
    }

    /// <summary>
    /// Erzeugt auf Basis der anfänglichen Vorgaben und des aktuellen Levels einen neuen Record, der die minimalen
    /// und maximalen Plazierung für die im Level befindlichen Provider sowie deren Wertebereich enthält .
    /// </summary>
    /// <param name="currentLevel">Das aktuelle Level</param>
    /// <returns>Die zur Generierung zu verwendenen Rahmenangaben für die Provider als ProviderDimension</returns>
    private ProviderDimension getNewProviderDimension(int currentLevel)
    {
        var newProviderDimension = new ProviderDimension()
        {
            MinCountBananaProvider = MinCountBananaProvider,
            MaxCountBananaProvider = MaxCountBananaProvider,
            MinCountGhostProtectionProvider = MinCountGhostProtectionProvider,
            MaxCountGhostProtectionProvider = MaxCountGhostProtectionProvider,
            MinCountWallProtectionProvider = MinCountWallProtectionProvider,
            MaxCountWallProtectionProvider = MaxCountWallProtectionProvider,
            MinCountNPCGhostProvider = MinCountNPCGhostProvider,
            MaxCountNPCGhostProvider = MaxCountNPCGhostProvider,
            leftStartXPositionProvider = leftStartXPositionProvider,
            topStartZPositionProvider = topStartZPositionProvider,
            rightStartXPositionProvider = rightStartXPositionProvider,
            bottomStartZPositionProvider = bottomStartZPositionProvider,
            MinValueBananaProvider = MinValueBananaProvider,
            MaxValueBananaProvider = MaxValueBananaProvider,
            MinValueGhostProtectionProvider = MinValueGhostProtectionProvider,
            MaxValueGhostProtectionProvider = MaxValueGhostProtectionProvider,
            MinValueWallProtectionProvider = MinValueWallProtectionProvider,
            MaxValueWallProtectionProvider = MaxValueWallProtectionProvider,
            MinValueNPCGhostProvider = MinValueNPCGhostProvider,
            MaxValueNPCGhostProvider = MaxValueNPCGhostProvider
        };

        return newProviderDimension;
    }

    /// <summary>
    /// Erzeugt auf Basis der anfänglichen Vorgaben und des aktuellen Levels einen neuen Record, der die minimalen
    /// und maximalen Plazierung für den Player enthält .
    /// </summary>
    /// <param name="currentLevel">Das aktuelle Level</param>
    /// <returns>Die zur Generierung zu verwendenen Rahmenangaben für den Player als PlayerDimension</returns>
    private PlayerDimension getNewPlayerDimension(int currentLevel)
    {
        var getNewPlayerDimension = new PlayerDimension()
        {
            leftStartXPositionPlayer = leftStartXPositionPlayer,
            topStartZPositionPlayer = topStartZPositionPlayer,
            rightStartXPositionPlayer = rightStartXPositionPlayer,
            bottomStartZPositionPlayer = bottomStartZPositionPlayer,
        };

        return getNewPlayerDimension;
    }

    #endregion
}