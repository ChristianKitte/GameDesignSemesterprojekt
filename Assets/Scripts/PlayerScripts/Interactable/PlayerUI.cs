using UnityEngine;
using TMPro;

/// <summary>
/// Steuert die UI zur Anzeige von Interactable Aktivitäten 
/// </summary>
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    /// <summary>
    /// Aktualisiert den angezeigten Text
    /// </summary>
    /// <param name="promptMessage">Die anzuzeigende Nachricht</param>
    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    /// <summary>
    /// Aktualisiert den angezeigten Text und ändert die Textfarbe
    /// </summary>
    /// <param name="promptMessage">Die anzuzeigende Nachricht</param>
    /// <param name="promptColor">Die zu verwendende Farbe</param>
    public void UpdateText(string promptMessage, Color promptColor)
    {
        promptText.color = promptColor;
        promptText.text = promptMessage;
    }
}