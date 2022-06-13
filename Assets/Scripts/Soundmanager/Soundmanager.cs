using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Tooltip("Eine AudioSource, die zum Abspielen von Effekten verwendet werden soll")] [SerializeField]
    private AudioSource effectPlayer;

    [Tooltip("Eine AudioSource, die zum Abspielen von Musik als Hintergrund verwendet werden soll")] [SerializeField]
    private AudioSource backgroundPlayer;

    [Tooltip("Eine AudioSource, die zum Abspielen von Musik als Hintergrund verwendet werden soll")] [SerializeField]
    private AudioSource menuPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        effectPlayer.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic()
    {
        backgroundPlayer.Play();
        menuPlayer.Stop();
    }

    public void PlayMenueMusic()
    {
        backgroundPlayer.Stop();
        menuPlayer.Play();
    }
}