using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    private int currentSecondsOld;
    private int currentSecondsNew;
    private float currentTime;
    private bool timerIsActive = true;

    private void Start()
    {
        EventManager.Instance().ResetTimer += ResetTimer;
        EventManager.Instance().PauseTimer += PauseTimer;
        EventManager.Instance().StartTimer += StartTimer;
    }

    private void OnDisable()
    {
        EventManager.Instance().ResetTimer -= ResetTimer;
        EventManager.Instance().PauseTimer -= PauseTimer;
        EventManager.Instance().StartTimer -= StartTimer;
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

    void ResetTimer()
    {
        currentTime = 0;
        currentSecondsNew = 0;
        currentSecondsOld = 0;
    }

    void PauseTimer()
    {
        timerIsActive = false;
    }

    void StartTimer()
    {
        timerIsActive = true;
    }
}