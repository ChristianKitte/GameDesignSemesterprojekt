using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enumerations;
using UnityEngine;

/// <summary>
/// Wenn das übergeordnete GameObjekt eine Kollision mit einem Objekt mit dem im "Tag" festgelegten
/// TargetTag hat, wird eine Nachricht vom Typ CollisionDetected mit dem festgelegten Objekttyp
/// versendet. Anschließend wird das Übergeordnete Objekt zerstört.
/// </summary>
public class DeleteOnCollision : MonoBehaviour
{
    [Tooltip("Festgelegtes Target für die Kollisionsmeldung")] [SerializeField]
    private string TargetTag = "Player";

    [Tooltip("Festgelegtes Objekt welches als Typ der Kollision gemeldet wird")] [SerializeField]
    private CollisionObjektTyp collisionObjectTyp = CollisionObjektTyp.DefaultValue;

    [Tooltip("Gibt den bei einer Kollision zu übermittelnden Wert, wenn keine SetRandomValue Komponente vorhanden ist")]
    [SerializeField]
    private int defaultValueOnMissingValue = 0;

    /// <summary>
    /// Ermöglicht das Überschreiben des defaultValueOnMissingValue aus anderen Skripten heraus
    /// </summary>
    /// <param name="value">Der zu setzende Wert</param>
    public void SetDefaultValue(int value)
    {
        this.defaultValueOnMissingValue = value;
    }

    /// <summary>
    /// Wird ausgelöst, sofern eie Kollision stattfindet
    /// </summary>
    /// <param name="collision">Das Kontextobjekt zum Ereignis</param>
    private void OnCollisionEnter(Collision collision)
    {
        var currentValue = GetComponent<SetRandomValue>()?.CurrentValue;
        if (!currentValue.HasValue)
        {
            currentValue = defaultValueOnMissingValue;
        }

        if (collision.gameObject.CompareTag(TargetTag))
        {
            EventManager.Instance().SendCollisionMessage(collisionObjectTyp, currentValue.Value);

            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}