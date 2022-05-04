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
    /// Wird ausgelöst, wenn die Spielzeit beendet ist
    /// </summary>
    public event Action TimeOver;

    /// <summary>
    /// Wird ausgelöst, wenn der Timer gestoppt und auf 0 gesetzt wird
    /// </summary>
    public event Action ResetTimer;

    /// <summary>
    /// Wird ausgelöst, wenn der Timer mit aktuellen Stand angehalten wird
    /// </summary>
    public event Action PauseTimer;

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
    /// Löst das Event GameFinished aus
    /// </summary>
    public void SendTimeOver()
    {
        TimeOver?.Invoke();
    }

    /// <summary>
    /// Löst das Ereignis ResetTimer aus
    /// </summary>
    public void SendResetTimer()
    {
        ResetTimer?.Invoke();
    }

    /// <summary>
    /// Löst das Ereignis PauseTimer aus
    /// </summary>   
    public void SendPauseTimer()
    {
        PauseTimer?.Invoke();
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

    #endregion
}