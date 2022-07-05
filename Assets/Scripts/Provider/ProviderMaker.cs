using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// Der ProviderMaker kapselt die Erstellung neuer Provider unter Berücksichtigung der
/// übergebenen ProviderDimension.  
/// </summary>
public class ProviderMaker : MonoBehaviour
{
    [Tooltip("Das Muster eines Providers für Bananen")] [SerializeField]
    private GameObject BananaProvider;

    [Tooltip("Das Muster eines Providers, um vor Wänden geschützt zu sein")] [SerializeField]
    private GameObject WallProtectionProvider;

    [Tooltip("Das Muster eines Providers für den Schutz vor Geistern")] [SerializeField]
    private GameObject GhostProtectionProvider;

    [Tooltip("Das Muster des Targets für das Levelziel des Spiels")] [SerializeField]
    private GameObject TargetProvider;

    [Tooltip("Das Muster des NPC Ghost")] [SerializeField]
    private GameObject NPCGhostProvider;

    /// <summary>
    /// Erzeugt alle Provider eines Levels
    /// </summary>
    /// <param name="providerDimension">Eine Instanz von ProviderDimensions</param>
    public void CreateProvider(ProviderDimension providerDimension)
    {
        var countOfBananaProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountBananaProvider,
            providerDimension.MaxCountBananaProvider + 1);

        var countOfGhostProtectionProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountGhostProtectionProvider,
            providerDimension.MaxCountGhostProtectionProvider + 1);

        var countOfWallProtectionProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountWallProtectionProvider,
            providerDimension.MaxCountWallProtectionProvider + 1);

        var countOfNPCGhostProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountNPCGhostProvider,
            providerDimension.MaxCountNPCGhostProvider + 1);

        for (int i = 0; i < countOfBananaProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            float randomValue = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(BananaProvider);
            newProvider.GetComponent<SetRandomValue>()?.SetRandomNumber(
                providerDimension.MinValueBananaProvider,
                providerDimension.MaxValueBananaProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        for (int i = 0; i < countOfGhostProtectionProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(GhostProtectionProvider);
            newProvider.GetComponent<SetRandomValue>()?.SetRandomNumber(
                providerDimension.MinValueGhostProtectionProvider,
                providerDimension.MaxValueGhostProtectionProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        for (int i = 0; i < countOfWallProtectionProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(WallProtectionProvider);
            newProvider.GetComponent<SetRandomValue>()?.SetRandomNumber(
                providerDimension.MinValueWallProtectionProvider,
                providerDimension.MaxValueWallProtectionProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        for (int i = 0; i < countOfNPCGhostProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(NPCGhostProvider);
            newProvider.GetComponent<SetRandomValue>()?.SetRandomNumber(
                providerDimension.MinValueNPCGhostProvider,
                providerDimension.MaxValueNPCGhostProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        CreateTargetProvider(providerDimension);
    }

    /// <summary>
    /// Erzeugt einen neuen Target Provider
    /// </summary>
    /// <param name="providerDimension">Eine Instanz von ProviderDimensions</param>
    public void CreateTargetProvider(ProviderDimension providerDimension)
    {
        float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
            providerDimension.rightStartXPositionProvider);

        float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
            providerDimension.topStartZPositionProvider);

        var newProvider = Instantiate(TargetProvider);
        newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);

        GameState.Instance().currentTarget = newProvider;
    }
}