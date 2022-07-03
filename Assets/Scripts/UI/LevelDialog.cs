using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace DefaultNamespace.UI
{
    /// <summary>
    /// Kapselt einen Dialog
    /// </summary>
    public class LevelDialog : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI okText;
        [SerializeField] private TextMeshProUGUI cancelText;
        [SerializeField] private TextMeshProUGUI infoText;

        /// <summary>
        /// Hält eine Instanz von GameState (Singleton)
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// Hält eine Instanz von EventManager (Singleton)
        /// </summary>
        private EventManager eventManager;

        private Action _okAction;
        private Action _cancelAction;

        private void Awake()
        {
            gameState = GameState.Instance();
            eventManager = EventManager.Instance();

            canvas.enabled = false;
        }

        /// <summary>
        /// Zeigt den Dialog an. Das Schließen des Dialogs änder nichts an dem Status
        /// LevelBreak. Er wird auf True gesetzt, wenn der Dialog aufgerufen wurde. Das
        /// Rücksetzen kann nur durch den Start eines neuen Levels erfolgen.
        /// </summary>
        /// <param name="infoText">Der anzuzeigende Text</param>
        /// <param name="okAction">Die Funktion bei Betätigung der OK Taste</param>
        /// <param name="cancelAction">Die Funktion bei Betätigung der Abbrechen Taste</param>
        /// <param name="okLabel">Der Buttontext für die OK Taste</param>
        /// <param name="cancelLabel">Der Buttontext für die Abbrechen Taste</param>
        public void Show(
            string infoText,
            Action okAction,
            Action cancelAction = null,
            string okLabel = "OK",
            string cancelLabel = "Abbrechen")
        {
            SoundManager.Instance.PlayMenueMusic();
            GameState.Instance().LevelMenuVisible = true;

            _okAction = okAction;
            _cancelAction = cancelAction;

            this.infoText.text = infoText;
            okText.text = okLabel;
            cancelText.text = cancelLabel;

            canvas.enabled = true;
        }

        /// <summary>
        /// Bestätigungsfunktion des Dialogs Der Zustand ist immer noch LevelBreak.
        /// </summary>
        public void Ok()
        {
            _okAction?.Invoke();
            Hide();
        }

        /// <summary>
        /// Abbruchfunktion des Dialogs. Der Zustand ist immer noch LevelBreak.
        /// </summary>
        public void Cancel()
        {
            _cancelAction?.Invoke();
            Hide();
        }

        /// <summary>
        /// Zerstören des Dialogs und aufräumen. Es wird das Event
        /// CloseLevelMenuEvent geworfen. Das Zesrtören ändert nichts
        /// an den Status für LevelBreak.
        /// </summary>
        public void Hide()
        {
            SoundManager.Instance.PlayBackgroundMusic();

            canvas.enabled = false;

            gameState = null;
            eventManager = null;

            Destroy(gameObject);
        }
    }
}