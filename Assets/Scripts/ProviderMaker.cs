using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProviderMaker : MonoBehaviour
{
    [Tooltip("Das Muster eines Providers für Lebenspunkte")] [SerializeField]
    private GameObject LiveProvider;

    [Tooltip("Das Muster eines Providers, um durch Wänder durchgehen zu können")] [SerializeField]
    private GameObject GoThroughProvider;

    [Tooltip("Das Muster eines Providers für den Schutz vor Geistern")] [SerializeField]
    private GameObject GhostProtectionProvider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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
    }
}