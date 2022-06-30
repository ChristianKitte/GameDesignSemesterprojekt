using UnityEngine;

// https://www.habrador.com/tutorials/programming-patterns/
// https://github.com/thangchung/clean-code-dotnet
// Template Method Pattern
// https://en.wikipedia.org/wiki/Template_method_pattern

/// <summary>
/// Ein Abstracte Klasse zur Definition einer Prompt Nachricht
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    [Tooltip("Der bei Annäherung anzuzeigende Text")] [SerializeField]
    public string promptMessage = "keine Nachricht";

    [Tooltip("Die Farbe des anzuzeigenden Textes")] [SerializeField]
    public Color promptColor = Color.white;

    // Wird vom Spieler aufgerufen
    public void BaseInteract()
    {
        Interact();
    }

    // Muss von den Ableitungen überschrieben werden
    protected virtual void Interact()
    {
    }
}