using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enumerations;
using UnityEngine;

/// <summary>
/// Der Event Manager verbindet als Singleton lose Event Publisher mit Event Listener, ohne
/// feste Referenzen.
/// </summary>
public class EventManager
{
    /// <summary>
    /// Einzige Instanz von EventManager
    /// </summary>
    private static EventManager _eventManager;

    /// <summary>
    /// Gibt die Instanz des EventManagers zurück (Singleton)
    /// </summary>
    /// <returns>Die einzige Instanz von EventManager</returns>
    public static EventManager Instance()
    {
        if (_eventManager == null)
        {
            _eventManager = new EventManager();
        }

        return _eventManager;
    }

    #region Events

    /// <summary>
    /// Wird jede Spielsekunde ausgelöst
    /// </summary>
    public event Action SecondTick;

    /// <summary>
    /// Wird ausgelöst, wenn der Timer gestoppt und auf 0 gesetzt wird
    /// </summary>
    public event Action<int> ResetTimer;

    /// <summary>
    /// Wird ausgelöst, wenn der Timer beim alten Stand weiterläuft wird
    /// </summary>
    public event Action StartTimer;

    /// <summary>
    /// Signalisiert eine Löschaufforderung an alle Provider
    /// </summary>
    public event Action DestroyProvider;

    /// <summary>
    /// Signalisiert die Kollision mit einem Objekt
    /// </summary>
    public event Action<CollisionObjektTyp, int> CollisionDetected;

    /// <summary>
    /// Signalisiert die Aufforderung das Spiel zu pausieren
    /// </summary>
    public event Action PauseGamePlayCallEvent;

    /// <summary>
    /// Signalisiert die Aufforderung das Spiel zu startet
    /// </summary>
    public event Action StartGamePlayCallEvent;

    /// <summary>
    /// Signalisiert die Aufforderung das Spiel fortzuführen
    /// </summary>
    public event Action ResumeGamePlayCallEvent;

    /// <summary>
    /// Signalisiert die Aufforderung zur Anzeige des Hauptmenüs
    /// </summary>
    public event Action MainMenueCallEvent;

    /// <summary>
    /// Startet ein neues Spiel
    /// </summary>
    public event Action StartNewGameEvent;

    /// <summary>
    /// Startet ein neues Level
    /// </summary>
    public event Action StartNewLevelEvent;

    #endregion

    #region TriggerCallFunctions

    /// <summary>
    /// Löst das Event SecondTick aus
    /// </summary>
    public void SendSecondTick()
    {
        SecondTick?.Invoke();
    }

    /// <summary>
    /// Löst das Ereignis ResetTimer aus
    /// </summary>
    /// <param name="newStartTimeInSeconds">Die Laufzeit, für die der Timer eingestellt wird</param>
    public void SendResetTimer(int newStartTimeInSeconds)
    {
        ResetTimer?.Invoke(newStartTimeInSeconds);
    }

    /// <summary>
    /// Löst das Ereignis StartTimer aus
    /// </summary>   
    public void SendStartTimer()
    {
        StartTimer?.Invoke();
    }

    /// <summary>
    /// Löst das Ereignis DestroyProvider aus
    /// </summary>
    public void SendKillSignalForProvider()
    {
        DestroyProvider?.Invoke();
    }

    /// <summary>
    /// Löst das Ereignis CollisionDetected aus
    /// </summary>
    /// <param name="objektTyp">Der Objekttyp der Kollision</param>
    /// <param name="objectValue">Der Wert der Kollision</param>
    public void SendCollisionMessage(CollisionObjektTyp objektTyp, int objectValue)
    {
        CollisionDetected?.Invoke(objektTyp, objectValue);
    }

    /// <summary>
    /// Wechselt zum Hauptmenü des Spiels
    /// </summary>
    public void ShowMainMenue()
    {
        MainMenueCallEvent?.Invoke();
    }

    /// <summary>
    /// Stop den aktuellen Spiellauf
    /// </summary>
    public void StopGamePlay()
    {
        PauseGamePlayCallEvent?.Invoke();
    }

    /// <summary>
    /// Startet den aktuellen Spiellauf (entfaltet die gleiche Wirkung wie ResumeGamePlay)
    /// </summary>
    public void StartGamePlay()
    {
        StartGamePlayCallEvent?.Invoke();
    }

    /// <summary>
    /// Führt den aktuellen Spiellauf weiter
    /// </summary>
    public void ResumeGamePlay()
    {
        ResumeGamePlayCallEvent?.Invoke();
    }

    /// <summary>
    /// Startet ein neues Spiel
    /// </summary>
    public void StartNewGame()
    {
        StartNewGameEvent?.Invoke();
    }

    /// <summary>
    /// Startet ein neues Level
    /// </summary>
    /*public void StartNewLevel()
    {
        StartNewLevelEvent?.Invoke();
    }*/

    #endregion
}