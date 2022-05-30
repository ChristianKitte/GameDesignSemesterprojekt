using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [Tooltip("Der verwendete Slider zur Anzeige des Live Wertes")] [SerializeField]
    private GameObject liveBar;

    [Tooltip("Der verwendete Slider zur Anzeige des GoThrough Wertes")] [SerializeField]
    private GameObject goThroughBar;

    [Tooltip("Der verwendete Slider zur Anzeige des GhostProtection Wertes")] [SerializeField]
    private GameObject ghostProtectionBar;

    [Tooltip("Der verwendete Slider zur Anzeige der verbleibenden Spielzeit")] [SerializeField]
    private GameObject playTimeBar;

    private SliderController liveBarSlider;
    private SliderController goThroughBarSlider;
    private SliderController ghostProtectionBarSlider;
    private SliderController playTimeBarSlider;

    private void Awake()
    {
        liveBarSlider = liveBar.GetComponent<SliderController>();
        goThroughBarSlider = goThroughBar.GetComponent<SliderController>();
        ghostProtectionBarSlider = ghostProtectionBar.GetComponent<SliderController>();
        playTimeBarSlider = playTimeBar.GetComponent<SliderController>();
    }

    public SliderController GetLiveBar()
    {
        return liveBarSlider;
    }

    public SliderController GetGoThroughBar()
    {
        return goThroughBarSlider;
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