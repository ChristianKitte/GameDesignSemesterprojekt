using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager _eventManager;

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

    public void SendKillSignalForProvider()
    {
        DestroyProvider?.Invoke();
    }
}