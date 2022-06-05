using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SliderManager : MonoBehaviour
{
    [Tooltip("Der verwendete Slider zur Anzeige des Banana Wertes")] [SerializeField]
    private GameObject bananaBar;

    [Tooltip("Der verwendete Slider zur Anzeige des WallProtection Wertes")] [SerializeField]
    private GameObject wallProtectionBar;

    [Tooltip("Der verwendete Slider zur Anzeige des GhostProtection Wertes")] [SerializeField]
    private GameObject ghostProtectionBar;

    [Tooltip("Der verwendete Slider zur Anzeige der verbleibenden Spielzeit")] [SerializeField]
    private GameObject playTimeBar;

    private SliderController bananaBarSlider;
    private SliderController wallProtectionBarSlider;
    private SliderController ghostProtectionBarSlider;
    private SliderController playTimeBarSlider;

    private void Awake()
    {
        bananaBarSlider = bananaBar.GetComponent<SliderController>();
        wallProtectionBarSlider = wallProtectionBar.GetComponent<SliderController>();
        ghostProtectionBarSlider = ghostProtectionBar.GetComponent<SliderController>();
        playTimeBarSlider = playTimeBar.GetComponent<SliderController>();
    }

    public SliderController GetBananaBar()
    {
        return bananaBarSlider;
    }

    public SliderController GetWallProtectionBar()
    {
        return wallProtectionBarSlider;
    }

    public SliderController GetGhostProtectionBar()
    {
        return ghostProtectionBarSlider;
    }

    public SliderController GetPlayTimeBar()
    {
        return playTimeBarSlider;
    }
}