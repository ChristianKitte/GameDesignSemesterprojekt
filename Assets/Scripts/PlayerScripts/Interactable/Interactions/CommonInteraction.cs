using System;
using UnityEngine;

namespace DefaultNamespace.PlayerScripts.PlayerAction
{
    /// <summary>
    /// Eine allgemeine Implementierung der abstracten Klasse Interactable. Muss dem
    /// eine Nachricht aussendenden als Komponente hinzugefügt werden
    /// </summary>
    public class CommonInteraction : Interactable
    {
        protected override void Interact()
        {
            base.Interact();
        }
    }
}