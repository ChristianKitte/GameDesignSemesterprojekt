using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Kapselt als Singleton die Steuerung aller Sounds des Spieles
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Die einzige Instanz des Spiels
    /// </summary>
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

    /// <summary>
    /// Spielt den übergebenen Clip einmalig als Effekt in 2D ab
    /// </summary>
    /// <param name="clip">Der abzuspielende Clip</param>
    public void PlayEffect(AudioClip clip)
    {
        effectPlayer.PlayOneShot(clip);
    }

    /// <summary>
    /// Aktiviert den Background Player und deaktiviert den Menü Player 
    /// </summary>
    public void PlayBackgroundMusic()
    {
        backgroundPlayer.Play();
        menuPlayer.Stop();
    }

    /// <summary>
    /// Aktiviert den Menü Player und deaktiviert den Background Player 
    /// </summary>
    public void PlayMenueMusic()
    {
        backgroundPlayer.Stop();
        menuPlayer.Play();
    }
}