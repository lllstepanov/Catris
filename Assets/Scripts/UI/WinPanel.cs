using UnityEngine;
using UnityEngine.UI;
using Catris;

namespace CatrisUI 
{
    /// <summary>
    /// Visual representation of the GameResult
    /// </summary>
    public class WinPanel : Singleton<WinPanel>
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
        /// Score text
        /// </summary>
        [SerializeField]
        private Text scoreText;

        /// <summary>
        /// BestScore image gameobject
        /// </summary>
        [SerializeField]
        private GameObject newBestScoreImage;

        /// <summary>
        /// Message text
        /// </summary>
        [SerializeField]
        private Text msgText;

        /// <summary>
        /// Panel gameobject
        /// </summary>
        [SerializeField]
        private GameObject winPanel;

        private void Start()
        {
            /// Assign Hide to restart button click
            restartButton.onClick.AddListener(() => Hide());

            /// Assign Restart method of the GameManager to restart button click
            restartButton.onClick.AddListener(() => GameManager.Instance.Restart());

            /// Assign Show method to CatField event
            CatField.Instance.OnGameOver += Show;
        }

        /// <summary>
        /// Sets up panel
        /// </summary>
        internal void SetUp(GameResult gameResult)
        {
            /// Sets the score
            scoreText.text = gameResult.score.ToString();
            
            /// Sets the message
            msgText.text = gameResult.msgText;

            /// Activates the image gameobject
            newBestScoreImage.SetActive(gameResult.bestScore);
        }

        /// <summary>
        /// Hides Panel
        /// </summary>
        private void Hide()
        {
            /// Set bool of the animator transition to false 
            anim.SetBool("Show", false);

            /// Hide gameobject after animation
            Invoke("DeactivateGO", 0.5f);
        }

        /// <summary>
        /// Deactivates gameobject
        /// </summary>
        private void DeactivateGO()
        {
            /// Sets win panel gameobject to false
            winPanel.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Show()
        {
            /// Sets win panel gameobject to true
            winPanel.SetActive(true);

            /// Set bool of the animator transition to true
            anim.SetBool("Show", true);
        }
    }
}

