using UnityEngine;
using UnityEngine.UI;

namespace CatrisUI
{
    /// <summary>
    /// Game View interface
    /// </summary>
    internal class MainMenuUI : MonoBehaviour
    {
        /// <summary>
        /// Pause button
        /// </summary>
        [SerializeField]
        private Button pauseButton;

        private void Start()
        {
            /// Assign Show method of PausePanel class to pause button click
            pauseButton.onClick.AddListener(() => PausePanel.Instance.Show());
        }
    }
}
