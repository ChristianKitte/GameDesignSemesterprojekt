using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class SetRandomPoints : MonoBehaviour
{
    [SerializeField] private GameObject Provider;
    [SerializeField] private TMP_Text TextComponent;
    [SerializeField] private int MinValue = 0;
    [SerializeField] private int MaxValue = 0;

    private float MinRealValue = 0.0f;
    private float MaxRealValue = 0.0f;

    private int livePoint;

    void Start()
    {
        MinRealValue = (float)MinValue / 10;
        MaxRealValue = (float)MaxValue / 10;
        
        livePoint = (int)(Random.Range(MinRealValue, MaxRealValue) * 10);
        TextComponent.SetText(livePoint.ToString());
    }
}