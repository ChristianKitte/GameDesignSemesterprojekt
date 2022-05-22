using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaker : MonoBehaviour
{
    [Tooltip("Das GameObject des Spielers")] [SerializeField]
    private GameObject PlayerCharacter;

    [Tooltip("Das Gameobjekt des Geistes")] [SerializeField]
    private GameObject GhostCharacter;

    public void SetPlayer(PlayerDimension playerDimension)
    {
        float randomXFloatNumber = Random.Range(playerDimension.leftStartXPositionPlayer,
            playerDimension.rightStartXPositionPlayer);

        float randomZFloatNumber = Random.Range(playerDimension.bottomStartZPositionPlayer,
            playerDimension.topStartZPositionPlayer);
        
        PlayerCharacter.transform.position = new Vector3(randomXFloatNumber, 1.0f, randomZFloatNumber);
    }
}