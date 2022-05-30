using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Lässt das unterliegende GameObject um die Y-Achse rotieren
/// </summary>
public class RotateYAxis : MonoBehaviour
{
    [Tooltip("Die Geschwindigkeit und Richtung der Rotation")] [SerializeField]
    private float rotationSpeed = 1.0f;

    [Tooltip("Eine zufällige Startzeit zwischen 0 und 2 Sekunde erzeugen")] [SerializeField]
    private bool randomStart = true;

    private bool turn = false;

    private void Start()
    {
        var waitFor = Random.Range(0, 2);
        StartCoroutine(wait(waitFor));
    }

    void Update()
    {
        if (turn)
        {
            transform.Rotate(new Vector3(0, 1 * rotationSpeed, 0));
        }
    }

    private IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        turn = true;
    }
}