using UnityEngine;
using UnityEngine.UI;
using Catris;

namespace CatrisUI 
{
    /// <summary>
    /// Enables pause of the game and shows pause panel
    /// </summary>
    internal class PausePanel : Singleton<PausePanel>
    {
        /// <summary>
        /// Animator variable
        /// </summary>
        [SerializeField]
        private Animator anim;

        /// <summary>
        /// Restart button
        /// </summary>
        [SerializeField]
        private Button restartButton;

        /// <summary>
        /// Continue button
        /// </summary>
        [SerializeField]
        private Button continueButton;

        /// <summary>
        /// Panel game object
        /// </summary>
        [SerializeField]
        private GameObject pausePanel;

        /// <summary>
        /// Delegate variable
        /// </summary>
        internal delegate void OnPauseDelegate();

        /// <summary>
        /// On touch event variable
        /// </summary>
        internal event OnPauseDelegate OnPauseEvent;

        private void Start()
        {
            /// Assign Hide to continue button click
            continueButton.onClick.AddListener(() => Hide());

            /// Assign OnPause event to continue button click
            continueButton.onClick.AddListener(() => OnPauseEvent?.Invoke());

            /// Assign Hide to restart button click
            restartButton.onClick.AddListener(() => Hide());

            /// Assign OnPause event to restart button click
            restartButton.onClick.AddListener(() => OnPauseEvent?.Invoke());

            /// Assign Restart method of the GameManager to restart button click
            restartButton.onClick.AddListener(() => GameManager.Instance.Restart());
        }

        /// <summary>
        /// Hides panel
        /// </summary>
        private void Hide()
        {
            /// Set bool of the animator transition to false 
            anim.SetBool("Show", false);
            
            /// Hide gameobject after animation
            Invoke("DeactivateGO", 0.5f);
        }

        /// <summary>
        /// Show panel
        /// </summary>
        internal void Show() 
        {
            /// Enable OnPause event
            OnPauseEvent?.Invoke();

            /// Sets pause panel gameobject to true
            pausePanel.SetActive(true);

            /// Set bool of the animator transition to true
            anim.SetBool("Show", true);
        }

        /// <summary>
        /// Deactivates gameobject
        /// </summary>
        private void DeactivateGO()
        {
            /// Sets pause panel gameobject to false
            pausePanel.SetActive(false);
        }
    }
}
