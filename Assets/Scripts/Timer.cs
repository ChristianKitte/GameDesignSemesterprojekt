using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int TimeInMinute;
    [SerializeField] private TMP_Text TextComponent;

    private float currentTime;
    private string currentTimeText;

    private bool timerIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        startTimer();
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
        Console.WriteLine(currentTimeText);

        if (currentTime <= 0)
        {
            startTimer();
        }
    }

    void startTimer()
    {
        currentTime = TimeInMinute * 60;
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