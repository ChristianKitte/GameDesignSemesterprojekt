using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer_ : MonoBehaviour
{
    [SerializeField] private int TimeInMinute;
    [SerializeField] private TMP_Text TextComponent;

    private float currentTime;
    private string currentTimeText;

    private bool timerIsActive = false;

    private void OnEnable()
    {
        //EventManager.Instance().ResetTimer += resetTimer;
        //EventManager.Instance().StartTimer += startTimer;
        //EventManager.Instance().PauseTimer += stopTimer;
        //EventManager.Instance().PauseTimer += pauseTimer;
    }

    private void OnDisable()
    {
        //EventManager.Instance().ResetTimer -= resetTimer;
        //EventManager.Instance().StartTimer -= startTimer;
        //EventManager.Instance().PauseTimer -= stopTimer;
        //EventManager.Instance().PauseTimer -= pauseTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        //startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive)
        {
            currentTime = currentTime - Time.deltaTime;
        }

        TimeSpan restzeit = TimeSpan.FromSeconds(currentTime);
        currentTimeText = $"{restzeit.Minutes.ToString()}:{restzeit.Seconds.ToString()}";

        TextComponent.SetText(currentTimeText);

        /* Für Debugzwecke
        if (currentTime <= 0)
        {
            resetTimer(5);  
            startTimer();
        }
        */
    }

    /// <summary>
    /// Setzt den Timer zurück und legt die Laufzeit neu fest. Der Timer wird nicht gestartet.
    /// </summary>
    /// <param name="newStartTimeInSeconds">Die Laufzeit, für die der Timer eingestellt wird</param>
    void resetTimer(int newStartTimeInSeconds)
    {
        currentTime = newStartTimeInSeconds;
        timerIsActive = true;
    }

    /// <summary>
    /// Startet den Timer mit der aktuell eingestellten Laufzeit
    /// </summary>
    void startTimer()
    {
        timerIsActive = true;
    }

    void pauseTimer()
    {
        timerIsActive = false;
    }

    void stopTimer()
    {
        timerIsActive = false;
    }
}