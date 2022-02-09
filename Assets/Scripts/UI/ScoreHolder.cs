using UnityEngine;
using UnityEngine.UI;

namespace CatrisUI 
{
    /// <summary>
    /// Show score on the scene
    /// </summary>
    internal class ScoreHolder : Singleton<ScoreHolder>
    {
        /// <summary>
        /// Animator variable
        /// </summary>
        private Animator anim;

        /// <summary>
        /// UIText variable - shows how many points player gain after merge 
        /// </summary>
        [SerializeField]
        private Text scoreMsg;

        /// <summary>
        /// UIText variable - shows how many points player gain this match
        /// </summary>
        [SerializeField]
        private Text currentScore;

        /// <summary>
        /// UIText variable - shows how many points player gain among all match 
        /// </summary>
        [SerializeField]
        private Text bestScore;

        private void Start()
        {
            /// Assign Animator of the object to local variable 
            anim = GetComponent<Animator>();

            /// Sets the best score according to last saves
            SetBestScore(ProfileManager.Instance.GetBestScore());
        }

        /// <summary>
        /// Show points after merge
        /// </summary>
        internal void SetMsg(int value)
        {
            /// Assign current points to UIText
            scoreMsg.text = "+" + value.ToString();
            
            /// Activates scaling
            anim.SetTrigger("ShowMsg");
        }

        /// <summary>
        /// Sets current score 
        /// </summary>
        internal void SetCurrentScore(int value) 
        {
            /// Assign current score to UIText
            currentScore.text = value.ToString();
            
            /// Activates scaling
            anim.SetTrigger("TweenScore");
        }

        /// <summary>
        /// Sets the best score so far
        /// </summary>
        internal void SetBestScore(int value)
        {
            /// Assign best score to UIText
            bestScore.text = value.ToString();
        }
    }
}