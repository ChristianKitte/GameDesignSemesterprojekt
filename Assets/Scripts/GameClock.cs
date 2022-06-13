using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Berechnet die Zeit und wirft bei Wechsel der Sekunde ein Event vom
/// Typ EventManager.Instance().SendSecondTick() 
/// </summary>
public class GameClock : MonoBehaviour
{
    private int currentSecondsOld;
    private int currentSecondsNew;
    private float currentTime;
    private bool timerIsActive = true;

    private void Start()
    {
        EventManager.Instance().ResetTimer += ResetTimer;
        EventManager.Instance().StartTimer += StartTimer;
        EventManager.Instance().PauseGamePlayCallEvent += PauseTimer;
        EventManager.Instance().ResumeGamePlayCallEvent += ResumeTimer;
    }

    private void OnDisable()
    {
        EventManager.Instance().ResetTimer -= ResetTimer;
        EventManager.Instance().StartTimer -= StartTimer;
        EventManager.Instance().PauseGamePlayCallEvent -= PauseTimer;
        EventManager.Instance().ResumeGamePlayCallEvent -= ResumeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive)
        {
            currentTime = currentTime + Time.deltaTime;
            currentSecondsNew = TimeSpan.FromSeconds(currentTime).Seconds;

            if (currentSecondsNew != currentSecondsOld)
            {
                currentSecondsOld = currentSecondsNew;
                EventManager.Instance().SendSecondTick();
            }
        }
    }

    /// <summary>
    /// Hält die Zählung an und setzt alle Werte auf den Urspung zurück
    /// </summary>
    /// <param name="newStartTimeInSeconds">Wird nicht verwendet</param>
    void ResetTimer(int newStartTimeInSeconds)
    {
        timerIsActive = false;

        currentTime = 0;
        currentSecondsNew = 0;
        currentSecondsOld = 0;
    }

    /// <summary>
    /// Startet den Timer
    /// </summary>
    void StartTimer()
    {
        timerIsActive = true;
    }

    /// <summary>
    /// Hält die Zählung an
    /// </summary>
    void PauseTimer()
    {
        timerIsActive = false;
    }

    /// <summary>
    /// Führt die Zählung weiter
    /// </summary>
    void ResumeTimer()
    {
        timerIsActive = true;
    }
}