using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Der ProviderMaker kapselt die Erstellung neuer Provider unter Berücksichtigung der
/// übergebenen ProviderDimension.  
/// </summary>
public class ProviderMaker : MonoBehaviour
{
    [Tooltip("Das Muster eines Providers für Lebenspunkte")] [SerializeField]
    private GameObject LiveProvider;

    [Tooltip("Das Muster eines Providers, um durch Wänder durchgehen zu können")] [SerializeField]
    private GameObject GoThroughProvider;

    [Tooltip("Das Muster eines Providers für den Schutz vor Geistern")] [SerializeField]
    private GameObject GhostProtectionProvider;

    [Tooltip("Das Muster des Targets für das Ende des Spiels")] [SerializeField]
    private GameObject TargetProvider;

    /// <summary>
    /// Erzeugt alle Provider eines Levels
    /// </summary>
    /// <param name="providerDimension">Eine Instanz von ProviderDimensions</param>
    public void CreateProvider(ProviderDimension providerDimension)
    {
        var countOfLiveProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountLiveProvider,
            providerDimension.MaxCountLiveProvider + 1);

        var countOfGhostProtectionProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountGhostProtectionProvider,
            providerDimension.MaxCountGhostProtectionProvider + 1);

        var countOfGoThroughProvider = RandomNumberGenerator.GetInt32(
            providerDimension.MinCountGoThroughProvider,
            providerDimension.MaxCountGoThroughProvider + 1);

        for (int i = 0; i < countOfLiveProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(LiveProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        for (int i = 0; i < countOfGhostProtectionProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(GhostProtectionProvider);
            newProvider.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
        }

        for (int i = 0; i < countOfGoThroughProvider; i++)
        {
            float randomXFloatNumber = Random.Range(providerDimension.leftStartXPositionProvider,
                providerDimension.rightStartXPositionProvider);

            float randomZFloatNumber = Random.Range(providerDimension.bottomStartZPositionProvider,
                providerDimension.topStartZPositionProvider);

            var newProvider = Instantiate(GoThroughProvider);
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
    }
}