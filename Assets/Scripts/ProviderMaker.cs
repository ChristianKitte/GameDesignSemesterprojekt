using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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

    }
}