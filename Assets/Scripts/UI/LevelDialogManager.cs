using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    [CreateAssetMenu(menuName = "Dialog Manager")]
    public class LevelDialogManager : ScriptableObject
    {
        [Tooltip("Ein Prefab, das eine Vorlage für den Dialog bereitstellt")] [SerializeField]
        private LevelDialog dialogPrefab;

        public void Show(
            string text,
            Action okAction,
            Action cancleAction = null,
            string okLabel = "OK",
            string cancelLabel = "Abbrechen")
        {
            var dialog = Instantiate(dialogPrefab);
            dialog.Show(text, okAction, cancleAction, okLabel, cancelLabel);
        }
    }
}