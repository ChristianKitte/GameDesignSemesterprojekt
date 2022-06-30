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

        private Action _okAction;
        private Action _cancelAction;

        private void Awake()
        {
            canvas.enabled = false;
        }

        /// <summary>
        /// Zeigt den Dialog an
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
            GameState.Instance().GameLevelDlgIsShowing = true;

            _okAction = okAction;
            _cancelAction = cancelAction;

            this.infoText.text = infoText;
            okText.text = okLabel;
            cancelText.text = cancelLabel;

            canvas.enabled = true;
        }

        /// <summary>
        /// Bestätigungsfunktion des Dialogs
        /// </summary>
        public void Ok()
        {
            _okAction?.Invoke();
            Hide();
        }

        /// <summary>
        /// Abbruchfunktion des Dialogs
        /// </summary>
        public void Cancel()
        {
            _cancelAction?.Invoke();
            Hide();
        }

        /// <summary>
        /// Zerstören des Dialogs
        /// </summary>
        public void Hide()
        {
            SoundManager.Instance.PlayBackgroundMusic();
            GameState.Instance().GameLevelDlgIsShowing = false;

            GameState.Instance().GameIsPaused = false;
            GameState.Instance().GameIsPlaying = true;

            canvas.enabled = false;
            Destroy(gameObject);
        }
    }
}