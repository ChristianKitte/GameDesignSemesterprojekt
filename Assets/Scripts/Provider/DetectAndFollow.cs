using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DetectAndFollow : MonoBehaviour
{
    [Tooltip("Der zu detektierende Layer")] [SerializeField]
    private LayerMask layer;

    [Tooltip("Ein Stellvertreterobjekt am Ursprung der Detektion")] [SerializeField]
    private GameObject directionOrigin;

    [Tooltip("Ein Stellvertreterobjekt in Vorwärtsrichtung der Detektion")] [SerializeField]
    private GameObject directionTarget;

    [Tooltip("Der Radius, in dem ein detektierbares Objekt wahrgenommen werden soll")] [SerializeField]
    private float detectionRadius = 5.0f;

    [Tooltip("Die Schnelligkeit, mit der sich das Objekt dem detektierten Objekt annähert")] [SerializeField]
    private float detectionApproximate = 10.0f;

    // Update is called once per frame
    void Update()
    {
        directionOrigin.transform.Rotate(0, 1, 0);

        Vector3 origin = directionOrigin.transform.position;
        Vector3 direction = directionOrigin.transform.position - directionTarget.transform.position;

        Debug.DrawRay(origin, direction * detectionRadius, Color.red, 1.0f);

        if (Physics.Raycast(origin, direction, out RaycastHit lastHit, detectionRadius, layer))
        {
            // In Richtung des Ziels drehendw
            transform.LookAt(lastHit.transform);

            // Sich in Richtung des Ziels bewegen
            var step = detectionApproximate * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastHit.transform.position, step);
        }
    }
}