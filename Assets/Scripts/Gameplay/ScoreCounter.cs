using CatrisUI;

namespace Catris 
{
    /// <summary>
    /// Counts the score of the game
    /// </summary>
    internal class ScoreCounter : Singleton<ScoreCounter>
    {
        /// <summary>
		/// Current score variable
		/// </summary>
        private int currentScore;

        /// <summary>
		/// Delegate variable
		/// </summary>
        private delegate void OnScoreUpdateDelegate();

        /// <summary>
        /// OnScoreUpdate event
        /// </summary>
        private event OnScoreUpdateDelegate OnScoreUpdate;

        /// <summary>
		/// Last sum variable
		/// </summary>
        private int sum = 0;

        private void Start()
        {
            /// Assigns SetMSG method of the ScoreHolder to OnScoreUpdate event
            OnScoreUpdate += () => { ScoreHolder.Instance.SetMsg(sum); };

            /// Assigns SetScore method of the ScoreHolder to OnScoreUpdate event
            OnScoreUpdate += () => { ScoreHolder.Instance.SetCurrentScore(currentScore); };
        }

        /// <summary>
		/// Return current score
		/// </summary>
        internal int GetScore() {
            ///
            return currentScore;
        }

        /// <summary>
		/// Sets current score to 0
		/// </summary>
        internal void ClearScore() 
        {
            ///
            currentScore = 0;
        }

        /// <summary>
		/// Calculates the score
		/// </summary>
        internal void CountScore(int value,int multiplyer) 
        {
            /// Calculation of the score 
            sum = value * 2 * multiplyer;
            
            /// Adding last score calculation to current score
            currentScore += sum;

            /// Checks if OnScoreUpdate event has subs and exicutes
            OnScoreUpdate?.Invoke();
        }
    }
}
