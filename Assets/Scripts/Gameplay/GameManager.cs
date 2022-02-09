using CatrisUI;

namespace Catris 
{
    /// <summary>
    /// Handles restart of the game and creates win information 
    /// </summary>
    internal class GameManager : Singleton<GameManager>
    {
        /// <summary>
		/// Text of future game result
		/// </summary>
        private string greatText = "Great!";

        /// <summary>
        /// Text of future game result
        /// </summary>
        private string newRecordText = "New Record!";

        private void Start()
        {
            /// Assign CreateGameResult method to OnGameOver event
            CatField.Instance.OnGameOver += ()=> {CreateGameResult(ScoreCounter.Instance.GetScore());};
        }

        /// <summary>
		/// Creates a new game result event and set up win panel
		/// </summary>
        internal void CreateGameResult(int score)
        {
            /// Creates empty GameResult object
            GameResult gameResult = new GameResult();

            /// Assign score to GameResult object
            gameResult.score = score;

            /// Check if there is a new record
            if (ProfileManager.Instance.GetBestScore() < score)
            {
                /// Sends new record to profile
                ProfileManager.Instance.SetBestScore(score);
                
                /// Sends new record to best score text on the scene
                ScoreHolder.Instance.SetBestScore(score);

                /// Sets the bestscore of the gameresult object to true
                gameResult.bestScore = true;
                
                /// Sets message of the gameresult object
                gameResult.msgText = newRecordText;
            }
            else
            {
                /// Sets message of the gameresult object
                gameResult.msgText = greatText;
            }

            /// Set up a win panel with gameresult object
            WinPanel.Instance.SetUp(gameResult);
        }

        /// <summary>
		/// Resets the game scene
		/// </summary>
        internal void Restart() 
        {
            /// Clears field from the cats
            CatField.Instance.ClearField();

            /// Clears queue from number from last game
            CatQueue.Instance.ClearQueue();
            
            /// Generates the new queue
            CatQueue.Instance.GenerateQueue();

            /// Updates the CatViewer object
            CatViewer.Instance.SetUp();

            /// Cleans score for new game
            ScoreCounter.Instance.ClearScore();
            
            /// Sets current score of the new game to 0
            ScoreHolder.Instance.SetCurrentScore(0);
        }
    }

    /// <summary>
    /// GameResult class contains information of the results from last game
    /// </summary>
    internal class GameResult
    {
        /// <summary>
        /// Message of the result
        /// </summary>
        internal string msgText = "";

        /// <summary>
        /// Best score flag
        /// </summary>
        internal bool bestScore = false;

        /// <summary>
        /// Players score from last game
        /// </summary>
        internal int score = 0;
    }
}
