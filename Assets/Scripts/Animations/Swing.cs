using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Lässt das unterliegende GameObject um die Y-Achse rotieren
/// </summary>
public class Swing : MonoBehaviour
{
    [Tooltip("Die Geschwindigkeit des Swingens")] [SerializeField]
    private float frequenz = 1.0f;

    [Tooltip("Die Amplitude")] [SerializeField]
    private float amplitude = 0.5f;

    private bool swing = false;
    private Vector3 initPos;

    private float salt;

    private void Start()
    {
        salt = Random.value;
        initPos = transform.position;

        var waitFor = Random.Range(0, 2);

        StartCoroutine(wait(waitFor));
    }

    void Update()
    {
        if (swing)
        {
            transform.position =
                new Vector3(initPos.x, Mathf.Sin(Time.time * frequenz * salt) * amplitude + initPos.y, initPos.z);
        }
    }

    private IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        swing = true;
    }
}