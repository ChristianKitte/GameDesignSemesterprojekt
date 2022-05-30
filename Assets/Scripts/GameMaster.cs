using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] private TMP_Text TextComponent;

    [SerializeField] private SliderManager sliderManager;

    #region Einstellungen und Variablen

    [Tooltip("Rundenzeit in Sekunden")] [SerializeField]
    private int TimePerRoundInSeconds;

    [Tooltip("Die zu verwendende WallFactory")] [SerializeField]
    private GameObject WallFactory;

    [Tooltip("Die zu verwendende ProviderFactory")] [SerializeField]
    private GameObject ProviderFactory;

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

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an LiveProvider")] [SerializeField]
    private int MinCountLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an LiveProvider")] [SerializeField]
    private int MaxCountLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MinCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an GhostProtectionProvider")] [SerializeField]
    private int MaxCountGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an GoThroughProvider")] [SerializeField]
    private int MinCountGoThroughProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an GoThroughProvider")] [SerializeField]
    private int MaxCountGoThroughProvider;

    [Tooltip("Die linke Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float leftStartXPositionProvider;

    [Tooltip("Die obere (forward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float topStartZPositionProvider;

    [Tooltip("Die rechte Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float rightStartXPositionProvider;

    [Tooltip("Die untere (backward) Position, ab der ein Provider plaziert werden darf")] [SerializeField]
    private float bottomStartZPositionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für LiveProvider")] [SerializeField]
    private int MinValueLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für LiveProvider")] [SerializeField]
    private int MaxValueLiveProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für GhostProtectionProvider")] [SerializeField]
    private int MinValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für GhostProtectionProvider")] [SerializeField]
    private int MaxValueGhostProtectionProvider;

    [Tooltip("Referenzwert in Level1 - Die minimale Anzahl an Punkten für GoThroughProvider")] [SerializeField]
    private int MinValueGoThroughProvider;

    [Tooltip("Referenzwert in Level1 - Die maximale Anzahl an Punkten für GoThroughProvider")] [SerializeField]
    private int MaxValueGoThroughProvider;

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

    private EventManager eventManager;
    private GameState gameState;

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente enabled wird
    /// </summary>
    private void OnEnable()
    {
        eventManager = EventManager.Instance();
        gameState = GameState.Instance();

        EventManager.Instance().SecondTick += HandleSecondEvent;
        EventManager.Instance().CollisionDetected += HandleCollisionDetectedEvent;
    }

    /// <summary>
    /// Wird von Unity aufgerufen, wenn die Komponente disabled wird
    /// </summary>
    private void OnDisable()
    {
        eventManager = null;
        gameState = null;

        EventManager.Instance().SecondTick -= HandleSecondEvent;
        EventManager.Instance().CollisionDetected -= HandleCollisionDetectedEvent;
    }

    // Start is called before the first frame update
    void Start()
    {
        startNewGame();
    }

    /// <summary>
    /// Startet ein neues Spiel vom Anfang an. Alle bis dahin gehaltene Werte gehen verloren. Der Spieler
    /// erhält seinen anfänglich Bestand an Lebenspunkten
    /// </summary>
    private void startNewGame()
    {
        gameState = GameState.Instance().reset();
        gameState.collectedLiveProviderPoints = 0;

        sliderManager.GetLiveBar().SetBarMaximum(200);
        sliderManager.GetLiveBar().SetCurrentValue(gameState.collectedLiveProviderPoints);

        gameState.currentLevel = 1;
        gameState.defaultSecondsToPlayPerLevel = TimePerRoundInSeconds;

        startGameLevel();
    }

    /// <summary>
    /// Beendet das aktuelle Spiel
    /// </summary>
    private void endGame()
    {
    }

    /// <summary>
    /// Pausiert das aktuelle Spiel ohne Änderung an den Spielständen
    /// </summary>
    private void pauseGame()
    {
        //Pausensignal senden und in ... wechseln
    }

    /// <summary>
    /// Führt das aktuelle Spiel auf Basis des aktuellen Standes weiter aus
    /// </summary>
    private void resumeGame()
    {
        //PausenEndesignal senden und zurück ins Spiel wechseln
    }

    /// <summary>
    /// Startet das aktuell gesetzte Level mit den aktuell geltenden Einstellungen
    /// </summary>
    private void startGameLevel()
    {
        // Hier fehlt noch die auf das aktuelle Level basierende Anpassung

        var secondsToPlay = gameState.defaultSecondsToPlayPerLevel;
        gameState.remainingSecondsToPlayLevel = secondsToPlay;

        sliderManager.GetPlayTimeBar().SetBarMaximum(secondsToPlay);
        sliderManager.GetPlayTimeBar().SetCurrentValue(secondsToPlay);

        // GoThrough wird zurück gesetzt 
        sliderManager.GetGoThroughBar().SetBarMaximum(0);
        sliderManager.GetGoThroughBar().SetCurrentValue(0);

        // GhostProtection wird zurück gesetzt
        sliderManager.GetGhostProtectionBar().SetBarMaximum(0);
        sliderManager.GetGoThroughBar().SetCurrentValue(0);

        // Provider und Player auf das aktuelle Level anpassen - Aktuell wird currentLevel nicht ausgewertet
        ProviderFactory.GetComponent<ProviderMaker>()?.CreateProvider(getNewProviderDimension(currentLevel));
        GetComponent<CharacterMaker>()?.SetPlayer(getNewPlayerDimension(currentLevel));

        // Spiel starten
        EventManager.Instance().SendResetTimer(secondsToPlay);
        EventManager.Instance().SendStartTimer();
    }

    /// <summary>
    /// Beendet das aktuell gesetzte Level. Beträgt der aktuelle Punktestand mehr als 0 Punkte, so
    /// wird das nächste Level gespielt. Beträgt der aktuelle Punktestand gleich oder weniger als
    /// 0 Punkte, so wird das vorherige Level aktiviert.
    /// </summary>
    private void finishCurrentGameLevel()
    {
        EventManager.Instance().SendKillSignalForProvider();
        if (gameState.collectedLiveProviderPoints < 0) // Das Level wurde verloren
        {
            runPreviousLevel();
        }
        else
        {
            runNextLevel();
        }
    }

    /// <summary>
    /// Setzt den Levelzähler um eins hoch und startet das neue Level
    /// </summary>
    private void runNextLevel()
    {
        gameState.currentLevel++;
        startGameLevel();
    }

    /// <summary>
    /// Setzt den Levelzähler um eins herunter und startet das neue Level.
    /// Ist das aktuelle Level das erste Level, so bleibt der Levelzähler unverändert.
    /// </summary>
    private void runPreviousLevel()
    {
        if (gameState.currentLevel == 1)
        {
            startGameLevel();
        }
        else
        {
            gameState.currentLevel--;
            startGameLevel();
        }
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
            case CollisionObjektTyp.LiveProvider:
                sliderManager.GetLiveBar().CountUp(value);
                gameState.collectedLiveProviderPoints = sliderManager.GetLiveBar().GetCurrentValue();

                break;
            case CollisionObjektTyp.GoThroughProvider:
                if (sliderManager.GetGoThroughBar().GetCurrentValue() < value)
                {
                    var newValue = value - sliderManager.GetGoThroughBar().GetCurrentValue();

                    sliderManager.GetGoThroughBar().SetBarMaximum(newValue);
                    sliderManager.GetGoThroughBar().SetCurrentValue(newValue);
                }

                gameState.collectedGoThroughProviderSeconds =
                    sliderManager.GetGoThroughBar().GetCurrentValue();

                break;
            case CollisionObjektTyp.GhostProtectionProvider:
                if (sliderManager.GetGhostProtectionBar().GetCurrentValue() < value)
                {
                    var newValue = value - sliderManager.GetGhostProtectionBar().GetCurrentValue();

                    sliderManager.GetGhostProtectionBar().SetBarMaximum(newValue);
                    sliderManager.GetGhostProtectionBar().SetCurrentValue(newValue);
                }

                gameState.collectedGhostProtectionProviderSeconds =
                    sliderManager.GetGhostProtectionBar().GetCurrentValue();

                break;
            case CollisionObjektTyp.MovingWall:
                sliderManager.GetLiveBar().CountDown(value);
                gameState.collectedLiveProviderPoints = sliderManager.GetLiveBar().GetCurrentValue();

                break;
            case CollisionObjektTyp.Ghost:

                break;
            case CollisionObjektTyp.MainTarget:
                finishCurrentGameLevel();

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

        if (sliderManager.GetGoThroughBar().GetCurrentValue() > 0)
        {
            sliderManager.GetGoThroughBar().CountDown(1);
            gameState.collectedGoThroughProviderSeconds =
                sliderManager.GetGoThroughBar().GetCurrentValue();
        }

        // Spiel beenden oder verbleibende Spielzeit aktualisieren
        if (gameState.remainingSecondsToPlayLevel <= 0)
        {
            finishCurrentGameLevel();
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
            WallFactory.GetComponent<WallMaker>().createWall(getNewWallDimension(currentLevel));
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
            MinCountLiveProvider = MinCountLiveProvider,
            MaxCountLiveProvider = MaxCountLiveProvider,
            MinCountGhostProtectionProvider = MinCountGhostProtectionProvider,
            MaxCountGhostProtectionProvider = MaxCountGhostProtectionProvider,
            MinCountGoThroughProvider = MinCountGoThroughProvider,
            MaxCountGoThroughProvider = MaxCountGoThroughProvider,
            leftStartXPositionProvider = leftStartXPositionProvider,
            topStartZPositionProvider = topStartZPositionProvider,
            rightStartXPositionProvider = rightStartXPositionProvider,
            bottomStartZPositionProvider = bottomStartZPositionProvider,
            MinValueLiveProvider = MinValueLiveProvider,
            MaxValueLiveProvider = MaxValueLiveProvider,
            MinValueGhostProtectionProvider = MinValueGhostProtectionProvider,
            MaxValueGhostProtectionProvider = MaxValueGhostProtectionProvider,
            MinValueGoThroughProvider = MinValueGoThroughProvider,
            MaxValueGoThroughProvider = MaxValueGoThroughProvider
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