using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enumerations;
using Unity.VisualScripting;
using UnityEditor.ProBuilder;
using UnityEngine.PlayerLoop;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private Directions MoveDirection = Directions.forward;

    [SerializeField] private float MinMoveSpeed = 5.0f;
    [SerializeField] private float MaxMoveSpeed = 10.0f;

    [SerializeField] private float MinHeight = 0.5f;
    [SerializeField] private float MaxHeight = 4.0f;

    [SerializeField] private float MinThickness = 0.25f;
    [SerializeField] private float MaxThickness = 1.5f;

    [SerializeField] private float MinLength = 0.5f;
    [SerializeField] private float MaxLength = 20.0f;

    private Vector3 directionVector;
    private float MoveSpeed = 0.0f;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        ensureLimits();
        setRandomParameter();
        setMoveDirection(MoveDirection);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionVector * MoveSpeed * Time.deltaTime, Space.World);
    }

    private void ensureLimits()
    {
        if (MinMoveSpeed <= 5.0f) MinMoveSpeed = 5.0f;
        if (MaxMoveSpeed >= 10.0f) MaxMoveSpeed = 10.0f;

        if (MinHeight <= 0.5f) MinHeight = 0.5f;
        if (MaxHeight >= 4.0f) MaxHeight = 4.0f;

        if (MinThickness <= 0.25f) MinThickness = 0.25f;
        if (MaxThickness >= 1.5f) MaxThickness = 1.5f;

        if (MinLength <= 0.5f) MinLength = 0.5f;
        if (MaxLength >= 20.0f) MaxLength = 20.0f;
    }

    private void setMoveDirection(Directions MoveDirection)
    {
        switch (MoveDirection)
        {
            case Directions.forward:
                directionVector = Vector3.forward;
                break;
            case Directions.backward:
                directionVector = Vector3.back;
                break;
            case Directions.Left:
                directionVector = Vector3.left;
                transform.Rotate(Vector3.up, 90.0f);
                break;
            case Directions.Right:
                directionVector = Vector3.right;
                transform.Rotate(Vector3.up, 90.0f);
                break;
        }
    }

    private void setRandomParameter()
    {
        //MoveDirection = (Directions)Random.Range(1, 4);
        MoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
        var height = Random.Range(MinHeight, MaxHeight);
        var thickness = Random.Range(MinThickness, MaxThickness);
        var length = Random.Range(MinLength, MaxLength);
    }
}