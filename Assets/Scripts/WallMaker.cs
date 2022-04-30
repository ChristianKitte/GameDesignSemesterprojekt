using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;

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
    /// anhand der übergebenen WallDimension. Ein Wall Objekt setzt sich hierbei aus einzelnen und
    /// aneinander gereihten Komponenten (Units) zusammen.
    /// </summary>
    /// <param name="wallDimension">Ein Record, der alle zur Erstellung benötigten Rahmenparameter
    /// enthält</param>
    public void createWall(WallDimension wallDimension)
    {
        if (wallObject != null)
        {
            Random.InitState(DateTime.Now.Second);
            float newMoveSpeed = Random.Range(
                wallDimension.MinMoveSpeed,
                wallDimension.MaxMoveSpeed);

            Random.InitState(DateTime.Now.Second);
            float newWallLength = Random.Range(
                wallDimension.MinLengthUnit,
                wallDimension.MaxLengthUnit);

            List<GameObject> newWallObjects = new List<GameObject>();

            for (int i = 0; i < newWallLength + 1; i++)
            {
                newWallObjects.Add(Instantiate(wallObject));
            }

            GetDirectionAndStartpoint(newWallObjects, wallDimension);

            foreach (var newWallObject in newWallObjects)
            {
                MoveForward moveForwardScript = newWallObject.gameObject.GetComponent<MoveForward>();

                moveForwardScript.SetMoveSpeed(newMoveSpeed);

                moveForwardScript.SetRestrictionArea(
                    wallDimension.leftDestroyXPosition,
                    wallDimension.topDestroyZPosition,
                    wallDimension.rightDestroyXPosition,
                    wallDimension.bottomDestroyZPosition);
            }
        }
    }

    /// <summary>
    /// Legt für die übergebene Liste an Wall Objekt Segmenten eine zufällige Richtung, Geschwindigkeit und
    /// Dimensionierung fest. Hierbei wird über den übergebenen WallDimension Record das aktuelle Level
    /// berücksichtigt.  
    /// </summary>
    /// <param name="newWallObjects">Liste einzelner Wall Objecte, die als Einheit eine neues Wall Object darstellen</param>
    /// <param name="wallDimension">Ein WallDimension Record für das aktuelle Wall Objekt</param>
    private void GetDirectionAndStartpoint(List<GameObject> newWallObjects, WallDimension wallDimension)
    {
        Directions direction = (Directions)RandomNumberGenerator.GetInt32(1, 5);
        //Transform curTransform = newWallObject.transform;

        float eulerAngle = 0;

        float randomXFloatNumber = Random.Range(wallDimension.leftStartXPosition,
            wallDimension.rightStartXPosition);

        float randomZFloatNumber = Random.Range(wallDimension.bottomStartZPosition,
            wallDimension.topStartZPosition);

        float newWallLength = Random.Range(
            wallDimension.MinLengthUnit,
            wallDimension.MaxLengthUnit);

        float curOffset = 0;
        foreach (var newWallObject in newWallObjects)
        {
            Transform curTransform = newWallObject.transform;

            switch (direction)
            {
                case Game.Enumerations.Directions.forward:
                    eulerAngle = 0f;

                    curTransform.position = new Vector3(
                        randomXFloatNumber,
                        wallDimension.HeightStartYPosition,
                        wallDimension.bottomStartZPosition - curOffset);
                    break;
                case Game.Enumerations.Directions.Right:
                    eulerAngle = 90f;

                    curTransform.position = new Vector3(
                        wallDimension.leftStartXPosition - curOffset,
                        wallDimension.HeightStartYPosition,
                        randomZFloatNumber);
                    break;
                case Game.Enumerations.Directions.backward:
                    eulerAngle = 180f;

                    curTransform.position = new Vector3(
                        randomXFloatNumber,
                        wallDimension.HeightStartYPosition,
                        wallDimension.topStartZPosition + curOffset);
                    break;
                case Game.Enumerations.Directions.Left:
                    eulerAngle = 270f;

                    curTransform.position = new Vector3(
                        wallDimension.rightStartXPosition + curOffset,
                        wallDimension.HeightStartYPosition,
                        randomZFloatNumber);
                    break;
            }

            curOffset = curOffset + 1.0f;
            curTransform.Rotate(0f, eulerAngle, 0f);
        }
    }
}