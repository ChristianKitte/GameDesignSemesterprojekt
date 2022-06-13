using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Der GamePlayManager kontrolliert als Singleton den Ablauf der Spielzeit
    /// </summary>
    public class GamePlayManager : MonoBehaviour
    {
        /// <summary>
        /// Einzige Instanz von GamePlayManager
        /// </summary>
        private static GamePlayManager Instance;

        /// <summary>
        /// Der alte TimeScale
        /// </summary>
        private float oldTimeScale;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                //EventManager.Instance().StartNewGameEvent += ResumeGamePlay;
                EventManager.Instance().PauseGamePlayCallEvent += PauseGamePlay;
                EventManager.Instance().StartGamePlayCallEvent += ResumeGamePlay;
                EventManager.Instance().ResumeGamePlayCallEvent += ResumeGamePlay;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Stoppt den aktuellen Verlauf der Spielzeit durch Setzen der TimeScale
        /// </summary>
        private void PauseGamePlay()
        {
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        /// <summary>
        /// Führt den aktuellen Verlauf der Spielzeit durch Setzen der vorherigen TimeScale fort 
        /// </summary>
        private void ResumeGamePlay()
        {
            Time.timeScale = oldTimeScale;
        }
    }
}