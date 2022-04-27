using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Die WallMaker erzeugt Wände, welche sich in zufälliger Richtung bewegen. Die Ausmaße der Wände und deren
/// Geschwindigkeit werden hierbei zufällig zwischen den übergebenen Min und Maxwerten generiert.
/// </summary>
public class WallMaker : MonoBehaviour
{
    [Tooltip("Ein Muster der Wand in Vorwärtsrichtung")] [SerializeField]
    private GameObject wallObject;

    private void Update()
    {
    }

    /// <summary>
    /// Erzeugt ein neues Wall Objekt anhand des hinterlegten GameObjects und setzt alle Paramtere
    /// anhand der übergebenen WallDimension
    /// </summary>
    /// <param name="wallDimension">Ein Record, der alle zur Erstellung benötigten Rahmenparameter
    /// enthält</param>
    public void createWall(WallDimension wallDimension)
    {
        if (wallObject != null)
        {
            float newDirectionAsEuler = GetDirections();

            float newMoveSpeed = (float)RandomNumberGenerator.GetInt32(
                (int)wallDimension.MinMoveSpeed,
                (int)wallDimension.MaxMoveSpeed + 1);

            var newWallObject = Instantiate(wallObject);

            newWallObject.transform.Rotate(0f, newDirectionAsEuler, 0f);
            newWallObject.transform.position = new Vector3(203, 0.5f, 191.4f);

            newWallObject.gameObject.GetComponent<MoveForward>().SetMoveSpeed(newMoveSpeed);

            newWallObject.gameObject.GetComponent<MoveForward>().SetRestrictionArea(
                wallDimension.leftDestroyXPosition,
                wallDimension.topDestroyZPosition,
                wallDimension.rightDestroyXPosition,
                wallDimension.bottomDestroyZPosition);
        }
    }

    /// <summary>
    /// Gibt zufallsbasiert einen Eulerwinkel zurück, der einen der in Directions existierenden
    /// Enumerationen entspricht (0, 90, 180, 270 Grad).
    /// </summary>
    /// <returns>Der Eulerwinkel</returns>
    private float GetDirections()
    {
        Directions direction = (Directions)RandomNumberGenerator.GetInt32(1, 5);
        float eulerAngle = 0;

        switch (direction)
        {
            case Game.Enumerations.Directions.forward:
                eulerAngle = 0f;
                break;
            case Game.Enumerations.Directions.Right:
                eulerAngle = 90f;
                break;
            case Game.Enumerations.Directions.backward:
                eulerAngle = 180f;
                break;
            case Game.Enumerations.Directions.Left:
                eulerAngle = 270f;
                break;
        }

        return eulerAngle;
    }
}